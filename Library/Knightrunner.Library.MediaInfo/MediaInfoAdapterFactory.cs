using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Knightrunner.Library.MediaInfo.Images;

namespace Knightrunner.Library.MediaInfo
{
    public static class MediaInfoAdapterFactory
    {


        private static string[] videoExtensions = new string[]
        {
            "??",
        };

        public static IMediaInfoAdapter GetAdapter(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            if (ImageInfoAdapter.IsImageExtension(extension))
            {
                return GetImageAdapter();
            }
            //else if (VideoAdapter.IsVideoExtension(extension))
            //{
            //    return GetVideoAdapter();
            //}
            else
            {
                throw new ArgumentException("Unknown file extension in file " + filePath);
            }
        }

        private static bool IsVideoExtension(string extension)
        {
            throw new NotImplementedException();
        }

        private static IMediaInfoAdapter GetImageAdapter()
        {
            throw new NotImplementedException();
        }

        private static IMediaInfoAdapter GetVideoAdapter()
        {
            throw new NotImplementedException();
        }
    }
}
