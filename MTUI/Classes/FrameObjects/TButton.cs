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
        public VectorI2 Location { get; set; }
        public int Layer { get; set; }
        public bool Selected { get; set; }

        public TLabel Label;
        public TButton(string text, VectorI2 location)
        {
            Label = new TLabel(text, new VectorI2(0, 0));
            Location = location ?? throw new NullReferenceException("Location cannot be null");
            Layer = 0;
            Selected = false;
        }

        public char[,] Compose()
        {
            if (Label == null || Location == null || Layer < 0)
                throw new InvalidOperationException("Cannot compose object when parameters and not correctly set.");
           char[,] labelBuffer = Label.Compose();

            if (labelBuffer == null)
                throw new NullReferenceException("Cannot compose while label buffer is null");
            char[,] finBuffer = new char[1, labelBuffer.GetLength(1) + 2];

            finBuffer[0, 0] = '[';
            finBuffer[0, finBuffer.GetLength(1) - 1] = ']';

            Compositor.Transpose(ref finBuffer, labelBuffer, new VectorI2(1,0));

            return finBuffer;
        }
    }
}
