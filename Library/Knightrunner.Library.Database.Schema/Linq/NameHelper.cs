using Knightrunner.Library.Core;
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
                    memberName = EnglishLanguage.PluralOf(table.Name);
                else
                    memberName = EnglishLanguage.PluralOf(table.Name.Substring(index + 1));
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
                    memberName = EnglishLanguage.SingularOf(table.Name);
                else
                    memberName = EnglishLanguage.SingularOf(table.Name.Substring(index + 1));
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
                    typeName = EnglishLanguage.SingularOf(table.Name);
                }
                else
                {
                    typeName = EnglishLanguage.SingularOf(table.Name.Substring(index + 1));
                }
            }

            return typeName;
        }
    }
}
