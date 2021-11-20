using Microsoft.Win32.SafeHandles;
using MTUI.Classes;
using MTUI.Classes.FrameObjects;
using MTUI.Classes.Nums;
using MTUI.Classes.Vector;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace MTUI
{
    class program
    {

        public static void Main(string[] argss)
        {
            /*TInstance instance = new TInstance();
            TWindow frame = new TWindow(new VectorI2(10,10), size: new Classes.Vector.VectorI2(6,6));
            frame.AddObject(new TButton("Test", new VectorI2(1,1)));
            frame.AddObject(new TLabel("Test2", new VectorI2(1, 3)));
            TWindow frame2 = new TWindow(new Classes.Vector.VectorI2(10, 20), size: new Classes.Vector.VectorI2(6,6), maxSize: new Classes.Vector.VectorI2(30,15));
            TWindow frame3 = new TWindow(new Classes.Vector.VectorI2(20, 10), size: new Classes.Vector.VectorI2(10, 10), minSize: new Classes.Vector.VectorI2(6, 6), maxSize: new Classes.Vector.VectorI2(40,20));
            instance.Frames.Add(frame);
            instance.Frames.Add(frame2);
            instance.AddFrame(frame3);
            instance.Init();*/
            while (true)
            {
                for (int i = 0; i < ConsoleInstance.DrawBuffer.Length; i++)
                {
                    ConsoleInstance.DrawBuffer[i].Char.UnicodeChar = ' ';
                    ConsoleInstance.SwapBuffers();

                }

            }
        }
    }
}
