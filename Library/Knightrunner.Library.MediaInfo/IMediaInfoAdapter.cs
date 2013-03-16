using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.MediaInfo
{
    public interface IMediaInfoAdapter
    {
        void Populate(MediaInfo mediaInfo);
    }
}
