using MTUI.Classes.Vector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes
{
    public class TCursor
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        /// <summary>
        /// Gets the window dimentions.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        /// <summary>
        /// Gets the current console instance.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// Gets the current position of the cursor.
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        private static IntPtr Window;
        private static Rect WindowRectangle = new Rect();
        private static POINT CursorPosition = new POINT();
        private static bool Alive;
        private static Vector.VectorF2 CursorCorrections;

        public TCursor()
        {
            Alive = true;
            Window = GetConsoleWindow(); 
        }
        
        public void CalculateCorrection(Vector.Vector<int> bufferSize)
        {
            UpdatePosition(false);
            
            CursorCorrections = new Vector.VectorF2((WindowRectangle.Right-WindowRectangle.Left)/bufferSize.X ,(WindowRectangle.Bottom - WindowRectangle.Top)/bufferSize.Y);
        }

        public Vector.Vector<int> UpdatePosition(bool ChangeCursorPosition = true)
        {
            GetCursorPos(out CursorPosition);
            GetWindowRect(Window, ref WindowRectangle);
            
            if(ChangeCursorPosition)
                return CurrentCursorPosition();
            return null;
        }

        public Vector.Vector<int> CurrentCursorPosition()
        { 
            if (CursorCorrections == null)
               return null;
            return new Vector.Vector<int>((int) ((CursorPosition.x - WindowRectangle.Left) / CursorCorrections.X)-1, (int) ((CursorPosition.y - WindowRectangle.Top) / CursorCorrections.Y-2));
        }
    }
}
