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
    public partial class ProjectsView : DataGridBaseView
    {
        public ProjectsView()
        {
            InitializeComponent();
            InitializeColumns();
            Controller.PerformingBindings += new EventHandler(Controller_PerformingBindings);
            Controller.ItemAdded += new EventHandler<ItemEventArgs>(Controller_ItemAdded);
        }

        private void InitializeColumns()
        {
            Controller.AddTextColumn("Name", "Name", 100);
            Controller.AddTextColumn("Description", "Description", 140);
            Controller.AddCheckboxColumn("Active", "Active", 80);
            Controller.AddTextColumn("ExternalCode", "External code", 100);
        }


        private void Controller_PerformingBindings(object sender, EventArgs e)
        {
            bindingSource.DataSource = Controller.DataContext.Projects;
        }


        private void Controller_ItemAdded(object sender, ItemEventArgs e)
        {
            var project = (Project)e.Item;
            project.Id = Guid.NewGuid();
            Controller.DataContext.Projects.InsertOnSubmit(project);
        }
    }
}
