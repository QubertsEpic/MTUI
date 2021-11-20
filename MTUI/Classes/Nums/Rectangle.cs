using MTUI.Classes.Buffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MTUI.ConsoleInstance;

namespace MTUI.Classes.Nums
{
    public class Rectangle
    {
        public int Top;
        public int Bottom;
        public int Left;
        public int Right;
        public Rectangle()
        {

        }

        public Rectangle(int top, int bottom, int left, int right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }


        public static void MakeLines(ref CharInfo[] info, Vector.VectorI2 size)
        {
            if (info == null)
                return;
            for (int i = 0; i < size.Y; i++)
            {
                info[size.Y * i].Char.UnicodeChar = '│';
                info[(size.Y * i) + size.Y - 1].Char.UnicodeChar = '│';
            }
            for (int i = 0; i < size.X; i++)
            {
                info[i].Char.UnicodeChar = '─';
                info[((size.Y - 1) * size.X) + i].Char.UnicodeChar = '─';
            }
        }

        public static void MakeFrame(ref CharInfo[] info, Vector.VectorI2 size)
        {

            info[0].Char.UnicodeChar = '┌';
            info[size.X - 1].Char.UnicodeChar = '┐';
            info[(size.Y * size.X) - size.X].Char.UnicodeChar = '└';
            info[size.Y * size.X - 1].Char.UnicodeChar = '┘';
        }

        public CharInfo[] Draw()
        {
            CharInfo[] buffer = BufferOperations.CreateBuffer((UInt32) (Bottom*Right), ' ', BufferAttributes.ForegroundWhite);
            MakeLines(ref buffer, GetSize());
            MakeFrame(ref buffer, GetSize());
            return buffer;
        }

        public Vector.VectorI2 GetSize() => new Vector.VectorI2(Right, Bottom);

        private bool CheckAllValues()
        {
            if (Top == int.MinValue || Bottom == int.MinValue || Left == int.MinValue || Right == int.MinValue)
            {
                return false;
            }
            return true;
        }

        public bool Intersecting(Rectangle rectangle)
        {
            if (rectangle == null)
            {
                throw new NullReferenceException("Cannot compare to a null rectangle.");
            }
            if (!CheckAllValues())
                return false;
            if (Bottom > rectangle.Top - 1 && Right > rectangle.Left - 1 && Left < rectangle.Right + 1 && Top < rectangle.Bottom + 1)
                return true;
            return false;
        }
    }
}
