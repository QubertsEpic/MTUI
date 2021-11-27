using MTUI.Classes.Vector;
using MTUI.Classes.Buffer;
using MTUI.Classes.Data.P_invoke;

namespace MTUI.Interfaces
{
    public interface FrameObject
    {
        Vector<int> Location { get; set; }
        int Layer { get; set; }
        bool Selected { get; set; }
        Buffer<CharInfo> Compose();
        
    }
}
