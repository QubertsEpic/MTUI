using System;
using System.Collections.Generic;
using static MTUI.ConsoleInstance;

namespace MTUI.Classes
{
    public static class Compositor
    {
        public static Vector.VectorI2 GetBufferSize(object[,] buffer) => new Vector.VectorI2(buffer.GetLength(1), buffer.GetLength(0));

        public static Vector.VectorI2 GetBufferSize(ConsoleInstance.CharInfo[,] buffer) => new Vector.VectorI2(buffer.GetLength(1), buffer.GetLength(0));

        public static bool CheckNull(List<Object> objects)
        {
            for (int i = 0; i < objects.Count; i++)
                if (objects[i] == null)
                    return true;
            return false;
        }

        public static bool Transpose(object[] master, Vector.VectorI2 masterSize, object toTranspose, Vector.VectorI2 toTransposeSize, Vector.VectorI2 offset)
        {
            if (CheckNull(new List<object>()
            {
                toTranspose
            }))
            {
                throw new Exception("Cannot transpose to or from a null array");

            }

            for (int i = 0; i < toTransposeSize.X; i++)
            {
                for (int j = 0; j < toTransposeSize.Y; j++)
                {
                    if (offset.Y + 1 + i > master.GetLength(0) || offset.X + 1 + j > master.GetLength(1) || offset.Y + i < 0 || offset.X + j < 0)
                        continue;

                    ChangeValue(new Vector.VectorI2(i + offset.X, j + offset.Y), master, in toTranspose, new Vector.VectorI2(i, j));
                }
            }
            return true;
        }

        public static object FindValue(Vector.VectorI2 location, in object array)
        {
            if (location == null || array == null)
                throw new NullReferenceException("Cannot access null array.");
            if (array is Object[] newSingleArray)
            {
                return newSingleArray[location.X];
            }
            if (array is Object[,] newMultiArray)
            {
                return newMultiArray[location.X, location.Y];
            }
            return null;
        }

        public static bool ChangeValue(Vector.VectorI2 masterLocation, in object array, in object transposeArray, Vector.VectorI2 transposeLocation)
        {
            if (masterLocation == null || array == null || transposeArray == null || transposeLocation == null)
                throw new NullReferenceException("Cannot change value when parameters are null.");
            if (array is object[] singleDimenArray)
            {
                singleDimenArray[masterLocation.X] = FindValue(new Vector.VectorI2(transposeLocation.X), in transposeArray);
                return true;
            }
            if (array is object[,] multiDimenArray)
            {
                multiDimenArray[masterLocation.X, masterLocation.Y] = FindValue(new Vector.VectorI2(transposeLocation.X, transposeLocation.Y), in transposeArray);
                return true;
            }

            return false;
        }

        //For reference.
        public static void Transpose(ref char[,] master, char[,] toTranspose, Vector.VectorI2 offset)
        {
            if (CheckNull(new List<object>()
            {
                master, toTranspose
            }))
            {
                throw new Exception("Cannot transpose to or from a null array");
            }
            for (int i = 0; i < toTranspose.GetLength(0); i++)
            {
                for (int j = 0; j < toTranspose.GetLength(1); j++)
                {
                    if (offset.Y + 1 + i > master.GetLength(0) || offset.X + 1 + j > master.GetLength(1) || offset.Y + i < 0 || offset.X + j < 0)
                        continue;
                    master[i + offset.Y, j + offset.X] = toTranspose[i, j];
                }
            }

        }



        public static void WriteStringToBuffer(ref char[,] buffer, string stringToWrite, Vector.VectorI2 Offset, bool exception = true)
        {
            if (string.IsNullOrWhiteSpace(stringToWrite) || Offset == null || buffer == null)
                throw new NullReferenceException("Cannot write string to buffer if Buffer, StringToWrite, or Offset it null.");
            if (Offset.X > buffer.GetLength(1) || Offset.Y > buffer.GetLength(0))
            {
                if (exception)
                {
                    throw new IndexOutOfRangeException("Cannot write string to buffer if starting position is beyond buffer.");
                }
                else
                {
                    return;
                }
            }
            for (int i = 0; i < stringToWrite.Length; i++)
            {
                if (i + 1 + Offset.X > buffer.GetLength(1))
                    break;
                buffer[Offset.Y, Offset.X + i] = stringToWrite[i];
            }

        }

        public static char[,] CreateBuffer(char setTo, Vector.VectorI2 Size)
        {
            char[,] buffer = new char[Size.Y, Size.X];
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    buffer[i, j] = setTo;
                }
            }
            return buffer;
        }

        public static void ResetBuffer(ref char[,] buffer, char resetTo, Vector.VectorI2 Size)
        {
            buffer = new char[Size.Y, Size.X];
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    buffer[i, j] = resetTo;
                }
            }
        }
    }
}
