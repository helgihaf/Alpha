using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Controls.PageSelection
{
    public interface IDetailPageRepository
    {
        IDetailPage GetPageOf(IDirector director, object dataObject);
    }
}
