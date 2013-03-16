using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Knightrunner.Library.Controls.PageSelection
{
    public interface IDirector
    {
        void Initialize(IDetailPageRepository detailPageRepository);

        IDetailPageRepository DetailPageRepository { get; }

        bool ShowObjectPage(object dataObject);
        bool LeaveCurrentPage();
        IDetailPage CurrentPage { get; }

        Control PageParentControl { get; }
    }
}
