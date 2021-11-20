using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.Vector
{
    /// <summary>
    /// A struct used to recieve the data from the Classes.TCursor.GetWindowRect(IntPtr hwnd, ref RECT rect) method.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }
}
