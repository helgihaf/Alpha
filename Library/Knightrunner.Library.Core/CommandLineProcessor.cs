using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Security.Permissions;

namespace Knightrunner.Library.Core
{
    /// <summary>
    /// The CommandLineProcessor class TODO: Describe class here
    /// </summary>
    public class CommandLineProcessor
    {
        private List<CommandLineParameter> commandLineParmeters = new List<CommandLineParameter>();
        private char parameterMarker = '/';
        private char valueMarker = ':';
        private char onSwitchMarker = '+';
        private char offSwitchMarker = '-';
        private char showUsageMarker = '?';
        private bool autoCheckForMissingArguments = false;

        public CommandLineProcessor()
        {
            MatchUnnamedArgumentsToParameters = true;
        }

        public void ProcessArguments(string [] args)
        {
            ProcessArguments(args, 0);
        }

        public void ProcessArguments(string[] args, int startIndex)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            // Make sure that we either have zero or one unnamed parameter accepting multiple values
            bool foundMultiple = false;

            // Initialize all parameters to their default values
            foreach (var parameter in commandLineParmeters)
            {
                parameter.IsPresent = false;
                if (!parameter.PositionalAcceptsMultipleValues)
                {
                    parameter.StringValue = null;
                }
                parameter.SwitchValue = true;
                if (parameter.PositionalAcceptsMultipleValues)
                {
                    if (foundMultiple)
                    {
                        throw new InvalidOperationException("Invalid command line parameter configuration: Only one command line parameter allowed accepting multiple values");
                    }
                    foundMultiple = true;
                }
            }
            ShowUsageRequested = false;

            UnnamedArguments = new List<string>();

            for (int i = startIndex; i < args.Length; i++)
            {
                ProcessArgument(args[i], UnnamedArguments);
                if (ShowUsageRequested)
                    return;
            }

            if (MatchUnnamedArgumentsToParameters)
            {
                // Match unnamed entries agains their parameter objects
                int parameterIndex = NextUnnamedParameter(-1);
                for (int i = 0; i < UnnamedArguments.Count; i++)
                {
                    if (parameterIndex == -1)
                    {
                        throw new CommandLineProcessorException("Invalid parameter " + UnnamedArguments[i]);
                    }
                    var parameter = commandLineParmeters[parameterIndex];
                    commandLineParmeters[parameterIndex].IsPresent = true;
                    if (!parameter.PositionalAcceptsMultipleValues)
                    {
                        parameter.StringValue = UnnamedArguments[i];
                        parameterIndex = NextUnnamedParameter(parameterIndex);
                    }
                    else
                    {
                        parameter.StringValues.Add(UnnamedArguments[i]);
                    }
                }
            }

            if (this.autoCheckForMissingArguments)
            {
                // Check if any required arguments are missing
                CheckForMissingArguments();
            }
        }

        private int NextUnnamedParameter(int parameterIndex)
        {
            for (int i = parameterIndex + 1; i < commandLineParmeters.Count; i++)
            {
                if (commandLineParmeters[i].Name == null)
                    return i;
            }

            return -1;
        }

        public void CheckForMissingArguments()
        {
            foreach (CommandLineParameter param in commandLineParmeters)
            {
                if (param.Required && !param.IsPresent)
                {
                    throw new CommandLineProcessorException("Required parameter " + ParamShortName(param) + " missing.");
                }
            }
        }


        private void ProcessArgument(string arg, List<string> unnamedArguments)
        {
            Debug.Assert(arg != null && arg.Length > 0);

            // Make sure the argument starts with the parameterMarker (e.g. '/'), otherwise this is an unnamed marker
            if (arg[0] != parameterMarker)
            {
                unnamedArguments.Add(arg);
                return;
            }

            if (arg.Length < 2)
            {
                throw new CommandLineProcessorException("Invalid command line parameter");
            }
            if (arg[1] == showUsageMarker)
            {
                ShowUsageRequested = true;
                return;
            }

            // Isolate the name of the parameter
            int nameEndIndex = arg.IndexOfAny(new char[] { valueMarker, onSwitchMarker, offSwitchMarker }, 1);
            string parameterName;
            if (nameEndIndex == -1)
            {
                parameterName = arg.Substring(1);
            }
            else
            {
                // Example data:
                //   0123456789
                //   /input:true
                //   nameEndIndex = 6
                //   length = 5 = 6 - 1 = nameEndIndex - 1
                parameterName = arg.Substring(1, nameEndIndex - 1);
            }

            // Find the parameter object        
            CommandLineParameter param = FindParameter(parameterName);
            if (param == null)
                throw new CommandLineProcessorException("Unknown parameter " + arg);
            
            param.IsPresent = true;

            if (nameEndIndex == -1)
            {
                if (param.ValueOption == CommandLineValueOption.One)
                {
                    throw CreateValueMissingException(param);
                }
                return;
            }

            int valueMarkerIndex;

            // Check for switches
            if (arg[nameEndIndex] == onSwitchMarker)
            {
                param.SwitchValue = true;
                valueMarkerIndex = nameEndIndex + 1;
            }
            else if (arg[nameEndIndex] == offSwitchMarker)
            {
                param.SwitchValue = false;
                valueMarkerIndex = nameEndIndex + 1;
            }
            else
            {
                valueMarkerIndex = nameEndIndex;
            }
            
            // Check if we have a value. Note that we can both have an on/off switch marker AND a value marker
            if (valueMarkerIndex >= arg.Length)
            {
                // No value
                if (param.ValueOption == CommandLineValueOption.One)
                {
                    throw CreateValueMissingException(param);
                }
                return;
            }
            else if (arg[valueMarkerIndex] != valueMarker)
            {
                throw new CommandLineProcessorException("Invalid command line parameter format for parameter " + ParamShortName(param) +
                    ". Expected '" + valueMarker + "'", param);
            }

            if (valueMarkerIndex + 1 >= arg.Length)
            {
                throw new CommandLineProcessorException("Invalid command line parameter format for parameter " + ParamShortName(param) +
                    ". Expected a value following '" + valueMarker + "'", param);
            }

            param.StringValue = arg.Substring(valueMarkerIndex + 1);
        }



