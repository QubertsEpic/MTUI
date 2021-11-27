using MTUI.Classes.Buffer;
using MTUI.Classes.Exceptions;
using MTUI.Interfaces;
using System;
using MTUI.Classes.Data.P_invoke;
using System.Collections.Generic;

namespace MTUI.Classes
{
    public class TWindow
    {
        public TWindowContent WindowContent;
        public Vector.Vector<int> Location, Size, MinSize, MaxSize, CursorPosition;
        public int Layer;
        public string FrameText;
        public bool Visible = true, ChangesMade;
        public Buffer<CharInfo>  cachedBuffer;
        /// <summary>
        /// Any X plane size will be doubled.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="minSize"></param>
        public TWindow(Vector.Vector<int> location, Vector.Vector<int> size = null, Vector.Vector<int> minSize = null, Vector.Vector<int> maxSize = null)
        {

            size = size ?? new Vector.Vector<int>(4, 4);
            size.X = size.X * 2;
            WindowContent = new TWindowContent(size);
            Location = location ?? throw new Exception("Location cannot be null");
            Size = new Vector.Vector<int>(WindowContent.Size.X + 2, WindowContent.Size.Y + 2);
            MinSize = new Vector.Vector<int>(2, 1);
            MaxSize = new Vector.Vector<int>(40, 20);
            CursorPosition = new Vector.Vector<int>(-1, -1);
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


        public void SetCursorPosition(Vector.Vector<int> position)
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
        public void ResizeWindow(Vector.Vector<int> newSize)
        {
            if (newSize == null)
            {
                throw new NullReferenceException("The window size cannot be null.");
            }
            if (newSize.X < MinSize.X || newSize.Y < MinSize.Y)
            {
                throw new InvalidOperationException("Cannot resize the window to lower than it's minimum size.");
            }
            Size.X = newSize.X + 2;
            Size.Y = newSize.Y + 2;
            WindowContent.Size.X = newSize.X;
            WindowContent.Size.Y = newSize.Y;
        }

        private void MakeWindow(ref Buffer<CharInfo> buffer)
        {
            if (buffer == null)
                return;
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                buffer.bufferArray[buffer.ConvertTo1D(i, 0)].Char.UnicodeChar = '═';
                buffer.bufferArray[buffer.ConvertTo1D(i, buffer.GetLength(1) - 1)].Char.UnicodeChar = '═';
            }
            for (int i = 0; i < buffer.GetLength(1); i++)
            {
                buffer.bufferArray[buffer.ConvertTo1D(0, i)].Char.UnicodeChar = '║';
                buffer.bufferArray[buffer.ConvertTo1D(buffer.GetLength(0) - 1, i)].Char.UnicodeChar = '║';
            }
        }

        public void AddObject(FrameObject objectToAdd)
        {
            if (objectToAdd == null)
                throw new NullReferenceException("Cannot add a null windowobject to a window.");
            WindowContent.AddObject(objectToAdd);
        }

        private void MakeFrame(ref Buffer<CharInfo> buffer)
        {
            MakeWindow(ref buffer);
            buffer.bufferArray[0].Char.UnicodeChar = '╔';
            buffer.bufferArray[buffer.ConvertTo1D(Size.X-1, 0)].Char.UnicodeChar = '╗';
            buffer.bufferArray[buffer.ConvertTo1D(0, Size.Y-1)].Char.UnicodeChar = '╚';
            buffer.bufferArray[buffer.ConvertTo1D(Size.X - 1, Size.Y - 1)].Char.UnicodeChar = '╝';

        }

        public Buffer<CharInfo> MakeBuffer()
        {
            if (WindowContent.Size.X < MinSize.X || WindowContent.Size.Y < MinSize.Y)
            {
                ResizeWindow(MinSize);
            }
            else
            {
                if (WindowContent.Size.X > MaxSize.X || WindowContent.Size.Y > MaxSize.Y)
                {
                    ResizeWindow(MaxSize);
                }
            }

            Buffer<CharInfo> buffer = new Buffer<CharInfo>(Size, new CharInfo() { Atrributes = (int)BufferAttributes.ForegroundWhite, Char = new CharUnion() { UnicodeChar = ' ' } });

            MakeFrame(ref buffer);

            Buffer<CharInfo> content = WindowContent.Composite();

            if (content == null)
                content = new Buffer<CharInfo>(new Vector.Vector<int>(0,0), new CharInfo());

            if (content.GetLength(0) > buffer.GetLength(0) || content.GetLength(1) > content.GetLength(1))
                throw new IndexOutOfRangeException("Window content cannot expand outwith the window size.");

            buffer.Transpose(content, new Vector.Vector<int>(1, 1));

            return buffer;
        }

        public Buffer<CharInfo> GetBuffer()
        {
            if (!ChangesMade)
                return cachedBuffer;
            return (cachedBuffer =  MakeBuffer());
        }
    }
}