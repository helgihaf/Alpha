using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema
{
    public interface IScriptDocumentGenerator
    {
        void WriteDocumentation(string databaseSchemaName, System.IO.StreamWriter writer, Table table);
    }
}
