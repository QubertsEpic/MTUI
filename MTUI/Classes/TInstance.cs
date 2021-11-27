using MTUI.Classes.Buffer;
using MTUI.Classes.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MTUI.Classes.Data.P_invoke;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using MTUI.Classes.Vector;
using MTUI.Classes.FrameObjects;

namespace MTUI.Classes
{
    public class TInstance
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool WriteConsoleOutputW(SafeFileHandle hConsoleOutput, CharInfo[] lpBuffer, ShortCoord dwBufferSize, ShortCoord dwBufferCoord, ref WindowRect lpWriteRegion);

        [DllImport("Kernel32.dll")]
        private static extern SafeFileHandle GetStdHandle(int nStdHandle);

        public CharInfo[] StandardBuffer;

        public Buffer<CharInfo> DrawBuffer;

        private Buffer<CharInfo> ReservedBuffer;

        public List<TWindow> Frames = new List<TWindow>();

        public WindowDisplayType DisplayType;

        public Random Random = new Random();

        public int CurrentlySelectedLayer;

        public Thread DrawThread;

        public bool Alive;

        public TCursor Cursor;
        public Vector.Vector<int> ConsoleDimensions, CursorPosition;

        const int FPS = 144;
        const int frameDelay = 1000 / FPS;
        /// <summary>
        /// Tiling currently does not work.
        /// </summary>
        /// <param name="displayType"></param>
        public TInstance(WindowDisplayType displayType = WindowDisplayType.Normal)
        {
            DisplayType = WindowDisplayType.Normal;
            CurrentlySelectedLayer = 0;
            Cursor = new TCursor();
            DrawBuffer = new Buffer<CharInfo>((ConsoleDimensions = new Vector.Vector<int>(Console.WindowWidth, Console.WindowHeight)), new CharInfo() { Char = new CharUnion { UnicodeChar = ' ' } });
            ResetBuffers();
        }

        public void Init()
        {
            new Thread(Process).Start();
            new Thread(UpdateScreen).Start();
        }

        public void AddFrame(TWindow frame)
        {
            if (frame == null)
                return;
            Frames.Add(frame);
        }

        public void Process()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            long frameTime, frameStart;
            while (true)
            {
                frameStart = stopwatch.ElapsedMilliseconds;
                if (Frames == null)
                {
                    throw new NullReferenceException("Cannot build image without frames.");
                }

                //Updates Cursor Position.
                Cursor.CalculateCorrection(DrawBuffer.GetSize());
                CursorPosition = Cursor.UpdatePosition();

                UpdateCollision(CursorPosition);

                GenerateBuffer();

                var debug = "X: " + CursorPosition.X.ToString() + "ch Y: " + CursorPosition.Y.ToString() + "ch" + " Colliding: " + CurrentlySelectedLayer.ToString();
                TLabel label = new TLabel(debug, null);
                DrawBuffer.Transpose(label.Compose(), new Vector.Vector<int>(0, 0));

                ResetBuffers();

                frameTime = stopwatch.ElapsedMilliseconds - frameStart;

                
                if(frameDelay > frameTime)
                {
                    Thread.Sleep((int) (frameDelay - frameTime));
                }
            }
        }

        public void ResetBuffers()
        {
            ReservedBuffer = DrawBuffer;
            DrawBuffer = new Buffer<CharInfo>((ConsoleDimensions = new Vector.Vector<int>(Console.WindowWidth, Console.WindowHeight)), new CharInfo() { Char = new CharUnion { UnicodeChar = ' ' }, Atrributes = (int) BufferAttributes.ForegroundWhite });
        }

        private void UpdateScreen()
        {
            Stopwatch watch = Stopwatch.StartNew();
            SafeFileHandle handle = GetStdHandle(-11);
            while (true)
            {
                long startTime = watch.ElapsedMilliseconds;
                WindowRect rect = new WindowRect() { Top = 0, Left = 0, Right = (short) ReservedBuffer.GetSize().X, Bottom = (short) ReservedBuffer.GetSize().Y};
                long endTime;
                Console.Title = "Output Status: " +  WriteConsoleOutputW(handle, ReservedBuffer.GetBuffer(), new ShortCoord((short)ReservedBuffer.GetSize().X,(short) ReservedBuffer.GetSize().Y), new ShortCoord(0,0), ref rect).ToString() + " FrameTime: " + (endTime = watch.ElapsedMilliseconds - startTime).ToString() + "ms";
                ;
                if(endTime < frameDelay)
                {
                    Thread.Sleep((int)(frameDelay - endTime));
                }
            }
        }

        public void GenerateBuffer()
        {
            for (int i = 0; i < Frames.Count; i++)
            {
                if (Frames[i] == null)
                    continue;
                Buffer<CharInfo> subBuffer = Frames[i].GetBuffer();
                DrawBuffer.Transpose(subBuffer, Frames[i].Location);
            }
        }

        public void UpdateCollision(Vector.Vector<int> position)
        {
            for(int i = 0; i < Frames.Count; i++)
            {
                if (Collision.Intersection(position, Frames[i].Location, Frames[i].Size))
                {
                    CurrentlySelectedLayer = i;
                    return;
                }
            }
            CurrentlySelectedLayer = -1;
        }
    }

    /// <summary>
    /// Tiling display type does not currently work.
    /// </summary>
    public enum WindowDisplayType
    {
        Normal, Tiling
    }
}
