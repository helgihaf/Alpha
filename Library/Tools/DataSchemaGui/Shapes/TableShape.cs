using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.Library.Database.Schema;

namespace DataSchemaGui.Shapes
{
    public partial class TableShape : Shape
    {
        private Table table;

        public TableShape()
        {
            InitializeComponent();

            AddEventForwarding(labelName);
        }

        public Table Table
        {
            get { return table; }
            set
            {
                if (!object.ReferenceEquals(table, value))
                {
                    this.table = value;
                    RefreshTable();
                }
            }
        }

        private void RefreshTable()
        {
            labelName.Text = table.Name;

            int rowIndex = 0;

            foreach (var column in GetColumnsByKeyState(table, true))
            {
                SetRow(rowIndex++, GetIndicatorText(table, column), GetColumnText(table, column));
            }


            foreach (var column in GetColumnsByKeyState(table, false))
            {
                SetRow(rowIndex++, GetIndicatorText(table, column), GetColumnText(table, column));
            }
        }

        private string GetIndicatorText(Table table, Column column)
        {
            List<string> indicators = new List<string>();

            if (column.InPrimaryKey)
            {
                indicators.Add("PK");
            }

            var fkIndex = table.IndexOfForeignKey(column);
            if (fkIndex >= 0)
            {
                indicators.Add("FK" + (fkIndex + 1).ToString());
            }

            return string.Join(", ", indicators);
        }


        private string GetColumnText(Table table, Column column)
        {
            return column.Name; // TODO: Improve
        }



        private void SetRow(int rowIndex, string column1, string column2)
        {
            if (rowIndex >= tableLayoutPanel.RowCount)
            {
                tableLayoutPanel.RowCount = rowIndex + 1;
            }

            var col1Label = CreateLabel();
            col1Label.Text = column1;
            tableLayoutPanel.Controls.Add(col1Label, 0, rowIndex);
            var col2Label = CreateLabel();
            col2Label.Text = column2;
            tableLayoutPanel.Controls.Add(col2Label, 1, rowIndex);
            
        }

        private Label CreateLabel()
        {
            Label label = new Label();
            label.AutoSize = false;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(label);

            return label;
        }


        private IEnumerable<Column> GetColumnsByKeyState(Table table, bool inPrimaryKey)
        {
            foreach (Column column in table.Columns)
            {
                if (column.InPrimaryKey == inPrimaryKey)
                    yield return column;
            }
        }

    }
}
