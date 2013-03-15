using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Knightrunner.Library.Database.Schema
{
    public class IndentedStreamWriter : StreamWriter
    {
        int indent = 0;
        bool needsIndent = true;

        public IndentedStreamWriter(Stream stream)
            : base(stream)
        {
        }

        public IndentedStreamWriter(string path)
            : base(path)
        {
        }

        public IndentedStreamWriter(Stream stream, Encoding encoding)
            : base(stream, encoding)
        {
        }

        public IndentedStreamWriter(string path, bool append)
            : base(path, append)
        {
        }

        public IndentedStreamWriter(Stream stream, Encoding encoding, int bufferSize)
            : base(stream, encoding, bufferSize)
        {
        }

        public IndentedStreamWriter(string path, bool append, Encoding encoding)
            : base(path, append, encoding)
        {
        }

        public IndentedStreamWriter(string path, bool append, Encoding encoding, int bufferSize)
            : base(path, append, encoding, bufferSize)
        {
        }

        public void Indent()
        {
            indent++;
        }

        public void Unindent()
        {
            if (indent > 0)
                indent--;
        }

        public void ClearIndent()
        {
            indent = 0;
        }

        public override void Write(string value)
        {
            if (needsIndent && indent > 0)
            {
                WriteIndent();
                needsIndent = false;
            }

            base.Write(value);
        }


        public override void WriteLine(string value)
        {
            if (needsIndent && indent > 0)
            {
                WriteIndent();
            }
            base.WriteLine(value);
            needsIndent = true;
        }


        private void WriteIndent()
        {
            base.Write(string.Empty.PadRight(indent, '\t'));
        }
    }
}
