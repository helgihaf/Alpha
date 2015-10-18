using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace Marson.LogEyes
{
    public class LogFile2 : IDisposable
    {
        private readonly MemoryMappedFile mappedFile;

        public LogFile2(string filePath)
        {
            this.FilePath = filePath;
            var fileInfo = new FileInfo(filePath);
            FileLength = fileInfo.Length;
            mappedFile = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open);
        }

        internal MemoryMappedFile MemoryMappedFile
        {
            get { return mappedFile; }
        }

        public string FilePath { get; private set; }
        public long FileLength { get; private set; }
        public Encoding Encoding { get; internal set; } = Encoding.ASCII;

        //public View CreatView()
        //{
        //    return new View(this);
        //}

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    mappedFile.Dispose();
                }

                disposedValue = true;
            }
        }

 
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}