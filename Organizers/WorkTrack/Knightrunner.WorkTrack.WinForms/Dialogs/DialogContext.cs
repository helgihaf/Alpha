using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.WorkTrack.Database;

namespace Knightrunner.WorkTrack.WinForms.Dialogs
{
    public class DialogContext<T>
    {
        public ISessionContext SessionContext { get; set; }
        public WorkTrackDataContext DataContext { get; set; }
        public T Item { get; set; }
    }
}
