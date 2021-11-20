using MTUI.Classes.Vector;
using MTUI.Interfaces;
using System;

namespace MTUI.Classes.FrameObjects
{
    public class TLabel : FrameObject
    {
        public VectorI2 Location { get; set; }
        public VectorI2 Size { get; set; }
        public int Layer { get; set; }
        public bool Selected { get; set; }

        public string Text;
        public ConsoleColor Foreground, Background;
        public TLabel(string text, VectorI2 location, ConsoleColor foregroundColour = ConsoleColor.White, ConsoleColor backgroundColour = ConsoleColor.Black)
        {
            Location = location ?? throw new Exception("Cannot composite object with no location");
            Text = text;
            Foreground = foregroundColour;
            Background = backgroundColour;
        }

        //Todo: allow for mutli line labels. This system isn't good enough.
        public char[,] Compose()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return new char[0, 0];
            char[,] characters = new char[1, Text.Length];
            for (int i = 0; i < Text.Length; i++)
            {
                characters[0 , i] = Text[i];
            }
            return characters;
        }
    }
}
