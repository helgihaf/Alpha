using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Knightrunner.WorkTrack.UI.Dialogs
{
    public partial class ViewHostDialog : Form
    {
        public ViewHostDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Clock;
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
