using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.Data.P_invoke
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ShortCoord
    {
        public short X;
        public short Y;
        public ShortCoord(short x, short y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
