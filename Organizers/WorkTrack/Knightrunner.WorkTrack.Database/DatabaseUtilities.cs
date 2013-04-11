using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Knightrunner.WorkTrack.Database
{
    public static class DatabaseUtilities
    {
        
        internal static bool SimplePropertiesEquals(object obj1, object obj2)
        {
            if (object.ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            if (obj1 == null || obj1 == null)
            {
                return false;
            }

            if (obj1.GetType() != obj2.GetType())
            {
                return false;
            }

            var properties = obj1.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                if (property.PropertyType.IsValueType)
                {
                    var value1 = property.GetValue(obj1, null);
                    var value2 = property.GetValue(obj2, null);
                    if (value1 != null)
                    {
                        if (!value1.Equals(value2))
                        {
                            return false;
                        }
                    }
                    else if (value2 != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// Gets a resource string for the specified enumeration value using the current thread's CurrentUICulture.
        /// </summary>
        /// <remarks>
        /// The resource string must have a name of the form type_member where type is the un-namespeced name
        /// of the enumeration type and member is the name of the enumeration member.
        /// Example: JobValidationMessage_DescriptionMissing.
        /// </remarks>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="resourceManager">The ResourceManager where the string is fetched from.</param>
        /// <param name="value">The enum value.</param>
        /// <param name="resourceCulture">The CultureInfo of the string to fetch.</param>
        /// <returns>The resource string for the enumeration value or null if none was found.</returns>
        public static string EnumResourceString<T>(System.Resources.ResourceManager resourceManager, T value)
        {
            return EnumResourceString(resourceManager, typeof(T), value, System.Threading.Thread.CurrentThread.CurrentUICulture);
        }


        /// <summary>
        /// Gets a resource string for the specified enumeration value.
        /// </summary>
        /// <remarks>
        /// The resource string must have a name of the form type_member where type is the un-namespeced name
        /// of the enumeration type and member is the name of the enumeration member.
        /// Example: JobValidationMessage_DescriptionMissing.
        /// </remarks>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="resourceManager">The ResourceManager where the string is fetched from.</param>
        /// <param name="value">The enum value.</param>
        /// <param name="resourceCulture">The CultureInfo of the string to fetch.</param>
        /// <returns>The resource string for the enumeration value or null if none was found.</returns>
        public static string EnumResourceString<T>(System.Resources.ResourceManager resourceManager, T value, System.Globalization.CultureInfo resourceCulture)
        {
            return EnumResourceString(resourceManager, typeof(T), value, resourceCulture);
        }


        /// <summary>
        /// Gets a resource string for the specified enumeration value using the current thread's CurrentUICulture.
        /// </summary>
        /// <remarks>
        /// The resource string must have a name of the form type_member where type is the un-namespeced name
        /// of the enumeration type and member is the name of the enumeration member.
        /// Example: JobValidationMessage_DescriptionMissing.
        /// </remarks>
        /// <param name="resourceManager">The ResourceManager where the string is fetched from.</param>
        /// <param name="enumType">The enum type.</param>
        /// <param name="value">The enum value.</param>
        /// <param name="resourceCulture">The CultureInfo of the string to fetch.</param>
        /// <returns>The resource string for the enumeration value or null if none was found.</returns>
        public static string EnumResourceString(System.Resources.ResourceManager resourceManager, Type enumType, object value)
        {
            return EnumResourceString(resourceManager, enumType, value, System.Threading.Thread.CurrentThread.CurrentUICulture);
        }

        /// <summary>
        /// Gets a resource string for the specified enumeration value.
        /// </summary>
        /// <remarks>
        /// The resource string must have a name of the form type_member where type is the un-namespeced name
        /// of the enumeration type and member is the name of the enumeration member.
        /// Example: JobValidationMessage_DescriptionMissing.
        /// </remarks>
        /// <param name="resourceManager">The ResourceManager where the string is fetched from.</param>
        /// <param name="enumType">The enum type.</param>
        /// <param name="value">The enum value.</param>
        /// <param name="resourceCulture">The CultureInfo of the string to fetch.</param>
        /// <returns>The resource string for the enumeration value or null if none was found.</returns>
        public static string EnumResourceString(System.Resources.ResourceManager resourceManager, Type enumType, object value, System.Globalization.CultureInfo resourceCulture)
        {
            if (resourceManager == null)
                throw new ArgumentNullException("resourceManager");

            if (enumType == null)
                throw new ArgumentNullException("enumType");

            if (value == null)
                throw new ArgumentNullException("value");

            string name = enumType.Name + "_" + Enum.GetName(enumType, value);
            return resourceManager.GetString(name, resourceCulture);
        }


        public static string BoolToYesNoString(bool value)
        {
            if (value)
            {
                return Properties.Resources.BoolYesText;
            }
            else
            {
                return Properties.Resources.BoolNoText;
            }
        }


    }
}
