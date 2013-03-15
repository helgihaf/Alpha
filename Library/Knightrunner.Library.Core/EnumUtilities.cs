using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;

namespace Knightrunner.Library.Core
{
    public static class EnumUtilities
    {
        public static IEnumerable<EnumValue> EnumDisplayValues(Type enumType, ResourceManager resourceManager)
        {
            var values = Enum.GetValues(enumType);
            var names = Enum.GetNames(enumType);

            for (int i = 0; i < values.Length; i++)
            {
                string displayValue = null;
                if (resourceManager != null)
                {
                    displayValue = resourceManager.GetString(enumType.Name + "_" + names[i]);
                }
                if (displayValue == null)
                {
                    displayValue = names[i];
                }

                yield return new EnumValue { Value = (int)values.GetValue(i), DisplayValue = displayValue };
            }
        }
    }


    public class EnumValue
    {
        public int Value { get; set; }
        public string DisplayValue { get; set; }

        public override string ToString()
        {
            return DisplayValue;
        }
    }
}
