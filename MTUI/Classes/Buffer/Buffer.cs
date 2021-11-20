using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MTUI.ConsoleInstance;

namespace MTUI.Classes.Buffer
{
    public class Buffer<T>
    {
        private T[] buffer;
        private Vector.VectorI2 size;
        public Buffer(Vector.VectorI2 bufferSize, T standardValue)
        {
            buffer = BufferOperations.CreateBuffer((UInt32) (bufferSize.Y*bufferSize.Y), standardValue);
            if(buffer == null)
            {
                throw new NullReferenceException("Buffer creation returned null.");
            }
        }

        public bool Transpose(T[] toTranspose, Vector.VectorI2 transposeBufferSize, Vector.VectorI2 offset)
        {
            if(Compositor.CheckNull(new List<object>()
            {
                buffer, size, toTranspose, transposeBufferSize
            }))
            {
                throw new NullReferenceException("Cannot transpose when null values are present.");
            }
            for(int i = 0; i < transposeBufferSize.X; i++)
            {
                for(int j = 0; j < transposeBufferSize.Y; j++)
                {
                    buffer[] = toTranspose[(transposeBufferSize.X * i) - j];
                }
            }
        }
    }
}
