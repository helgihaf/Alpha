using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Versioning
{
    public class Script
    {
        public ScriptType ScriptType { get; set; }
        public Version Version { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string ScriptText { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as Script;
            if ((System.Object)other == null)
            {
                return false;
            }

            // Memberwise compare
            return
                object.Equals(ScriptType, other.ScriptType) &&
                object.Equals(Version, other.Version) &&
                object.Equals(Sequence, other.Sequence) &&
                object.Equals(Name, other.Name) &&
                object.Equals(ScriptText, other.ScriptText);

            
        }


        public override int GetHashCode()
        {
            var versionHashCode = Version != null ? Version.GetHashCode() : 0;
            var nameHashCode = Name != null ? Name.GetHashCode() : 0;
            var scriptTextHashCode = ScriptText != null ? ScriptText.GetHashCode() : 0;

            return
                this.ScriptType.GetHashCode() ^
                versionHashCode ^
                this.Sequence.GetHashCode() ^
                nameHashCode ^
                scriptTextHashCode;
        }
    }
}
