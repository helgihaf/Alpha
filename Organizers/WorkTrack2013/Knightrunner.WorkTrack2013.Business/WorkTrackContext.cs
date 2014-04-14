using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.WorkTrack2013.Business
{
    public class WorkTrackContext
    {
        public Interface.IDataSourceFactory DataSourceFactory { get; set; }

        internal void Validate()
        {
            if (DataSourceFactory == null)
            {
                throw new ValidationException(this.GetType().Name + ".DataSourceFactory cannot be null.");
            }
        }
    }
}
