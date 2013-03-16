using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.Library.Database.Schema;

namespace DataSchemaGui.DetailPages
{
    public partial class ColumnTypePage : BasePage
    {
        private class ColumnTypeSelector : DataSelector<string, ColumnType>
        {
        }

        private ColumnTypeSelector emptyColumnTypeSelector;

        public ColumnTypePage()
        {
            InitializeComponent();
            Caption = "Column type";
            emptyColumnTypeSelector = new ColumnTypeSelector { Id = null, Object = null, Text = "(none)" };
        }

        private ColumnType ColumnType
        {
            get { return (ColumnType)DataObject; }
        }


        protected override void LoadDataObject()
        {
            textBoxName.Text = ColumnType.Name;
            textBoxDescription.Text = ColumnType.Description;
            PopulateBaseTypes();
            ColumnTypeSelector.SetComboSelection(comboBoxBaseType, ColumnType.BaseType);
            textBoxMaxLength.Text = ColumnType.MaxLength.HasValue ? ColumnType.MaxLength.Value.ToString() : string.Empty;
            checkBoxMaxLengthIsInherited.Checked = ColumnType.MaxLengthIsInherited;
            checkBoxCanBeNull.Checked = ColumnType.CanBeNull;
            checkBoxCanBeNullIsInherited.Checked = ColumnType.CanBeNullIsInherited;
            checkBoxIsDbGenerated.Checked = ColumnType.IsDbGenerated;
            checkBoxIsDbGeneratedIsInherited.Checked = ColumnType.IsDbGeneratedIsInherited;
            textBoxEnumTypeName.Text = ColumnType.EnumTypeName;
            checkBoxEnumTypeNameIsInherited.Checked = ColumnType.EnumTypeNameIsInherited;
            textBoxPrecision.Text = ColumnType.Precision.HasValue ? ColumnType.Precision.Value.ToString() : string.Empty;
            checkBoxPrecisionIsInherited.Checked = ColumnType.PrecisionIsInherited;
            textBoxScale.Text = ColumnType.Scale.HasValue ? ColumnType.Scale.Value.ToString() : string.Empty;
            checkBoxScaleIsInherited.Checked = ColumnType.ScaleIsInherited;

            TargetsToDataTable();
            bindingSource.DataSource = dataTableTargets;
            dataGridViewTargets.DataSource = null;
            dataGridViewTargets.DataSource = bindingSource;

            IsDataChanged = false;
        }

        private void PopulateBaseTypes()
        {
            comboBoxBaseType.BeginUpdate();
            comboBoxBaseType.Items.Clear();

            comboBoxBaseType.Items.Add(emptyColumnTypeSelector);
            var selectors =
                from columnType in ColumnType.DataSchema.ColumnTypes
                orderby columnType.Name
                select new ColumnTypeSelector
                {
                    Id = columnType.Name,
                    Object = columnType,
                    Text = columnType.Name
                };
            comboBoxBaseType.Items.AddRange(selectors.ToArray());

            comboBoxBaseType.EndUpdate();
        }

        private void TargetsToDataTable()
        {
            dataTableTargets.Rows.Clear();
            foreach (var target in ColumnType.Targets.OrderBy(t => t.Key))
            {
                var row = dataTableTargets.NewRow();
                row[0] = target.TargetSystem.Name;
                row[1] = target.DataType;
                row[2] = target.DataTypeWhenReferenced;
                dataTableTargets.Rows.Add(row);
            }
        }


    }
}
