using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Knightrunner.Library.Database.Versioning.ResourceScripting
{
    /// <summary>
    /// This class contains methods that scans assemblies looking for resources that represent versioned database scripts.
    /// </summary>
    public static class ResourceScriptScanner
    {
        /// <summary>
        /// Scans the manifest resources in the specified assembly for versioned database scripts and puts them in the
        /// scriptStore.
        /// </summary>
        /// <param name="scriptStore">The script store where scanned scripts are stored.</param>
        /// <param name="assembly">The assembly to scan.</param>
        /// <remarks>
        /// For a resource to be considered a valid database script it must be named in the form (y).(t)._(a)._(b)._(c)._(d).(s)-(x).sql
        /// where
        /// (y) is any string (can contain dots).
        /// (t) is one of Create, Update or Finalize.
        /// (a), (b), (c) and (d) are non-negative integers and represent the script version. NOTE: Leading zeroes are allowed for readability.
        /// (s) is a non-negative integer and represents the sequence number of the script within the version.
        /// (x) is a descriptive name of the script and must not contain any dots.
        /// </remarks>
        public static void Scan(ScriptStore scriptStore, Assembly assembly)
        {
            if (scriptStore == null)
            {
                throw new ArgumentNullException("scriptStore");
            }

            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            foreach (string resourceName in assembly.GetManifestResourceNames())
            {
                Debug.WriteLine(resourceName);
                var script = CreateScriptFromResourceName(resourceName);
                if (script != null)
                {
                    script.ScriptText = LoadScriptText(assembly, resourceName);
                    switch (script.ScriptType)
                    {
                        case ScriptType.Create:
                            scriptStore.CreateScripts.Add(script);
                            break;
                        case ScriptType.Update:
                            scriptStore.UpdateScripts.Add(script);
                            break;
                        case ScriptType.Finalize:
                            scriptStore.FinalizeScripts.Add(script);
                            break;
                        default:
                            throw new NotImplementedException("Implementation missing for ScriptType." + script.ScriptType.ToString());
                    }
                }
            }

        }

        private static Script CreateScriptFromResourceName(string resourceName)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                return null;
            }

            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "Considering resource name '{0}'", resourceName));

            // Example name:
            // ConsoleApplication9.Scripts.Update._001._000._00001._0000.001-Update.sql

            var nameParts = resourceName.Split('.');
            var partIndex = nameParts.Length - 1;

            if (partIndex < 7)
            {
                Trace.WriteLine("...Rejected, too few parts in name.");
                return null;
            }

            // Part: Extension
            
            if (!string.Equals("sql", nameParts[partIndex], StringComparison.OrdinalIgnoreCase))
            {
                Trace.WriteLine("...Rejected, wrong extension.");
                return null;
            }


            // Part: Sequence and name
            partIndex--;

            var sequenceAndName = nameParts[partIndex--];
            if (string.IsNullOrEmpty(sequenceAndName))
            {
                Trace.WriteLine("...Rejected, sequence and name part empty.");
                return null;
            }

            var sequenceIndex = sequenceAndName.IndexOf('-');
            if (sequenceIndex <= 0)
            {
                Trace.WriteLine("...Rejected, no sequence number.");
                return null;
            }

            int sequence;
            if (!int.TryParse(sequenceAndName.Substring(0, sequenceIndex).Trim(), out sequence))
            {
                Trace.WriteLine("...Rejected, invalid sequence number.");
                return null;
            }

            string name = string.Empty;
            if (sequenceIndex + 1 < sequenceAndName.Length)
            {
                name = sequenceAndName.Substring(sequenceIndex + 1).Trim();
            }

            // Part: Version
            int[] versionNumbers = new int[4];
            for (int i = versionNumbers.Length - 1; i >= 0; i--)
            {
                if (!TryParseVersionNumber(nameParts[partIndex--], out versionNumbers[i]))
                {
                    Trace.WriteLine("...Rejected, invalid version number.");
                    return null;
                }
            }
            var version = new Version(versionNumbers[0], versionNumbers[1], versionNumbers[2], versionNumbers[3]);

            // Part: ScriptType
            ScriptType scriptType;
            if (!Enum.TryParse<ScriptType>(nameParts[partIndex--], out scriptType))
            {
                Trace.WriteLine("...Rejected, invalid script type.");
                return null;
            }

            return new Script
            {
                ScriptType = scriptType,
                Version = version,
                Sequence = sequence,
                Name = name,
            };
        }


        private static bool TryParseVersionNumber(string s, out int versionNumber)
        {
            int index = 0;
            if (s.StartsWith("_", StringComparison.Ordinal))
            {
                index++;
            }
            return int.TryParse(s.Substring(index), out versionNumber);
        }

        private static string LoadScriptText(Assembly assembly, string resourceName)
        {
            using (StreamReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
            {
                return textStreamReader.ReadToEnd();
            }
        }


    }
}
