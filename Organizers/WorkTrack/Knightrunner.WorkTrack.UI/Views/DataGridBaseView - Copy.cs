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
        protected ISessionContext sessionContext;
        protected WorkTrackDataContext dataContext;
        private bool enableTracking;

        public DataGridBaseView()
        {
            InitializeComponent();
        }


        public void LoadData(ISessionContext sessionContext)
        {
            this.sessionContext = sessionContext;
            RefreshData();
        }

        public void SaveChanges()
        {
            dataContext.SubmitChanges();
            UpdateActions();
        }


        public virtual void Close()
        {
            if (dataContext != null)
            {
                dataContext.SubmitChanges();
                dataContext.Dispose();
                dataContext = null;
            }
        }


        protected void RefreshData()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
                dataContext = null;
            }
            dataContext = sessionContext.CreateDataContext();
            enableTracking = false;
            PerformBindings();
            UpdateActions();
            enableTracking = true;
        }


        protected virtual void PerformBindings()
        {
        }



        protected DataGridViewTextBoxColumn AddTextColumn(string dataPropertyName, string columnHeader, int width)
        {
            var column =
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = dataPropertyName,
                    HeaderText = columnHeader,
                    Width = width,
                };
            dataGridView.Columns.Add(column);

            return column;
        }

        protected DataGridViewCheckBoxColumn AddCheckboxColumn(string dataPropertyName, string columnHeader, int width)
        {
            var column =
                new DataGridViewCheckBoxColumn
                {
                    DataPropertyName = dataPropertyName,
                    HeaderText = columnHeader,
                    Width = width
                };
            dataGridView.Columns.Add(column);

            return column;
        }

        protected DataGridViewComboBoxColumn AddComboBoxColumn(string dataPropertyName, string columnHeader, int width, object itemsDataSource, string displayMember, string valueMember)
        {
            var column =
                new DataGridViewComboBoxColumn
                {
                    DataPropertyName = dataPropertyName,
                    HeaderText = columnHeader,
                    DataSource = itemsDataSource,
                    DisplayMember = displayMember,
                    ValueMember = valueMember,
                    Width = width
                };
            dataGridView.Columns.Add(column);

            return column;
        }


        private void UpdateActions()
        {
            if (dataContext == null)
            {
                return;
            }

            var changeSet = dataContext.GetChangeSet();
            buttonSave.Enabled = changeSet.Deletes.Count > 0 || changeSet.Inserts.Count > 0 || changeSet.Updates.Count > 0;
            buttonCancel.Enabled = buttonSave.Enabled;
        }

        private void bindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (!enableTracking)
            {
                return;
            }

            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                OnItemAdded(bindingSource.CurrencyManager.List[e.NewIndex]);
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (e.OldIndex != -1)
                {
                    OnItemDeleted(bindingSource.CurrencyManager.List[e.OldIndex]);
                }
            }
            UpdateActions();
        }

        protected virtual void OnItemAdded(object item)
        {
        }

        protected virtual void OnItemDeleted(object item)
        {
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (ConfirmMessage("Are you sure you want to cancel all changes in this view?", "Cancel"))
            {
                RefreshData();
            }
        }

        private bool ConfirmMessage(string msg, string caption)
        {
            return
                MessageBox.Show(this, msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}
