using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Knightrunner.WorkTrack.UI.Views
{
    public partial class LoadingView : UserControl
    {
        public LoadingView()
        {
            InitializeComponent();

            var version =
                this.GetType().Assembly.GetName().Version;
            labelVersion.Text = version.ToString();
        }


        public string Status
        {
            get { return labelStatus.Text; }
            set { labelStatus.Text = value; }
        }
    }
}
