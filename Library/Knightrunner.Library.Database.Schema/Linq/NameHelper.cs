using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema.Linq
{
    internal static class NameHelper
    {
        public static string GetMemberNamePlural(Table table)
        {
            string memberName;
            if (table.MemberName != null)
                memberName = table.MemberName;
            else
            {
                int index = table.Name.LastIndexOf('.');
                if (index == -1)
                    memberName = PluralOf(table.Name);
                else
                    memberName = PluralOf(table.Name.Substring(index + 1));
            }

            return memberName;
        }


        public static string GetMemberNameSingular(Table table)
        {
            string memberName;
            if (table.MemberName != null)
                memberName = table.MemberName;
            else
            {
                int index = table.Name.LastIndexOf('.');
                if (index == -1)
                    memberName = SingularOf(table.Name);
                else
                    memberName = SingularOf(table.Name.Substring(index + 1));
            }

            return memberName;
        }

        public static string GetTypeName(Table table)
        {
            string typeName;
            if (table.TypeName != null)
                typeName = table.TypeName;
            else
            {
                int index = table.Name.LastIndexOf('.');
                if (index == -1)
                {
                    typeName = SingularOf(table.Name);
                }
                else
                {
                    typeName = SingularOf(table.Name.Substring(index + 1));
                }
            }

            return typeName;
        }


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
