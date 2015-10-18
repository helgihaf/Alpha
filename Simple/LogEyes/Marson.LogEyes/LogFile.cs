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
        private const int bufferSize = 4096;
        private bool disposedValue = false;
        private string filePath;
        private readonly FileInfo fileInfo;
        private List<long> lineOffsets;

        public LogFile(string filePath)
        {
            this.filePath = filePath;
            this.fileInfo = new FileInfo(filePath);
        }

        public int LineCount
        {
            get
            {
                return lineOffsets.Count;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                lineOffsets = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        //private void UpdateLineOffsets()
        //{
        //    if (lineOffsets == null)
        //    {
        //        lineOffsets = new List<long>();
        //    }
        //    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.Read | FileShare.Write))
        //    {
        //        if (lineOffsets.Count > 0)
        //        {
        //            stream.Seek(lineOffsets[lineOffsets.Count - 1], SeekOrigin.Begin);
        //        }

        //        ILexer lexer = new StreamAsciiLexer(stream);
        //        Token token = lexer.Next();

        //        while (token != Token.EndOfStream)
        //        {
        //            lineOffsets.Add(lexer.Position);
        //            token = lexer.Next();
        //        }
        //    }
        //}

        public List<string> GetTailLines()
        {
            var result = new List<string>();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.Read | FileShare.Write))
            {
                stream.Seek(-bufferSize, SeekOrigin.End);
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        result.Add(line);
                    }
                }
            }

            return result;
        }


    }
}
