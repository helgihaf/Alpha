using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Database.Schema.Verification;
using Knightrunner.Library.Core.Collections;

namespace Knightrunner.Library.Database.Schema
{
    public class ColumnType : DynamicKeyedItem<string>
    {
        private int? maxLength;
        private bool? canBeNull;
        private bool? isDbGenerated;
        private string enumTypeName;
        private int? precision;
        private int? scale;

        public ColumnType()
        {
            Targets = new TargetCollection(this);
        }

        public DataSchema DataSchema { get; internal set; }

        public string Name
        {
            get { return Key; }
            set { Key = value; }
        }

        public string Description { get; set; }
        public ColumnType BaseType { get; set; }
        public TargetCollection Targets { get; private set; }

        public int? MaxLength
        {
            get
            {
                if (maxLength == null)
                {
                    if (BaseType != null)
                    {
                        return BaseType.MaxLength;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return maxLength.Value;
                }
            }
            set
            {
                maxLength = value;
            }
        }

        public bool MaxLengthIsInherited
        {
            get { return !maxLength.HasValue; }
        }

        public bool CanBeNull
        {
            get
            {
                if (canBeNull == null)
                {
                    if (BaseType != null)
                    {
                        return BaseType.CanBeNull;
                    }
                    else
                    {
                        throw CreateNoValueException("CanBeNull");
                    }
                }
                else
                {
                    return canBeNull.Value;
                }
            }
            set
            {
                canBeNull = value;
            }        
        }

        public bool CanBeNullIsInherited
        {
            get { return !canBeNull.HasValue; }
        }

        public bool IsDbGenerated
        {
            get
            {
                if (isDbGenerated == null)
                {
                    if (BaseType != null)
                    {
                        return BaseType.IsDbGenerated;
                    }
                    else
                    {
                        throw CreateNoValueException("IsDbGenerated");
                    }
                }
                else
                {
                    return isDbGenerated.Value;
                }
            }
            set
            {
                isDbGenerated = value;
            }
        }

        public bool IsDbGeneratedIsInherited
        {
            get { return !isDbGenerated.HasValue; }
        }

        public string EnumTypeName
        {
            get
            {
                if (enumTypeName == null)
                {
                    if (BaseType != null)
                    {
                        return BaseType.EnumTypeName;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return enumTypeName;
                }
            }
            set
            {
                enumTypeName = value;
            }
        }

        public bool EnumTypeNameIsInherited
        {
            get { return enumTypeName == null; }
        }


        public int? Precision
        {
            get
            {
                if (precision == null)
                {
                    if (BaseType != null)
                    {
                        return BaseType.Precision;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return precision.Value;
                }
            }
            set
            {
                precision = value;
            }
        }

        public bool PrecisionIsInherited
        {
            get { return !precision.HasValue; }
        }

        public int? Scale
        {
            get
            {
                if (scale == null)
                {
                    if (BaseType != null)
                    {
                        return BaseType.Scale;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return scale.Value;
                }
            }
            set
            {
                scale = value;
            }
        }

        public bool ScaleIsInherited
        {
            get { return !scale.HasValue; }
        }


        private Exception CreateNoValueException(string property)
        {
            return new InvalidOperationException(property + " is not defined for column type " + Name);
        }





        public void Verify(IVerificationContext context)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                context.Add(new VerificationMessage(Severity.Error, Properties.Resources.ColumnTypeNameEmpty));
            }

            if (!string.IsNullOrWhiteSpace(EnumTypeName) && (Precision.HasValue || Scale.HasValue))
            {
                context.Add(new VerificationMessage(Severity.Warning, 
                    string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.ColumnTypeWithEnumHasPrecisionScale, Name)));
            }
        }

        public static IEqualityComparer<ColumnType> NameComparer
        {
            get { return new NameComparerClass(); }
        }

        private class NameComparerClass : IEqualityComparer<ColumnType>
        {
            public bool Equals(ColumnType x, ColumnType y)
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(x.Name, y.Name) == 0;
            }

            public int GetHashCode(ColumnType obj)
            {
                return obj.GetHashCode();
            }
        }

    }
}
