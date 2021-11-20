using MTUI.Classes.Exceptions;
using MTUI.Interfaces;
using System;
using static MTUI.ConsoleInstance;

namespace MTUI.Classes
{
    public class TWindow
    {
        public TWindowContent WindowContent;
        public Vector.VectorI2 Location, Size, MinSize, MaxSize, CursorPosition;
        public int Layer;
        public string FrameText;
        public bool Visible = true, ChangesMade;
        public char[,] buffer;
        /// <summary>
        /// Any X plane size will be doubled.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="minSize"></param>
        public TWindow(Vector.VectorI2 location, Vector.VectorI2 size = null, Vector.VectorI2 minSize = null, Vector.VectorI2 maxSize = null)
        {

            size = size ?? new Vector.VectorI2(4, 4);
            size.X = size.X * 2;
            WindowContent = new TWindowContent(size);
            Location = location ?? throw new Exception("Location cannot be null");
            Size = new Vector.VectorI2(WindowContent.Size.X + 2, WindowContent.Size.Y + 2);
            MinSize = new Vector.VectorI2(2, 1);
            MaxSize = new Vector.VectorI2(40, 20);
            CursorPosition = new Vector.VectorI2(-1, -1);
            ChangesMade = true;

            if (minSize != null)
            {
                MinSize = minSize;
                if (size.Y < minSize.Y || size.X < minSize.X)
                {
                    ResizeWindow(minSize);
                }
            }
            if (maxSize != null)
            {
                MaxSize = maxSize;
                if (size.Y > maxSize.Y || size.X > maxSize.Y)
                {
                    ResizeWindow(maxSize);
                }
            }
        }


        public void SetCursorPosition(Vector.VectorI2 position)
        {
            if (position == null)
            {
                return;
            }
            if (position.X > Size.X || position.Y > Size.Y || position.X < 0 || position.Y < 0)
            {
                return;
            }

        }


        public string GetTitle() => WindowContent.FrameTitle;

        /// <summary>
        /// Resize the contents of the window, the window border will always be newsize x/y + 2.
        /// </summary>
        /// <param name="newSize"></param>
        public void ResizeWindow(Vector.VectorI2 newSize)
        {
            if (newSize == null)
            {
                ConsoleInstance.LogClient.Write("Window attempted to be resized to a null length/height.");
                throw new NullReferenceException("The window size cannot be null.");
            }
            if (newSize.X < MinSize.X || newSize.Y < MinSize.Y)
            {
                ConsoleInstance.LogClient.Write("Attempted resize is to a size smaller than minimum.");
                throw new InvalidOperationException("Cannot resize the window to lower than it's minimum size.");
            }
            Size.X = newSize.X + 2;
            Size.Y = newSize.Y + 2;
            WindowContent.Size.X = newSize.X;
            WindowContent.Size.Y = newSize.Y;
        }

        private void MakeWindow(ref char[,] buffer)
        {
            if (buffer == null)
                return;
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                buffer[i, 0] = '║';
                buffer[i, buffer.GetLength(1) - 1] = '║';
            }
            for (int i = 0; i < buffer.GetLength(1); i++)
            {
                buffer[0, i] = '═';
                buffer[buffer.GetLength(0) - 1, i] = '═';
            }
        }

        public void AddObject(FrameObject objectToAdd)
        {
            if (objectToAdd == null)
                throw new NullReferenceException("Cannot add a null windowobject to a window.");
            WindowContent.AddObject(objectToAdd);
        }

        private void MakeFrame(ref char[,] buffer)
        {
            MakeWindow(ref buffer);
            buffer[0, 0] = '╔';
            buffer[0, Size.X - 1] = '╗';
            buffer[Size.Y - 1, 0] = '╚';
            buffer[Size.Y - 1, Size.X - 1] = '╝';

        }

        public char[,] MakeBuffer()
        {
            if (Size.Y > ConsoleInstance.ConsoleHeight)
            {
                Size.Y = ConsoleInstance.ConsoleHeight;
                ConsoleInstance.LogClient.Write("Error: Window Y Too Large");
            }

            if (Size.X > ConsoleInstance.ConsoleWidth)
            {
                Size.X = ConsoleInstance.ConsoleWidth;
                ConsoleInstance.LogClient.Write("Error: Window X Too Large");
            }

            //Else used to perform one less check when size is not too small and not too large.

            if (WindowContent.Size.X < MinSize.X || WindowContent.Size.Y < MinSize.Y)
            {
                ResizeWindow(MinSize);
                ConsoleInstance.LogClient.Write("Error: Window too small.");
            }
            else
            {
                if (WindowContent.Size.X > MaxSize.X || WindowContent.Size.Y > MaxSize.Y)
                {
                    ResizeWindow(MaxSize);
                    ConsoleInstance.LogClient.Write("Error: Window too large.");
                }
            }

            char[,] buffer = new char[0, 0];

            Compositor.ResetBuffer(ref buffer, ' ', new Vector.VectorI2(Size.X, Size.Y));

            MakeFrame(ref buffer);

            char[,] content = WindowContent.Composite();

            if (content == null)
                content = new char[0, 0];

            if (content.GetLength(0) > buffer.GetLength(0) || content.GetLength(1) > content.GetLength(1))
                throw new IndexOutOfRangeException("Window content cannot expand outwith the window size.");

            Compositor.Transpose(ref buffer, content, new Vector.VectorI2(1, 1));

            return buffer;
        }

        public CharInfo[] GetBuffer()
        {
            CharInfo[] buffer = new CharInfo[10 * 10];
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                buffer[i] = new CharInfo();
                buffer[i].Char.UnicodeChar = '#';
                buffer[i].Atrributes = (int)Buffer.BufferAttributes.ForegroundWhite;
            }

            return buffer; //buffer;
        }
    }
}