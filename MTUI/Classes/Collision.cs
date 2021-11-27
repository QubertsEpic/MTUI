using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTUI.Classes;

namespace MTUI.Classes
{
    public static class Collision
    {
        public static bool Intersection(Vector.Vector<int> location, Vector.Vector<int> boxStartingPoint, Vector.Vector<int> boxEndingPoint)
        {
            //Makes sure that all of the variables are valid.
            if (location == null || boxStartingPoint == null || boxEndingPoint == null)
                throw new NullReferenceException("Variables were not ");
            /*
             * Checks if the location is more than the x and y starting points (upper left-hand corner) and if the location is less than the x and y values of the ending points (lower right-hand corner).
             */
            if (location.X > boxStartingPoint.X -1 && location.Y > boxStartingPoint.Y - 1 && location.X < boxStartingPoint.X + boxEndingPoint.X && location.Y < boxStartingPoint.Y + boxEndingPoint.Y)
                return true;
            return false;
        }
    }
}
