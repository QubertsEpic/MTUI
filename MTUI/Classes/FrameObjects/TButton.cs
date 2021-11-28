using MTUI.Classes.Buffer;
using MTUI.Classes.Data.P_invoke;
using MTUI.Classes.Vector;
using MTUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.FrameObjects
{
    public class TButton : FrameObject
    {
        public Vector<int> Location { get; set; }
        public int Layer { get; set; }
        public bool Selected { get; set; }

        public TLabel Label;
        public TButton(string text, Vector<int> location)
        {
            Label = new TLabel(text, new Vector<int>(0, 0));
            Location = location ?? throw new NullReferenceException("Location cannot be null");
            Layer = 0;
            Selected = false;
        }

        public Buffer<CharInfo> Compose()
        {
            if (Label == null || Location == null || Layer < 0)
                throw new InvalidOperationException("Cannot compose object when parameters and not correctly set.");
            Buffer<CharInfo> labelBuffer = Label.Compose();

            if (labelBuffer == null)
                throw new NullReferenceException("Cannot compose while label buffer is null");
            Buffer<CharInfo> finBuffer = new Buffer<CharInfo>(new Vector.Vector<int>(labelBuffer.GetLength(0) + 2, 1), new CharInfo() { Atrributes = (int) BufferAttributes.ForegroundWhite, Char = new CharUnion() { UnicodeChar = ' '} });

            finBuffer.bufferArray[finBuffer.ConvertTo1D(0, 0)].Char.UnicodeChar = '[';
            finBuffer.bufferArray[finBuffer.ConvertTo1D(finBuffer.GetLength(0) - 1, 0)].Char.UnicodeChar = ']';

            finBuffer.Transpose(labelBuffer, new Vector<int>(1, 0));

            return finBuffer;
        }
    }
}
