using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.WorkTrack.WinForms
{
    public static class WinFormUtilities
    {
        public static string TextOrNull(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            else
            {
                return null;
            }
        }
    }
}
