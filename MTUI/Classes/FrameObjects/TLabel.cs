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
        public ConsoleColor Foreground, Background;
        public TLabel(string text, Vector<int> location, ConsoleColor foregroundColour = ConsoleColor.White, ConsoleColor backgroundColour = ConsoleColor.Black)
        {
            Location = location ?? new Vector<int>(0,0);
            Text = text;
            Foreground = foregroundColour;
            Background = backgroundColour;
        }

        //Todo: allow for mutli line labels. This system isn't good enough.
        public Buffer<CharInfo> Compose()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return new Buffer<CharInfo>(new Vector.Vector<int>(0, 0), new CharInfo());
            Buffer<CharInfo> characters = new Buffer<CharInfo>(new Vector<int>(Text.Length, 1), new CharInfo() { Char = new CharUnion() { UnicodeChar = ' ' }, Atrributes = (int) BufferAttributes.ForegroundWhite });
            for (int i = 0; i < Text.Length; i++)
            {
                characters.bufferArray[i].Char.UnicodeChar = Text[i];
            }
            return characters;
        }

    }
}
