using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Database.Schema.Verification;
using System.Collections.Specialized;

namespace Knightrunner.Library.Database.Schema
{
    public class Column
    {
        private bool? canBeNull;
        private string description;

        public string Name { get; set; }
        public ColumnType ColumnType { get; set; }
        public bool CanBeNull
        {
            get
            {
                if (!canBeNull.HasValue)
                    return ColumnType.CanBeNull;
                else
                    return canBeNull.Value;
            }
            set
            {
                canBeNull = value;
            }
        }

        public bool InPrimaryKey { get; set; }
        public string Description
        {
            get
            {
                if (description == null)
                {
                    var columnType = this.ColumnType;
                    while (columnType.Description == null && columnType.BaseType != null)
                    {
                        columnType = columnType.BaseType;
                    }
                    return columnType.Description;
                }
                else
                {
                    return description;
                }
            }
            set
            {
                description = value;
            }
        }

        public void Verify(IVerificationContext context, string tableName)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                context.Add(new VerificationTableMessage(Severity.Error, tableName, Properties.Resources.ColumnNameEmpty));
            }
        }

        /// <summary>
        /// Gets the effective target of this column given the specified targetSystem.
        /// </summary>
        /// <param name="targetSystem"></param>
        /// <returns></returns>
        public Target GetEffectiveTarget(TargetSystem targetSystem)
        {
            ColumnType columnType = this.ColumnType;
            Target target = null;

            while (target == null && columnType != null)
            {
                target = columnType.Targets[targetSystem.Name];
                if (target == null)
                {
                    columnType = columnType.BaseType;
                }
            }

            return target;
        }



    }
}
