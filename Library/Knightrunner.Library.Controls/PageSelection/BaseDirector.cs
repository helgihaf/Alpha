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
    public partial class BaseDirector : UserControl, IDirector
    {
        public BaseDirector()
        {
            InitializeComponent();
        }

        public IDetailPageRepository DetailPageRepository { get; private set; }

        public void Initialize(IDetailPageRepository detailPageRepository)
        {
            this.DetailPageRepository = detailPageRepository;
        }


        public virtual bool LeaveCurrentPage()
        {
            if (CurrentPage != null)
            {
                if (CurrentPage.IsDataChanged)
                {
                    if (!CurrentPage.SaveData())
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        public virtual bool ShowObjectPage(object dataObject)
        {
            if (dataObject == null && CurrentPage == null)
            {
                return true;
            }

            IDetailPage detailPage = DetailPageRepository.GetPageOf(this, dataObject);
            if (CurrentPage != null && !object.ReferenceEquals(CurrentPage, detailPage))
            {
                CurrentPage.Hide();
            }

            CurrentPage = detailPage;
            if (CurrentPage != null)
            {
                CurrentPage.DataObject = dataObject;
                CurrentPage.Show();
            }

            return true;
        }

        public IDetailPage CurrentPage { get; private set; }

        public virtual Control PageParentControl
        {
            get { return this; }
        }
    }
}
