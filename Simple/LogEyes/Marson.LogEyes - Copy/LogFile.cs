using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marson.LogEyes
{
    public class LogFile : IDisposable
    {
        private bool disposedValue = false;
        private string filePath;
        private List<int> lineLengths;
        private readonly MemoryMappedFile mappedFile;
        private long lastOffset;

        public LogFile(string filePath)
        {
            this.filePath = filePath;
            UpdateLineLengths();
            mappedFile = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open);
        }

        public long LengthInBytes { get; private set; }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    mappedFile.Dispose();
                }
                lineLengths = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private static List<int> CreateLineIndexes(string fileName)
        {
            var list = new List<int>();
            using (var reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line.Length);
                }
            }
            return list;
        }

        private void UpdateLineLengths()
        {
            if (lineLengths == null)
            {
                lineLengths = new List<int>();
                lastOffset = 0;
            }
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.Read | FileShare.Write))
            {
                if (lastOffset > 0)
                {
                    stream.Seek(lastOffset, SeekOrigin.Begin);
                }
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineLengths.Add(line.Length);
                    }
                }
            }
        }

        public List<string> GetTailLines(int lineCount)
        {
            var result = new List<string>();
            long fileOffset = LengthInBytes - lineCount * averageBytesPerLine;
            var accessorLength = LengthInBytes - fileOffset;
            using (var accessor = mappedFile.CreateViewAccessor(fileOffset, accessorLength))
            {
                var lexer = new AccessorAsciiLexer(accessor);
                var parser = new Parser(lexer);
                string line;
                while ((line = parser.NextLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }


    }
}
