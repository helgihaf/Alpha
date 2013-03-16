using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace Knightrunner.Library.Images
{
    public class ImageInfo
    {
        private bool propertiesRead;

        private DateTime? takenTime;
        private long fileSize;
        private DateTime creationTime;
        private DateTime modifiedTime;
        private string cameraMake;

        public string FilePath { get; private set; }

        public DateTime? TakenTime
        {
            get
            {
                AssertProperties();
                return takenTime;
            }
        }

        public long FileSize 
        {
            get
            {
                AssertProperties();
                return fileSize;
            }
        }

        public DateTime CreationTime 
        {
            get
            {
                AssertProperties();
                return creationTime;
            }
        }

        public DateTime ModifiedTime 
        {
            get
            {
                AssertProperties();
                return modifiedTime;
            }
        }

        public string CameraMake
        {
            get
            {
                AssertProperties();
                return cameraMake;
            }
        }

        public byte[] ImageHash { get; private set; }


        public ImageInfo(string filePath)
        {
            this.FilePath = filePath;
        }

        public void CalculateHash()
        {
            using (var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                using (var fileStream = new FileStream(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    ImageHash = md5.ComputeHash(fileStream);
                }
            }
        }


        private void AssertProperties()
        {
            if (!propertiesRead)
            {
                SetFileProperties();
                SetImageProperties();
                propertiesRead = true;
            }
        }

        private void SetFileProperties()
        {
            FileInfo fileInfo = new FileInfo(FilePath);
            if (!fileInfo.Exists)
                throw new FileNotFoundException(FilePath);
            fileSize = fileInfo.Length;
            creationTime = fileInfo.CreationTime;
            modifiedTime = fileInfo.LastWriteTime;
        }

        private void SetImageProperties()
        {
            using (Image image = Image.FromFile(this.FilePath))
            {
                this.takenTime = null;

                // First try the ExifDTOrig: The date and time when the original image data was generated.
                string takenTime = GetImagePropertyValue(image, PropertyTagId.ExifDTOrig) as string;
                if (takenTime != null)
                {
                    this.takenTime = ParseCameraDateTimeProperty(takenTime);
                }
                else
                {
                    // Then try the DateTime property: The date and time of image creation. 
                    takenTime = GetImagePropertyValue(image, PropertyTagId.DateTime) as string;
                    if (takenTime != null)
                    {
                        this.takenTime = ParseCameraDateTimeProperty(takenTime);
                    }
                }

                this.cameraMake = GetImagePropertyValue(image, PropertyTagId.Camera_Make) as string;
            }
        }

        public static object GetImagePropertyValue(Image image, PropertyTagId tagId)
        {
            object result = null;

            PropertyItem propertyItem = null;
            try
            {
                propertyItem = image.GetPropertyItem((int)tagId);
            }
            catch (ArgumentException)
            {
            }

            if (propertyItem != null)
            {
                return GetImagePropertyValue(propertyItem);
            }

            return result;
        }

        public static object GetImagePropertyValue(PropertyItem property)
        {
            object propValue = null;
            try
            {
                switch ((PropertyTagType)property.Type)
                {
                    case PropertyTagType.ASCII:
                        ASCIIEncoding encoding = new ASCIIEncoding();
                        propValue = encoding.GetString(property.Value, 0, property.Len - 1);
                        break;
                    case PropertyTagType.Int16:
                        propValue = BitConverter.ToInt16(property.Value, 0);
                        break;
                    case PropertyTagType.SLONG:
                    case PropertyTagType.Int32:
                        propValue = BitConverter.ToInt32(property.Value, 0);
                        break;
                    case PropertyTagType.SRational:
                    case PropertyTagType.Rational:
                        UInt32 numberator = BitConverter.ToUInt32(property.Value, 0);
                        UInt32 denominator = BitConverter.ToUInt32(property.Value, 4);

                        if (denominator != 0)
                            propValue = ((double)numberator / (double)denominator).ToString();
                        else
                            propValue = "0";

                        if (propValue.ToString() == "NaN")
                            propValue = "0";
                        break;
                    case PropertyTagType.Undefined:
                        propValue = "Undefined Data";
                        break;
                }

            }
            catch (ArgumentException)
            {
            }
            return propValue;
        }


        public static string GetPropertyIdName(int id)
        {
            return ((PropertyTagId)id).ToString();
        }

        private static DateTime? ParseCameraDateTimeProperty(string date)
        {
            DateTime dateTimeResult;
            if (DateTime.TryParseExact(date, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeResult))
                return dateTimeResult;
            else
                return null;
        }



        public DateTime MainDate
        {
            get
            {
                return MainDateTime.Date;
            }
        }




        public DateTime MainDateTime
        {
            get
            {
                if (TakenTime != null)
                    return TakenTime.Value;

                if (CreationTime < ModifiedTime)
                    return CreationTime;
                else
                    return ModifiedTime;
            }
        }
    }

}
