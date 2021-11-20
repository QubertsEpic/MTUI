using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MTUI.ConsoleInstance;

namespace MTUI.Classes.Buffer
{
    public static class BufferOperations
    {
        public static T[] CreateBuffer<T>(UInt32 Size, T standardValue)
        {
            T[] newBuffer = new T[Size];
            if(!ResetBuffer(ref newBuffer, standardValue))
            {
                throw new Exception("Failed to create new buffer.");
            }
            return newBuffer;
        }

        public static bool ResetBuffer<T>(ref T[] buffer, T standardValue)
        {
            try
            {
                if (buffer == null)
                    return false;
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = standardValue;
                }
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
    }
}
