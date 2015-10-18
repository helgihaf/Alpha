using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marson.Compare.Core
{
    internal class DateTimeFileComparer : IFileComparer
    {
        public CompareStatus Compare(string filePathLeft, string filePathRight)
        {
            var leftInfo = File.GetLastWriteTimeUtc(filePathLeft);
            var rightInfo = File.GetLastWriteTimeUtc(filePathRight);
            CompareStatus status;
            if (leftInfo < rightInfo)
            {
                status = CompareStatus.RightNewer;
            }
            else if (leftInfo > rightInfo)
            {
                status = CompareStatus.LeftNewer;
            }
            else
            {
                status = CompareStatus.Equal;
            }
            return status;
        }
    }
}
