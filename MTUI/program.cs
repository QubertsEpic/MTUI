using MTUI.Classes;
using MTUI.Classes.FrameObjects;
using MTUI.Classes.Vector;
using System.Threading;

namespace MTUI
{
    class program
    {
        /// <summary>
        /// Strictly for testing.
        /// </summary>
        /// <param name="argss"></param>
        public static void Main(string[] argss)
        {
            //TestInstance.
            TInstance instance = new TInstance(60);
            instance.MakeNewFrame(new Vector<int>(0, 0), new Vector<int>(6, 6));
            instance.MakeNewFrame(new Vector<int>(0, 20), new Vector<int>(6, 6));
            instance.MakeNewFrame(new Vector<int>(20, 0), new Vector<int>(6,6));
            instance.MakeNewFrame(new Vector<int>(20, 20), new Vector<int>(6,6));

            instance.Frames[0].AddObject(new TLabel("Collider: 0", new Vector<int>(0, 0)));
            instance.Frames[0].AddObject(new TButton("TestButton", new Vector<int>(0, 3)));
            instance.Frames[1].AddObject(new TLabel("Collider: 1", new Vector<int>(0, 0)));
            instance.Frames[2].AddObject(new TLabel("Collider: 2", new Vector<int>(0, 0)));
            instance.Frames[3].AddObject(new TLabel("Collider: 3", new Vector<int>(0, 0)));

            instance.Init();
            for(int i = 1; i < 100000000; i++)
            {
                instance.FPS =  144%i;
                Thread.Sleep(6);
            }
        }
    }
}
