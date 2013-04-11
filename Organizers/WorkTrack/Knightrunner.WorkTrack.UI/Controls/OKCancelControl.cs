using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Knightrunner.WorkTrack.UI.Controls
{
    public partial class OKCancelControl : UserControl
    {
        public OKCancelControl()
        {
            InitializeComponent();
        }

        public Button ButtonOK
        {
            get { return buttonOK; }
        }

        public Button ButtonCancel
        {
            get { return buttonCancel; }
        }


        public event EventHandler ButtonOKClick
        {
            add { buttonOK.Click += value; }
            remove { buttonOK.Click -= value; }

        }

        public event EventHandler ButtonCancelClick
        {
            add { buttonCancel.Click += value; }
            remove { buttonCancel.Click -= value; }

        }


    }
}
