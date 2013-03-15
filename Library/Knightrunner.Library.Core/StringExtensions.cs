using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Core
{
    public static class StringExtensions
    {
        public static string[] WordWrap(this string source, int lineLength)
        {
            List<string> strings = new List<string>();

            int index = 0;

            while (index < source.Length)
            {
                int lastSpaceIndex = -1;
                char lastChar = char.MinValue;
                int lineStartIndex = index;
                for (int i = 0; i < lineLength && index < source.Length; i++)
                {
                    char currentChar = source[index];

                    if (currentChar == '\r')
                    {
                        lastSpaceIndex = index;
                        if (index + 1 < source.Length && source[index + 1] == '\n')
                            index++;
                        index++;
                        break;
                    }
                    if (currentChar == '\n')
                    {
                        lastSpaceIndex = index;
                        index++;
                        break;
                    }

                    if (currentChar == ' ' && lastChar != ' ')
                        lastSpaceIndex = index;
                    lastChar = currentChar;

                    index++;
                }

                // Check if index refers to a whitespace - a perfect match line
                if (index == source.Length || char.IsWhiteSpace(source[index]))
                    lastSpaceIndex = index;

                if (lastSpaceIndex == -1)
                    lastSpaceIndex = Math.Min(lineLength + index, source.Length);

                string line = source.Substring(lineStartIndex, lastSpaceIndex - lineStartIndex);
                strings.Add(line);

                index = lastSpaceIndex;
                while (index < source.Length && source[index] == ' ')
                    index++;
            }

            return strings.ToArray();
        }
    }
}
