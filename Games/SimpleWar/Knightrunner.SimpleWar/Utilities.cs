using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.SimpleWar
{
    public static class Utilities
    {
        public static string GetResourceTextFile(Assembly assembly, string fileName)
        {
            var name = assembly.GetName().Name;
            using (var stream = assembly.GetManifestResourceStream(name + "." + fileName))
            {
                using (var sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }        

    }
}
