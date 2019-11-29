using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVL.UI.Views
{
    public interface IView
    {
        EDrawNodeMode DrawNodeMode { get; }

        bool DrawHeight { get; } 

        bool DrawValues { get; }
        
        event EventHandler ModeChanged;

        event EventHandler FullScreenModeClick;

        PictureBox DrawWindow { get; }


        Decimal Value { get; }

        string TraversalList { set; }

        int CountSteps { set; }

        event EventHandler AddEvent;

        event EventHandler FillingAddEvent;

        event EventHandler DeleteEvent;

        event EventHandler FindEvent;

        event EventHandler Traversal;
    }
}
