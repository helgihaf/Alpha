using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.WorkTrack.Database
{
    public interface ISessionContext
    {
        Guid UserId { get; }
        WorkTrackDataContext CreateDataContext();
    }
}
