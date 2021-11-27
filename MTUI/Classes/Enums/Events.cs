using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.Enums
{

    public class Events
    {
        public event EventHandler MouseMoved;
        public event EventHandler KeyPressed;
        public event EventHandler MouseButtomPressed;
    }

    public class MouseMoved : EventArgs
    {
        public Vector.Vector<int> NewMousePosition;

        public MouseMoved(Vector.Vector<int> NewPosition) 
        {
            NewMousePosition = NewPosition;
        }
    }

    public enum Character
    {
        ArrowDown,
        ArrowUp,
        ArrowLeft,
        ArrowRight,
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
        K1,
        K2,
        K3,
        K4,
        K5,
        K6,
        K7,
        K8,
        K9,
        K0,
        N1,
        N2,
        N3,
        N4,
        N5,
        N6,
        N7,
        N8,
        N9,
        N0,
        Apostrophe,
        Comma,
        FullStop,
        Hash,
        Equil,
        Dash,
        SemiColon
    }
}
