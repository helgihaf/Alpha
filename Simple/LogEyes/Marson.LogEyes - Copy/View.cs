using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace Marson.LogEyes
{
    //public class View : IDisposable
    //{
    //    private readonly LogFile logFile;
    //    private readonly Offset offset;
    //    private readonly int lineCount;

    //    private MemoryMappedViewAccessor accessor;
    //    private long avgBytesPerLine = 1024;
    //    private long accessorLength;

    //    public View(LogFile logFile, Offset offset, int lineCount)
    //    {
    //        this.logFile = logFile;
    //        this.offset = offset;
    //        this.lineCount = lineCount;
    //    }

    //    public string[] ReadLines(int lineCount)
    //    {
    //        AssertLexer();
    //        var lines = new List<string>();
    //        Lexer.Token token = lexer.Next();
    //        while (token != Lexer.Token.EOF && token != Lexer.Newline)
    //        {
    //            token = lexer.Next();
    //        }
    //        if (token == )
    //            if (logFile.Encoding == Encoding.ASCII)
    //            {
    //                long position = 0;
    //                byte b = 0;
    //                while (position < accessorLength && b != 10)
    //                {
    //                    b = accessor.ReadByte(position++);
    //                }

    //                if (position == accessorLength)
    //                {
    //                    return null;
    //                }

    //                b = accessor.ReadByte();
    //            }
    //    }


    //    public string[] ReadLinesOld(int lineCount)
    //    {
    //        AssertAccessor();
    //        var lines = new List<string>();
    //        if (logFile.Encoding == Encoding.ASCII)
    //        {
    //            long position = 0;
    //            byte b = 0;
    //            while (position < accessorLength && b != 10)
    //            {
    //                b = accessor.ReadByte(position++);
    //            }

    //            if (position == accessorLength)
    //            {
    //                return null;
    //            }

    //            b = accessor.ReadByte();
    //        }
    //    }

    //    private void AssertAccessor()
    //    {
    //        if (offset == Offset.End)
    //        {
    //            long fileOffset = logFile.FileLength - lineCount * avgBytesPerLine;
    //            accessorLength = logFile.FileLength - fileOffset;
    //            accessor = logFile.MemoryMappedFile.CreateViewAccessor(fileOffset, accessorLength);
    //        }
    //        else
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            if (accessor != null)
    //            {
    //                accessor.Dispose();
    //                accessor = null;
    //            }
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //    }
    //}
}