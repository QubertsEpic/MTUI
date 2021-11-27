using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.Vector
{
    public class Vector<T>
    {
        public T X;
        public T Y;
        public Vector(T x, T y)
        {
            X = x;
            Y = y;
        }

        public Vector(T x)
        {
            X = x;
            Y = default(T);
        }
    }
}
