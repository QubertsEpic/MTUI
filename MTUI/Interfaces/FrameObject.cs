using MTUI.Classes.Vector;
using System;

namespace MTUI.Interfaces
{
    public interface FrameObject
    {
        VectorI2 Location { get; set; }
        int Layer { get; set; }
        bool Selected { get; set; }
        char[,] Compose();
        
    }
}
