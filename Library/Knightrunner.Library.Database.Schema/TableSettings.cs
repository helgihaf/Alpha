using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Database.Schema
{
    public class TableSettings
    {
        private Dictionary<string, Dictionary<string, string>> targets = new Dictionary<string, Dictionary<string, string>>();

        public void Add(string target, string property, string value)
        {
            Dictionary<string, string> properties;
            if (!targets.TryGetValue(target, out properties))
            {
                properties = new Dictionary<string, string>();
                targets.Add(target, properties);
            }

            properties.Add(property, value);
        }


        public bool TryGetValue(string target, string property, out string value)
        {
            bool result;
            value = null;

            Dictionary<string, string> properties;
            if (targets.TryGetValue(target, out properties))
            {
                result = properties.TryGetValue(property, out value);
            }
            else
            {
                result = false;
            }

            return result;
        }


        public bool GetValueAsBool(string target, string property)
        {
            bool result;
            string boolString;

            if (TryGetValue(target, property, out boolString))
            {
                result = ParseBoolString(boolString);
            }
            else
            {
                result = false;
            }

            return result;
        }

        public string GetValueAsString(string target, string property)
        {
            string result;

            if (!TryGetValue(target, property, out result))
            {
                result = null;
            }

            return result;
        }


        private bool ParseBoolString(string boolString)
        {
            bool result;

            if (boolString == "1")
                result = true;
            else if (boolString == "0")
                result = false;
            else
                result = bool.Parse(boolString);

            return result;
        }



    }
}
