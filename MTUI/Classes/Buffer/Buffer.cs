using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.Buffer
{
    public class Buffer<T>
    {
        public T[] bufferArray;
        private Vector.Vector<int> size;
        public Buffer()
        {
            size = new Vector.Vector<int>(0,0);
            bufferArray = new T[0];
        }

        public Buffer(Vector.Vector<int> bufferSize, T[] buffer)
        {
            bufferArray = buffer;
            if(bufferArray == null)
            {
                throw new NullReferenceException("Cannot have empty buffer!");
            }
            size = bufferSize;
        }

        public Buffer(Vector.Vector<int> bufferSize, T standardValue)
        {
            bufferArray = BufferOperations.CreateBuffer((UInt32) (bufferSize.Y*bufferSize.X), standardValue);
            if(bufferArray == null)
            {
                throw new NullReferenceException("Buffer creation returned null.");
            }
            size = bufferSize;
        }

        public int ConvertTo1D(int x, int y) => (size.X * y) + x;    

        public T this[int index]
        {
            get { return bufferArray[index]; }
            set { bufferArray[index] = value; }
        }
        public T this[int x, int y]
        {
            get { return bufferArray[(size.X * y) + x]; }
            set { bufferArray[(size.X * y) + x] = value; }
        }
        public void SetValue(int x, T value) => bufferArray[x] = value;
        public void SetValue(int x, int y, T value) => bufferArray[(size.X * y) + x] = value;
        public T GetValue(int x) => bufferArray[x];
        public T GetValue(int x, int y) => bufferArray[(y * size.X) + x];

        public T[] GetBuffer() => bufferArray;
        public int GetLength(int dimension)
        {
            switch (dimension)
            {
                case 0:
                    return size.X;
                case 1:
                    return size.Y;
            }
            throw new IndexOutOfRangeException("Dimension doesn't exist.");
        }
        public Vector.Vector<int> GetSize() => size;

        public static bool CheckNull(List<object> objects)
        {
            if (objects == null)
                return false;
            for(int i = 0; i < objects.Count; i++)
            {
                if (objects[i] == null)
                    return true;
            }
            return false;
        }

        public bool Transpose(Buffer<T> toTranspose, Vector.Vector<int> offset)
        {
            if(CheckNull(new List<object>()
            {
                bufferArray, toTranspose
            }))
            {
                throw new NullReferenceException("Cannot transpose when null values are present.");
            }
            for(int i = 0; i < toTranspose.GetSize().X; i++)
            {
                for(int j = 0; j < toTranspose.GetSize().Y; j++)
                {
                    SetValue(i + offset.X, j + offset.Y, toTranspose.GetValue(i, j));
                }
            }
            return true;
        }
    }
}
