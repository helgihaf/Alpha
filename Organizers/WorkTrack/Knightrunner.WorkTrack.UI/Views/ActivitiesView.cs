using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.WorkTrack.Database;

namespace Knightrunner.WorkTrack.UI.Views
{
    public partial class ActivitiesView : DataGridBaseView
    {

        public ActivitiesView()
        {
            InitializeComponent();
            InitializeColumns();
            Controller.PerformingBindings += new EventHandler(Controller_PerformingBindings);
        }

        private void InitializeColumns()
        {
            Controller.AddComboBoxColumn("Project", "Project", 120, this.projectBindingSource, "Name", "Id");
            Controller.AddTextColumn("Name", "Name", 100);
            Controller.AddTextColumn("Description", "Description", 140);
            Controller.AddCheckboxColumn("Active", "Active", 80);
            Controller.AddTextColumn("ExternalCode", "External code", 100);
        }


        private void Controller_PerformingBindings(object sender, EventArgs e)
        {
            projectBindingSource.DataSource = Controller.DataContext.Projects;
            bindingSource.DataSource = Controller.DataContext.Activities;
        }

    }
}
