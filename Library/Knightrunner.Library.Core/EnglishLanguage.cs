using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.Library.Core
{
    public static class EnglishLanguage
    {
        public static string SingularOf(string text)
        {
            if (text == null || text.Length <= 1)
                return text;

            if (text.EndsWith("ies", StringComparison.OrdinalIgnoreCase))
                return text.Substring(0, text.Length - 3) + "y";
            if (text.EndsWith("oes", StringComparison.OrdinalIgnoreCase))
                return text.Substring(0, text.Length - 3) + "o";
            if (text.EndsWith("pes", StringComparison.OrdinalIgnoreCase))
                return text.Substring(0, text.Length - 3) + "pe";           // e.g. type
            if (text.EndsWith("les", StringComparison.OrdinalIgnoreCase))
                return text.Substring(0, text.Length - 3) + "le";           // e.g. table
            if (text.EndsWith("nes", StringComparison.OrdinalIgnoreCase))
                return text.Substring(0, text.Length - 1);                  // "lines" -> "line"
            if (text.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                return text.Substring(0, text.Length - 1);

            return text;
        }

        public static string PluralOf(string text)
        {
            if (text == null || text.Length <= 1)
                return text;

            if (text.EndsWith("sh", StringComparison.OrdinalIgnoreCase))
                return text + "es";
            if (text.EndsWith("ch", StringComparison.OrdinalIgnoreCase))
                return text + "es";
            if (text.EndsWith("us", StringComparison.OrdinalIgnoreCase))
                return text + "es";
            if (text.EndsWith("ss", StringComparison.OrdinalIgnoreCase))
                return text + "es";
            //-ies rule
            if (text.EndsWith("y", StringComparison.OrdinalIgnoreCase))
                return text.Remove(text.Length - 1, 1) + "ies";
            // -oes rule
            if (text.EndsWith("o", StringComparison.OrdinalIgnoreCase))
                return text.Remove(text.Length - 1, 1) + "oes";
            // -s suffix rule
            if (!text.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                return text + "s";

            return text;
        }
    }
}
