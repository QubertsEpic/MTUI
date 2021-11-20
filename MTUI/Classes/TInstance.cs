using MTUI.Classes.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MTUI.ConsoleInstance;

namespace MTUI.Classes
{
    public class TInstance
    {
        public List<TWindow> Frames = new List<TWindow>();

        public WindowDisplayType DisplayType;

        public Random Random = new Random();

        public int CurrentlySelectedLayer;

        public Thread DrawThread;

        public bool Alive;

        public TCursor Cursor;
        public Vector.VectorF2 CursorPosition;


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
        }

        public void AddFrame(TWindow frame)
        {
            if (frame == null)
                return;
            Frames.Add(frame);
        }

        public void Init()
        {
            Alive = true;
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
                    ConsoleInstance.LogClient.Write("Error: No Frames Loaded.");
                    throw new NullReferenceException("Cannot build image without frames.");
                }

                //Updates Cursor Position.

                Cursor.CalculateCorrection();
                ConsoleInstance.CursorPosition = Cursor.UpdatePosition();

                UpdateCollision();

                GenerateBuffer();

                string debug = "X: " + ConsoleInstance.CursorPosition.X.ToString() + "ch Y: " + ConsoleInstance.CursorPosition.Y.ToString() + "ch" + " Colliding: " + CurrentlySelectedLayer.ToString();
                Console.Title = debug;

                ConsoleInstance.SwapBuffers();

                frameTime = stopwatch.ElapsedMilliseconds - frameStart;

                
                if(frameDelay > frameTime)
                {
                    Thread.Sleep((int) (frameDelay - frameTime));
                }
            }
        }

        public void GenerateBuffer()
        {
            for (int i = 0; i < Frames.Count; i++)
            {
                if (Frames[i] == null)
                    continue;
                CharInfo[] subBuffer = Frames[i].GetBuffer();
                if (subBuffer.GetLength(0) > ConsoleInstance.ConsoleHeight || subBuffer.GetLength(1) > ConsoleInstance.ConsoleWidth)
                    continue;
                Compositor.Transpose(ConsoleInstance.DrawBuffer, ConsoleInstance.GetBufferSize(), subBuffer, Frames[i].Size, Frames[i].Location);
            }
        }

        public void UpdateCollision()
        {
            if (ConsoleInstance.CursorPosition == null)
                return;
            for(int i = 0; i < Frames.Count; i++)
            {
                if (Collision.Intersection(ConsoleInstance.CursorPosition, Frames[i].Location, Frames[i].Size))
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
