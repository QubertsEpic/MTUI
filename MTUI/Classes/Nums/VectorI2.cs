using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.Vector
{
    public class VectorI2
    {
        public int X;
        public int Y;
        public VectorI2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public VectorI2(int x)
        {
            X = x;
            Y = int.MinValue;
        }
    }
}
