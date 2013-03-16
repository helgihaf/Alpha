using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.MediaInfo.Images
{
    public class ImageInfoAdapter : IMediaInfoAdapter
    {
        private static string[] imageExtensions = new string[]
		{
            ".bmp",
			".jpg",
			".png",
			".raw",
            ".tif",
		};


        public static bool IsImageExtension(string extension)
        {
            for (int i = 0; i < imageExtensions.Length; i++)
            {
                if (StringComparer.InvariantCultureIgnoreCase.Compare(extension, imageExtensions[i]) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void Populate(MediaInfo mediaInfo)
        {
            throw new NotImplementedException();
        }
    }
}
