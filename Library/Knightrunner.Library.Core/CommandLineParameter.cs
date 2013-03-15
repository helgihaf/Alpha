using System;
using System.Linq;
using System.Collections.Generic;

namespace Knightrunner.Library.Core
{
    /// <summary>
    /// The CommandLineParameter class contains the definition of a command line parameter.
    /// </summary>
    public class CommandLineParameter
    {
        private string name;
        private string description;
        private bool required;
        private CommandLineValueOption valueOption;
        private string valueName;
        private bool isSwitch;
        private bool positionalAcceptsMultipleValues;
        private List<string> stringValues;

        /// <summary>
        /// Create an unnamed (positional) command line parameter with the specifed settings.
        /// </summary>
        /// <param name="required">True if the parameter is required, false otherwise.</param>
        /// <param name="valueName">A descriptive name of the value</param>
        /// <param name="positionalAcceptsMultipleValues">True if the parameter accepts multiple unnamed values from the command line (e.g. list of files), false otherwise.</param>
        /// <param name="description">A description of the parameter</param>
        public CommandLineParameter(bool required, string valueName, bool positionalAcceptsMultipleValues, string description)
        {
            this.name = null;
            this.required = required;
            this.positionalAcceptsMultipleValues = positionalAcceptsMultipleValues;
            this.valueOption = CommandLineValueOption.One;
            this.valueName = valueName;
            this.isSwitch = false;
            this.description = description;
        }

        /// <summary>
        /// Create a named command line parameter with the specifed settings.
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="required">True if the paramter is required, false otherwise.</param>
        /// <param name="valueOption">Should this parameter have a value? Is it required or optional?</param>
        /// <param name="isSwitch">True if the parameter is a switch that can have an on/off value, false otherwise.</param>
        /// <param name="description">A description of the parameter</param>
        public CommandLineParameter(string name, bool required, CommandLineValueOption valueOption, bool isSwitch, string description)
        {
            this.name = name;
            this.required = required;
            this.valueOption = valueOption;
            this.isSwitch = isSwitch;
            this.description = description;
        }

        /// <summary>
        /// Create a named command line parameter with the specifed settings.
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="required">True if the paramter is required, false otherwise.</param>
        /// <param name="valueOption">Should this parameter have a value? Is it required or optional?</param>
        /// <param name="valueName">If this parameter can have a value, this is a descriptive name of that value</param>
        /// <param name="isSwitch">True if the parameter is a switch that can have an on/off value, false otherwise.</param>
        /// <param name="description">A description of the parameter</param>
        public CommandLineParameter(string name, bool required, CommandLineValueOption valueOption, string valueName, bool isSwitch, string description)
        {
            this.name = name;
            this.required = required;
            this.valueOption = valueOption;
            this.valueName = valueName;
            this.isSwitch = isSwitch;
            this.description = description;
        }

        /// <summary>
        /// Gets the description of the command line parameter.
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// Gets the name of the command line parameter or null if this is an unnamed parameters.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        
        /// <summary>
        /// Gets a value indicating if this parameter is required.
        /// </summary>
        public bool Required
        {
            get { return required; }
        }

        public CommandLineValueOption ValueOption
        {
            get { return valueOption; }
        }

        public string ValueName
        {
            get { return valueName; }
        }

        public bool IsSwitch
        {
            get { return isSwitch; }
        }

        public bool IsPresent { get; set; }

        public string StringValue { get; set; }
        
        public List<string> StringValues
        {
            get
            {
                if (stringValues == null)
                {
                    stringValues = new List<string>();
                }
                return stringValues;
            }
        }

        public bool SwitchValue { get; set; }

        public bool PositionalAcceptsMultipleValues
        {
            get { return positionalAcceptsMultipleValues; }
        }
    }

    /// <summary>
    /// Indicates whether a command line parameter should be specified with or without a value.
    /// </summary>
    public enum CommandLineValueOption
    {
        /// <summary>
        /// A value is never allowed
        /// </summary>
        Never,
        /// <summary>
        /// A single value can optionally be specified
        /// </summary>
        Optional,
        /// <summary>
        /// A single value should always be specified
        /// </summary>
        One,
    }
    
}
