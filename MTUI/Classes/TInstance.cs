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

        public Vector.Vector<int> BufferCorrection;

        public Buffer<CharInfo> DrawBuffer;

        private Buffer<CharInfo> ReservedBuffer;

        public List<TWindow> Frames = new List<TWindow>();

        public Random Random = new Random();

        public int CurrentlySelectedLayer;

        public Thread DrawThread;

        private string consoleTitle;

        public string ConsoleTitle
        {
            get { return consoleTitle; }
            set
            {
                Console.Title = value;
                consoleTitle = value;
            }
        }

        public bool Alive;

        public WindowRect rect;

        public TCursor Cursor;
        public Vector.Vector<int> ConsoleDimensions, CursorPosition;

        private int fps;

        public int FPS
        {
            get { return fps; }
            set
            {
                fps = value;
                frameDelay = 1000 / value;
            }
        }

        int frameDelay;



        public Thread ProcessingThread, UpdatingThread;
        /// <summary>
        /// Tiling currently does not work.
        /// </summary>
        /// <param name="displayType"></param>
        public TInstance(int fps)
        {
            CurrentlySelectedLayer = 0;
            Cursor = new TCursor();
            DrawBuffer = new Buffer<CharInfo>((ConsoleDimensions = new Vector.Vector<int>(Console.WindowWidth, Console.WindowHeight)), new CharInfo() { Char = new CharUnion { UnicodeChar = ' ' } });
            FPS = fps;
            ResetBuffers();
        }

        public void Init()
        {
            Alive = true;
            ProcessingThread = new Thread(Process);
            ProcessingThread.Start();
            UpdatingThread = new Thread(UpdateScreen);
            ConsoleTitle = "MTUI Instance";
            UpdatingThread.Start();
        }

        /// <summary>
        /// Stops MTUI from processing and rendering.
        /// </summary>
        public void Stop()
        {
            Alive = false;
        }

        public void AddFrame(TWindow frame)
        {
            if (frame == null)
                throw new NullReferenceException("Cannot add null window.");
            Frames.Add(frame);
        }

        public void MakeNewFrame(Vector.Vector<int> location, Vector.Vector<int> size = null, Vector.Vector<int> minSize = null, Vector.Vector<int> maxSize = null)
        {
            TWindow window = new TWindow(location, size, minSize, maxSize);
            AddFrame(window);
        }

        public void Process()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (Alive)
            {
                long frameStart = stopwatch.ElapsedMilliseconds;
                if (Frames == null)
                {
                    throw new NullReferenceException("Cannot build image without frames.");
                }

                //Updates Cursor Position.

                Cursor.CalculateCorrection(new Vector.Vector<int>(DrawBuffer.GetSize().X / 3, DrawBuffer.GetSize().Y / 3));
                CursorPosition = Cursor.UpdatePosition();

                UpdateCollision(CursorPosition);

                GenerateBuffer();

                var debug = "X: " + CursorPosition.X.ToString() + "ch Y: " + CursorPosition.Y.ToString() + "ch" + " Colliding: " + CurrentlySelectedLayer.ToString() + " Current FPS: " + FPS;
                TLabel label = new TLabel(debug, null, foregroundColor: BufferAttributes.ForegroundTurquoise);
                DrawBuffer.Transpose(label.Compose(), new Vector.Vector<int>(0, ConsoleDimensions.Y - 1), BufferCorrection);

                ResetBuffers();

                long frameTime = stopwatch.ElapsedMilliseconds - frameStart;


                if (frameDelay > frameTime)
                {
                    Thread.Sleep((int)(frameDelay - frameTime));
                }
            }
        }

        private void ResetBuffers()
        {
            ReservedBuffer = DrawBuffer;
            ConsoleDimensions = new Vector.Vector<int>(Console.WindowWidth, Console.WindowHeight);
            DrawBuffer = new Buffer<CharInfo>(new Vector.Vector<int>((ConsoleDimensions.X - 1) * 3, (ConsoleDimensions.Y - 1) * 3), new CharInfo() { Char = new CharUnion { UnicodeChar = '░' }, Atrributes = (int)BufferAttributes.ForegroundRed });
            BufferCorrection = new Vector<int>(DrawBuffer.GetSize().X / 3, DrawBuffer.GetSize().Y / 3);
            rect = new WindowRect()
            {
                Bottom = (short)ConsoleDimensions.Y,
                Right = (short)ConsoleDimensions.X
            };
            Console.CursorVisible = false;
        }

        private void UpdateScreen()
        {
            Stopwatch watch = Stopwatch.StartNew();
            SafeFileHandle handle = GetStdHandle(-11);
            while (Alive)
            {
                long startTime = watch.ElapsedMilliseconds;
                /* Console.Title = "Output Status: " + */
                WriteConsoleOutputW(handle, ReservedBuffer.GetBuffer(),
                         new ShortCoord((short)ReservedBuffer.GetSize().X, (short)ReservedBuffer.GetSize().Y),
                    new ShortCoord((short)(BufferCorrection.X), (short)(BufferCorrection.Y)), ref rect);
                long endTime = watch.ElapsedMilliseconds - startTime;
                if (endTime < frameDelay)
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
                DrawBuffer.Transpose(subBuffer, Frames[i].Location, BufferCorrection);
            }
        }

        public void UpdateCollision(Vector.Vector<int> position)
        {
            for (int i = 0; i < Frames.Count; i++)
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
