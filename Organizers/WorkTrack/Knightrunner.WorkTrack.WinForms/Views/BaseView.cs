using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.WorkTrack.Database;

namespace Knightrunner.WorkTrack.WinForms.Views
{
    public partial class BaseView : UserControl, IView
    {
        public BaseView()
        {
            InitializeComponent();
        }


        public ISessionContext SessionContext { get; set; }

        public virtual void LoadData()
        {
        }

    }
}
