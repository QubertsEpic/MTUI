using MTUI.Classes;
using MTUI.Classes.Vector;
using MTUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes
{
    public class TWindowContent
    {
        public VectorI2 Size;
        public ObservableCollection<FrameObject> FrameObjects;
        public string FrameTitle = "Frame";
        public int CurrentlySelectedObject;
        public State CurrentState;
        public TWindowContent(VectorI2 size) 
        {
            Size = size ?? throw new NullReferenceException("Size not found.");
            if (Size.Y > ConsoleInstance.ConsoleHeight - 2 || Size.X > ConsoleInstance.ConsoleWidth - 2)
                throw new IndexOutOfRangeException("Cannot display window larger than the available space.");
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
                ConsoleInstance.LogClient.Write("Error: Frameobjects not initialised.");
            }
            if (frameObject.Location == null)
            {
                ConsoleInstance.LogClient.Write("Error: FrameObject has no location to place. Infringement of location ruleset upon initialisation.");
                return;
            }
            FrameObjects.Add(frameObject);
        }

        public char[,] Composite()
        {
            if (FrameObjects == null)
                return new char[0, 0];
            if (FrameObjects.Count < 1)
                return new char[0, 0];
            char[,] buffer = new char[0,0];
            Compositor.ResetBuffer(ref buffer, ' ', new VectorI2(Size.X, Size.Y));
            int allocatedSpace = (int)Size.Y / FrameObjects.Count;
            for(int i = 0; i < FrameObjects.Count; i++)
            {
                if (FrameObjects[i].Location == null)
                    continue;
                char[,] objectBuffer = FrameObjects[i].Compose();
                Compositor.Transpose(ref buffer, objectBuffer, FrameObjects[i].Location);
            }
            return buffer;
        }

        public void Allign(ref char[,] buffer, ref char[,] objectBuffer, VectorF2 location)
        {
            if(buffer == null || objectBuffer == null || location == null)
            {
                ConsoleInstance.LogClient.Write("Error: Cannot allign without all parameters. ");
                return;
            }

            if(objectBuffer.GetLength(0) > buffer.GetLength(0) || objectBuffer.GetLength(1) > buffer.GetLength(1))
            {
                ConsoleInstance.LogClient.Write("Error: Object Buffer is larger than Window Buffer. ");
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
