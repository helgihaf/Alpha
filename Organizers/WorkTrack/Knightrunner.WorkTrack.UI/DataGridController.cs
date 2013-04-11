using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.WorkTrack.Database;
using System.Windows.Forms;
using System.ComponentModel;

namespace Knightrunner.WorkTrack.UI
{
    public class DataGridController : IDisposable
    {
        private bool disposed;

        private WorkTrackDataContext dataContext;
        private bool enableTracking;

        private DataGridView dataGridView;
        private BindingSource bindingSource;
        private ToolStripButton buttonSave;
        private ToolStripButton buttonCancel;

        private Control ultimateParent;

        public DataGridController(DataGridView dataGridView, BindingSource bindingSource, ToolStripButton buttonSave, ToolStripButton buttonCancel)
        {
            this.dataGridView = dataGridView;
            this.bindingSource = bindingSource;
            
            this.buttonSave = buttonSave;
            if (this.buttonSave != null)
            {
                this.buttonSave.Click += new EventHandler(buttonSave_Click);
            }

            this.buttonCancel = buttonCancel;
            if (this.buttonCancel != null)
            {
                this.buttonCancel.Click += new EventHandler(buttonCancel_Click);
            }

            if (this.bindingSource != null)
            {
                this.bindingSource.ListChanged += new ListChangedEventHandler(bindingSource_ListChanged);
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (dataContext != null)
                {
                    dataContext.SubmitChanges();
                    dataContext.Dispose();
                    dataContext = null;
                }
                buttonSave.Click -= new EventHandler(buttonSave_Click);
                buttonCancel.Click -= new EventHandler(buttonCancel_Click);
                bindingSource.ListChanged -= new ListChangedEventHandler(bindingSource_ListChanged);
            }
            
            disposed = true;
        }



        public ISessionContext SessionContext { get; set; }

        public WorkTrackDataContext DataContext
        {
            get { return dataContext; }
        }

        public DataGridView DataGridView
        {
            get { return dataGridView; }
        }

        public BindingSource BindingSource
        {
            get { return bindingSource; }
        }

        public ToolStripButton ButtonSave
        {
            get { return buttonSave; }
        }

        public ToolStripButton ButtonCancel
        {
            get { return buttonCancel; }
        }

        public void SaveChanges()
        {
            dataContext.SubmitChanges();
            UpdateActions();
        }


        public void RefreshData()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
                dataContext = null;
            }
            dataContext = SessionContext.CreateDataContext();
            enableTracking = false;
            OnPerformBindings();
            UpdateActions();
            enableTracking = true;
        }


        public event EventHandler PerformingBindings;
        
        private void OnPerformBindings()
        {
            if (PerformingBindings != null)
            {
                PerformingBindings(this, EventArgs.Empty);
            }
        }



        public DataGridViewTextBoxColumn AddTextColumn(string dataPropertyName, string columnHeader, int width)
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

        public DataGridViewCheckBoxColumn AddCheckboxColumn(string dataPropertyName, string columnHeader, int width)
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

        public DataGridViewComboBoxColumn AddComboBoxColumn(string dataPropertyName, string columnHeader, int width, object itemsDataSource, string displayMember, string valueMember)
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

        public event EventHandler<ItemEventArgs> ItemAdded;
        public event EventHandler<ItemEventArgs> ItemDeleted;


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


        private void OnItemAdded(object item)
        {
            if (ItemAdded != null)
            {
                ItemAdded(this, new ItemEventArgs { Item = item });
            }
        }

        private void OnItemDeleted(object item)
        {
            if (ItemDeleted != null)
            {
                ItemDeleted(this, new ItemEventArgs { Item = item });
            }
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

        private Control GetUltimateParent()
        {
            if (ultimateParent == null)
            {
                ultimateParent = dataGridView.Parent;
                while (!(ultimateParent.Parent is Form))
                {
                    ultimateParent = ultimateParent.Parent;
                }
            }

            return ultimateParent;
        }

        private bool ConfirmMessage(string msg, string caption)
        {
            return
                MessageBox.Show(GetUltimateParent(), msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

    }


    public class ItemEventArgs : EventArgs
    {
        public object Item { get; set; }
    }
}
