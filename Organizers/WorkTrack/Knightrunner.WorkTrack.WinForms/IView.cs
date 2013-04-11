using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.WorkTrack.Database;

namespace Knightrunner.WorkTrack.WinForms
{
    public interface IView : IDisposable
    {
        ISessionContext SessionContext { get; set; }
        void LoadData();
    }
}
