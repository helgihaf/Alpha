using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Versioning
{
    public interface IScriptRunner
    {
        event EventHandler<ScriptRunnerEventArgs> ExecutingCommand;
        event EventHandler<ScriptRunnerEventArgs> ExecutedCommand;
        
        void RunScript(Script script);
    }
}
