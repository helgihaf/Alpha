using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.Library.Database.Schema
{
    public interface IScriptDocumentGeneratorFactory
    {
        IScriptDocumentGenerator Create(string name);
    }
}
