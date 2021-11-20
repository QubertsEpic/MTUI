using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using MTUI.Classes;
using MTUI.Classes.Buffer;
using MTUI.Classes.Data;
using MTUI.Classes.Vector;

namespace MTUI
{

    public class ConsoleInstance
    {
        #region BufferStructs
        [StructLayout(LayoutKind.Sequential)]
        public struct BufferCoord
        {
            public short X;
            public short Y;
            public BufferCoord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };
        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct CharUnion
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Atrributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public short Left { get; set; }
            public short Top { get; set; }
            public short Right { get; set; }
            public short Bottom { get; set; }

        }
        #endregion

        public static CharInfo[] DrawBuffer;
        private static CharInfo[] _activeBuffer;
        public static int CurrentBuffer = 0;
        public static int ConsoleWidth, ConsoleHeight;
        private static WindowRect _screenSize;
        public static VectorI2 CursorPosition;
        public static Log LogClient;
        public static bool Alive;
        public static int FPS = 60, FPSInterval = 1000 / FPS;
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool WriteConsoleOutput(SafeFileHandle hConsoleOutput, CharInfo[] lpBuffer, BufferCoord dwBufferSize, BufferCoord dwBufferCoord, ref WindowRect lpWriteRegion);

        [DllImport("Kernel32.dll")]
        private static extern SafeFileHandle GetStdHandle(int nStdHandle);

       

        static ConsoleInstance()
        {
            LogClient = new Log();

            ResetBuffers();
            Alive = true;
            new Thread(Draw).Start();
        }

        public static VectorI2 GetBufferSize() => new VectorI2(ConsoleWidth, ConsoleHeight);

        private static void ResetBuffers()
        {
            DrawBuffer = BufferOperations.CreateBuffer((uint)((ConsoleWidth = Console.WindowWidth)*(ConsoleHeight = Console.WindowHeight)), '░', (short) BufferAttributes.ForegroundWhite);
            _screenSize = new WindowRect() { Left = 0, Top = 0, Bottom = (short) ConsoleHeight, Right = (short)ConsoleWidth};
            Console.CursorVisible = false;
        }

        public static void SwapBuffers()
        {
            if (DrawBuffer == null)
            {
                throw new Exception("DrawBuffer invalid.");
            }
            _activeBuffer = DrawBuffer;
            ResetBuffers();
        }

        //Method for drawing anything on the currently selected buffer.
        public static void Draw()
        {
            Stopwatch timer = Stopwatch.StartNew();
            //-11 is written as it is equal to DWORD "STD_OUTPUT_HANDLE" 
            SafeFileHandle handle = GetStdHandle(-11);
            long startTime, endTime;
            int i = 0;
            while (true)
            {
                Console.Title = (++i).ToString() + "f : " + ((startTime = timer.ElapsedMilliseconds)/1000).ToString() + "s";
                
                WriteConsoleOutput(handle, _activeBuffer, new BufferCoord((short)ConsoleWidth, (short)ConsoleHeight), new BufferCoord(0, 0), ref _screenSize);
                
                endTime = timer.ElapsedMilliseconds - startTime;
                if (endTime < FPSInterval)
                    Thread.Sleep((int) (FPSInterval - endTime));
            }
        }
    }
}
