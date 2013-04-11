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
    public partial class DataGridBaseView : UserControl
    {
        public DataGridController Controller { get; private set; }

        public DataGridBaseView()
        {
            InitializeComponent();
            Controller = new DataGridController(dataGridView, bindingSource, buttonSave, buttonCancel);
        }


        public void LoadData(ISessionContext sessionContext)
        {
            Controller.SessionContext = sessionContext;
            Controller.RefreshData();
        }

        public virtual void Close()
        {
            if (Controller != null)
            {
                Controller.Dispose();
                Controller = null;
            }
        }


    }
}