        private Exception CreateValueMissingException(CommandLineParameter param)
        {
            return new CommandLineProcessorException("Value missing for parameter " + ParamShortName(param), param);
        }

        private bool IsArgumentMarker(string arg)
        {
            string marker;
            return IsArgumentMarker(arg, out marker);
        }

        private bool IsArgumentMarker(string arg, out string marker)
        {
            marker = null;
            
            if (string.IsNullOrEmpty(arg))
                return false;

            if (arg.Length < 2)
                return false;

            if (arg[0] != this.parameterMarker)
                return false;

            marker = arg.Substring(1);
            
            return true;
        }

        public string ParamShortName(CommandLineParameter param)
        {
            string result;

            if (param.Name == null)
                result = param.ValueName;
            else
                result = this.parameterMarker.ToString() + param.Name;

            return result;
        }

        public CommandLineParameter this[string parameterName]
        {
            get { return FindParameter(parameterName); }
        }


        private CommandLineParameter FindParameter(string parameterName)
        {
            CommandLineParameter result = null;
            
            foreach (CommandLineParameter param in this.commandLineParmeters)
            {
                if (param.Name == parameterName)
                {
                    result = param;
                    break;
                }
            }
            
            return result;
        }

        [SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
        public void ShowUsage(string[] description)
        {
            var process = Process.GetCurrentProcess();
            string executableName = Path.GetFileName(process.MainModule.FileName);
            ShowUsage(description, executableName);
        }

        public void ShowUsage(string[] description, string executableName)
        {
            if (description != null && description.Length > 0)
            {
                Console.Out.WriteLine();
                foreach (string line in description)
                {
                    Console.Out.WriteLine(line);
                }
            }
            Console.Out.WriteLine();
            Console.Out.WriteLine("Usage:");
            StringBuilder sb = new StringBuilder();
            sb.Append(executableName + " ");
            foreach (CommandLineParameter param in commandLineParmeters)
            {
                if (!param.Required)
                {
                    sb.Append("[");
                }
                sb.Append(GetParamText(param));
                if (!param.Required)
                {
                    sb.Append("]");
                }
                sb.Append(" ");
            }

            Console.Out.WriteLine(sb.ToString().Trim());

            const int maxParamLength = 14;
            foreach (CommandLineParameter param in commandLineParmeters)
            {
                string paramText = GetParamText(param);
                Console.Out.Write("  " + paramText.PadRight(maxParamLength) + "  ");
                if (paramText.Length >= maxParamLength)
                {
                    Console.Out.WriteLine();
                    Console.Out.Write(string.Empty.PadLeft(maxParamLength + 4));
                }

                int descWidth = Console.WindowWidth - maxParamLength - 4;
                
                string[] descriptionLines = param.Description.WordWrap(descWidth);

                for (int i = 0; i < descriptionLines.Length; i++)
                {
                    if (i > 0)
                        Console.Out.Write(string.Empty.PadLeft(maxParamLength + 4));
                    Console.Out.WriteLine(descriptionLines[i]);
                }
            }
        }


        private string GetParamText(CommandLineParameter param)
        {
            StringBuilder sb = new StringBuilder();
            if (param.Name == null)
            {
                sb.Append(param.ValueName);
            }
            else
            {
                sb.Append(this.parameterMarker);
                sb.Append(param.Name);
                string valueFriendlyName = string.IsNullOrEmpty(param.ValueName) ? "v" : param.ValueName;
                if (param.ValueOption == CommandLineValueOption.Optional)
                {
                    sb.AppendFormat(valueMarker + "[<{0}>]", valueFriendlyName);
                }
                else if (param.ValueOption == CommandLineValueOption.One)
                {
                    sb.AppendFormat(valueMarker + "<{0}>", valueFriendlyName);
                }
            }
            return sb.ToString().TrimEnd();
        }


        public List<CommandLineParameter> CommandLineParameters
        {
            get { return this.commandLineParmeters; }
        }

        public char ParameterMarker
        {
            get { return parameterMarker; }
            set { parameterMarker = value; }
        }

        public bool AutoCheckForMissingArguments
        {
            get { return autoCheckForMissingArguments; }
            set { autoCheckForMissingArguments = value; }
        }


        public bool ShowUsageRequested { get; private set; }
        public bool MatchUnnamedArgumentsToParameters { get; set; }

        public List<string> UnnamedArguments { get; private set; }
    }
    
    
    [Serializable]
    public class CommandLineProcessorException : ApplicationException
    {
        private CommandLineParameter commandLineParameter;

        public CommandLineProcessorException(string message)
            : base(message)
        {
        }

        public CommandLineProcessorException(string message, CommandLineParameter param)
            : base(message)
        {
            this.commandLineParameter = param;
        }
        
        public CommandLineProcessorException(string message, CommandLineParameter param, Exception inner)
            : base(message, inner)
        {
            this.commandLineParameter = param;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Serialization constructor.
        /// </summary>
        protected CommandLineProcessorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public CommandLineParameter CommandLineParameter
        {
            get { return commandLineParameter; }
        }


        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("commandLineParameter", commandLineParameter);
        }

    }
    
    
}
