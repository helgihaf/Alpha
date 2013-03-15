using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema
{
    public interface IScriptGenerator
    {
        ScriptCasing IdentifierCasing { get; set; }
        string FileName { get; set; }
        IScriptDocumentGenerator ScriptDocumentGenerator { get; set; }
    }

    public enum ScriptCasing
    {
        Preserve,
        Lowercase,
        Uppercase
    }
}
