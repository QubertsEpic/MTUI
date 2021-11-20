using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.Buffer
{
    [Flags]
    public enum BufferAttributes
    {
        /// <summary>
        /// Do not use the "Add" enums after using one of the traditional foreground colours.
        /// </summary>
        ForegroundAddBlue = 0x0001,
        ForegroundAddGreen = 0x0002,
        ForegroundAddRed = 0x0004,
        ForegroundAddIntensify = 0x0008,

        BackgroundAddBlue = 0x0010,
        BackgroundAddGreen = 0x0020,
        BackgroundAddRed = 0x0040,
        BackgroundIntensify = 0x0080,

        ForegroundBlack = 0x0000,
        ForegroundDarkBlue = 0x0001,
        ForegroundDarkGreen = 0x0002,
        ForegroundDarkTurquoise = 0x0003,
        ForegroundDarkRed = 0x0004,
        ForegroundDarkMagenta = 0x0005,
        ForeGroundDarkYellow = 0x0006,
        ForegroundDarkGray = 0x0007,
        ForegroundLightGray = 0x0008,
        ForegroundBlue = 0x0009,
        ForegroundGreen = 0x000a,
        ForegroundTurquoise = 0x000b,
        ForegroundRed = 0x000c,
        ForegroundMegenta = 0x000d,
        ForegroundYellow = 0x000e,
        ForegroundWhite = 0x000f,
        
        BackgroundWhite = 0x00f0
    }
}
