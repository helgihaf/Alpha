using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marson.LogEyes
{
    public class StreamAsciiLexer : ILexer
    {
        private const byte Newline = 10;
        private const byte CarriageReturn = 13;

        private Stream stream;
        private long position;
        private readonly StringBuilder lexeme = new StringBuilder();

        public StreamAsciiLexer(Stream stream)
        {
            this.stream = new BufferedStream(stream);
            position = 0;
        }

        public string Lexeme()
        {
            return lexeme.ToString();
        }

        public Token Next()
        {
            byte characterByte;

            lexeme.Clear();
            characterByte = ReadByte();

            while (characterByte == Newline || characterByte == CarriageReturn)
            {
                position++;
                characterByte = ReadByte();
            }

            while (characterByte != byte.MinValue && characterByte != Newline && characterByte != CarriageReturn)
            {
                lexeme.Append(Encoding.ASCII.GetString(new byte[] { characterByte }));
                position++;
                characterByte = ReadByte();
            }

            if (lexeme.Length > 0)
            {
                return Token.String;
            }
            else
            {
                return Token.EndOfStream;
            }
        }

        private byte ReadByte()
        {
            int value = stream.ReadByte();
            return value >= 0 ? Convert.ToByte(value) : byte.MinValue;
        }

        public long Position
        {
            get { return position; }
        }
    }
}
