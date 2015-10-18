using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marson.Compare.Core
{
    public class FileEntry : Entry
    {
        internal override void Compare(IFileComparer fileComparer)
        {
            CompareStatus status;
            if (Left == null)
            {
                status = CompareStatus.RightOrphant;
            }
            else if (Right == null)
            {
                status = CompareStatus.LeftOrphant;
            }
            else
            {
                status = fileComparer.Compare(Left.FullPath, Right.FullPath);
            }

            CompareStatuses.Clear();
            CompareStatuses.Add(status);
        }


    }
}
