using MTUI.Classes;
using MTUI.Classes.Buffer;
using MTUI.Classes.Vector;
using MTUI.Interfaces;
using System;
using System.Collections.Generic;
using MTUI.Classes.Data.P_invoke;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes
{
    public class TWindowContent
    {
        public Vector<int> Size;
        public ObservableCollection<FrameObject> FrameObjects;
        public string FrameTitle = "Frame";
        public int CurrentlySelectedObject;
        public State CurrentState;
        public TWindowContent(Vector<int> size) 
        {
            Size = size ?? throw new NullReferenceException("Size not found.");
            CurrentState = State.Dorment;
            CurrentlySelectedObject = -1;
        }

        public void ChangeCurrentlySelectedObject(int objectSelected)
        {
            if (objectSelected > FrameObjects.Count || objectSelected < -1)
                return;
            CurrentlySelectedObject = objectSelected;
        }

        public void ChangeState(State state)
        {
            CurrentState = state;
        }

        public void AddObject(FrameObject frameObject)
        {
            if (FrameObjects == null)
            {
                FrameObjects = new ObservableCollection<FrameObject>();
            }
            if (frameObject.Location == null)
            {
                return;
            }
            FrameObjects.Add(frameObject);
        }

        public Buffer<CharInfo> Composite()
        {
            if (FrameObjects == null)
                return new Buffer<CharInfo>();
            if (FrameObjects.Count < 1)
                return new Buffer<CharInfo>();
            Buffer<CharInfo> buffer = new Buffer<CharInfo>(Size, new CharInfo() { Atrributes =(int) BufferAttributes.ForegroundWhite, Char = new CharUnion() { UnicodeChar = ' ' } });
            for (int i = 0; i < FrameObjects.Count; i++)
            {
                if (FrameObjects[i].Location == null)
                    continue;
                Buffer<CharInfo> objectBuffer = FrameObjects[i].Compose();
                if (objectBuffer == null)
                    throw new NullReferenceException("Cannot use null buffer.");
                buffer.Transpose(objectBuffer, FrameObjects[i].Location);
            }
            return buffer;
        }

        public void Allign(ref char[,] buffer, ref char[,] objectBuffer, VectorF2 location)
        {
            if(buffer == null || objectBuffer == null || location == null)
            {
                return;
            }

            if(objectBuffer.GetLength(0) > buffer.GetLength(0) || objectBuffer.GetLength(1) > buffer.GetLength(1))
            {
                return;
            }  
            
            for(int i = 0; i < objectBuffer.GetLength(0); i++)
            {
                for(int j = 0; j < objectBuffer.GetLength(1); j++)
                {
                    if (i + location.Y > buffer.GetLength(0) || j + location.X > buffer.GetLength(1))
                        continue;
                    buffer[(int)location.Y + i, (int)location.X + j] = objectBuffer[i, j];
                }
            }
        }
    }

    public enum State
    {
        Active, Dorment, Closing
    }
    public enum LayoutType
    {
        StackLayout, Grid
    }
    public enum StartingLocation
    {
        Centre, Default
    }
}
