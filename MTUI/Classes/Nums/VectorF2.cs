using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.Vector
{
    public class VectorF2
    {
        public double X, Y;
        public VectorF2(double x, double y)
        {
            X = x;
            Y = y;
        }
        public VectorF2(double x)
        {
            X = x;
            Y = double.MinValue;
        }

    }
}
