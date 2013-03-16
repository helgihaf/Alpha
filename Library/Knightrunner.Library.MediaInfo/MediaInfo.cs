using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Knightrunner.Library.MediaInfo
{
    public class MediaInfo
    {
        private bool propertiesRead;

        private DateTime? takenTime;
        private long fileSize;
        private DateTime creationTime;
        private DateTime modifiedTime;
        private string cameraMake;

        public MediaInfo(string filePath)
        {
            this.FilePath = filePath;
        }

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


        private void AssertProperties()
        {
            if (!propertiesRead)
            {
                SetFileProperties();
                var adapter = MediaInfoAdapterFactory.GetAdapter(this.FilePath);
                adapter.Populate(this);
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

    }
}
