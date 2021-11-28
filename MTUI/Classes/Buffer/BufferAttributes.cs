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
        
        BackgroundBlack = 0x0000,
        BackgroundWhite = 0x00f0,
        BackgroundDarkBlue = 0x0010,
        BackgroundDarkGreen = 0x0020,
        BackgroundDarkTurqoise = 0x0030,
        BackgroundDarkRed = 0x0040,
        BcakgroundDarkMagenta = 0x0050,
        BackgroundDarkYellow = 0x0060,
        BackgroundDarkGray = 0x0070,
        BackgroundLightGray = 0x0080,
        BackgroundBlue = 0x0090,
        BackgroundGreen = 0x00a0,
        BackgroundTurquoise = 0x00b0,
        BackgroundRed = 0x00c0,
        BackgroundMagenta = 0x00d0,
        BackgroundYellow = 0x00e0
    }

    public static class BufferAttributeOperations
    {
        public static short Concatinate(BufferAttributes[] attributes)
        {
            if (attributes == null)
                return 0;
            short output = 0;
            for(int i = 0; i < attributes.Length; i++)
            {
                output += (short) attributes[i];
            }
            return output;
        }
    }
}
