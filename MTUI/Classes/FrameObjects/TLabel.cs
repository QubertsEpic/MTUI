using MTUI.Classes.Buffer;
using MTUI.Classes.Data.P_invoke;
using MTUI.Classes.Vector;
using MTUI.Interfaces;
using System;

namespace MTUI.Classes.FrameObjects
{
    public class TLabel : FrameObject
    {
        public Vector<int> Location { get; set; }
        public Vector<int> Size { get; set; }
        public int Layer { get; set; }
        public bool Selected { get; set; }

        public string Text;
        public BufferAttributes foregroundColor, backgroundColor;
        public TLabel(string text, Vector<int> location, BufferAttributes foregroundColor = BufferAttributes.ForegroundWhite, BufferAttributes backgroundColor = BufferAttributes.BackgroundBlack)
        {
            Location = location ?? new Vector<int>(0,0);
            Text = text;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
        }

        //Todo: allow for mutli line labels. This system isn't good enough.
        public Buffer<CharInfo> Compose()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return new Buffer<CharInfo>(new Vector.Vector<int>(0, 0), new CharInfo());
            Buffer<CharInfo> characters = new Buffer<CharInfo>(new Vector<int>(Text.Length, 1), new CharInfo() { Char = new CharUnion() { UnicodeChar = ' ' }, Atrributes = BufferAttributeOperations.Concatinate(new[] { foregroundColor, backgroundColor })});
            for (int i = 0; i < Text.Length; i++)
            {
                characters.bufferArray[i].Char.UnicodeChar = Text[i];
            }
            return characters;
        }

    }
}
