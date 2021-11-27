using MTUI.Classes;
using MTUI.Classes.FrameObjects;
using MTUI.Classes.Vector;

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
            TInstance instance = new TInstance();
            TWindow frame = new TWindow(new Vector<int>(1, 1), size: new Classes.Vector.Vector<int>(6, 6));
            TWindow frame1 = new TWindow(new Vector<int>(1, 20), size: new Classes.Vector.Vector<int>(6, 6));
            TWindow frame2 = new TWindow(new Vector<int>(20, 1), size: new Classes.Vector.Vector<int>(6, 6));
            TWindow frame3 = new TWindow(new Vector<int>(20, 20), size: new Classes.Vector.Vector<int>(6, 6));

            frame.AddObject(new TLabel("Collider: 0", new Vector<int>(0, 0)));
            frame1.AddObject(new TLabel("Collider: 1", new Vector<int>(0, 0)));
            frame2.AddObject(new TLabel("Collider: 2", new Vector<int>(0, 0)));
            frame3.AddObject(new TLabel("Collider: 3", new Vector<int>(0, 0)));

            instance.Frames.Add(frame);
            instance.Frames.Add(frame1);
            instance.Frames.Add(frame2);
            instance.Frames.Add(frame3);

            instance.Init();
        }
    }
}
