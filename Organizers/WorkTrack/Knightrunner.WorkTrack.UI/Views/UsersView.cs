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
    public partial class UsersView : DataGridBaseView
    {
        public UsersView()
        {
            InitializeComponent();
            InitializeColumns();
            Controller.PerformingBindings += new EventHandler(Controller_PerformingBindings);
        }

        private void InitializeColumns()
        {
            Controller.AddTextColumn("Username", "Username", 100);
            Controller.AddTextColumn("FullName", "Full name", 140);
            Controller.AddTextColumn("WindowsAccount", "Windows account", 100);
            Controller.AddCheckboxColumn("Active", "Active", 80);
        }


        private void Controller_PerformingBindings(object sender, EventArgs e)
        {
            bindingSource.DataSource = Controller.DataContext.Users;
        }
    }
}
