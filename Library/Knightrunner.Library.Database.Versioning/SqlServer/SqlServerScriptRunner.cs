using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace Knightrunner.Library.Database.Versioning.SqlServer
{
    public class SqlServerScriptRunner : IScriptRunner
    {
        private SqlConnection sqlConnection;

        public SqlServerScriptRunner(SqlConnection sqlConnection)
        {
            if (sqlConnection == null)
            {
                throw new ArgumentNullException("sqlConnection");
            }

            this.sqlConnection = sqlConnection;
        }

        public event EventHandler<ScriptRunnerEventArgs> ExecutingCommand;
        public event EventHandler<ScriptRunnerEventArgs> ExecutedCommand;

        public void RunScript(Script script)
        {
            using (StringReader reader = new StringReader(script.ScriptText))
            {
                string textLine;
                StringBuilder commandText = new StringBuilder();
                while ((textLine = reader.ReadLine()) != null)
                {
                    if (textLine.Trim().ToUpperInvariant() == "GO")
                    {
                        ExecuteCommand(commandText.ToString(), script);
                        commandText.Clear();
                    }
                    else
                    {
                        commandText.AppendLine(textLine);
                    }
                }

                // If there is no GO in the SQL statement then run the entire statement
                if (commandText.Length > 0)
                {
                    ExecuteCommand(commandText.ToString(), script);
                }
            }

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private void ExecuteCommand(string commandText, Script script)
        {
            OnExecutingCommand(ref commandText, script);
            using (var command = new SqlCommand(commandText, this.sqlConnection))
            {
                command.ExecuteNonQuery();
                OnExecutedCommand(commandText, script);
            }
        }

        private void OnExecutingCommand(ref string commandText, Script script)
        {
            if (ExecutingCommand != null)
            {
                var eventArgs = new ScriptRunnerEventArgs
                {
                    Script = script,
                    CommandText = commandText
                };
                ExecutingCommand(this, eventArgs);

                if (!object.ReferenceEquals(eventArgs.CommandText, commandText))
                {
                    commandText = eventArgs.CommandText;
                }
            }
        }

        private void OnExecutedCommand(string commandText, Script script)
        {
            if (ExecutedCommand != null)
            {
                var eventArgs = new ScriptRunnerEventArgs
                {
                    Script = script,
                    CommandText = commandText
                };
                ExecutedCommand(this, eventArgs);
            }
        }
    }
}
