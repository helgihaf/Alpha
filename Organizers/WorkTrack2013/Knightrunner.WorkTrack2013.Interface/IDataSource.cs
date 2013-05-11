using Knightrunner.WorkTrack2013.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.WorkTrack2013.Interface
{
    public interface IDataSource : IDisposable
    {
        List<Contract.Project> GetProjects();
    }
}
