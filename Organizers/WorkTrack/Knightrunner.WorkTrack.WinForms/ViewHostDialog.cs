using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Knightrunner.WorkTrack.WinForms
{
    public partial class ViewHostDialog : Form
    {
        public IView view;

        public ViewHostDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Clock;
        }


        public IView View
        {
            get { return view; }
            set
            {
                if (object.ReferenceEquals(view, value))
                {
                    return;
                }

                if (view != null)
                {
                    Controls.Remove(view as Control);
                    view.Dispose();
                }
                view = value;
                if (view != null)
                {
                    var control = view as Control;
                    control.Dock = DockStyle.Fill;
                    Controls.Add(control);
                    control.BringToFront();
                }
            }
        }

        private void ViewHostDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }
    }
}
