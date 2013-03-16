using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Knightrunner.Library.Controls.PageSelection
{
    public partial class DetailPage : UserControl, IDetailPage
    {
        private bool isDataChanged;
        private object dataObject;
        private bool isLoading;

        public DetailPage()
        {
            InitializeComponent();
        }


        public object DataObject
        {
            get { return dataObject; }

            set
            {
                if (object.ReferenceEquals(dataObject, value))
                {
                    return;
                }

                dataObject = value;
                try
                {
                    isLoading = true;
                    LoadDataObject();
                }
                finally
                {
                    isLoading = false;
                }
            }
        }

        protected virtual void LoadDataObject()
        {
        }

        public virtual bool SaveData()
        {
            return true;
        }


        public string Caption { get; protected set; }


        protected virtual void OnDataChanged()
        {
            if (DataChanged != null)
            {
                DataChanged(this, EventArgs.Empty);
            }
        }

        public bool IsDataChanged
        {
            get { return isDataChanged; }
            set
            {
                if (isDataChanged == value)
                {
                    return;
                }

                isDataChanged = value;
                OnDataChanged();
            }
        }

        public event EventHandler<EventArgs> DataChanged;


        protected bool IsLoading
        {
            get { return isLoading; }
        }
    }
}
