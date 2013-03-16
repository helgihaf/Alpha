using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Knightrunner.Library.Database.Schema;
using DataSchemaGui.Shapes;

namespace DataSchemaGui
{
    public partial class Diagram : UserControl
    {
        private DataSchema dataSchema;

        public Diagram()
        {
            InitializeComponent();
        }


        public DataSchema DataSchema
        {
            get { return dataSchema; }
            set
            {
                if (!object.ReferenceEquals(dataSchema, value))
                {
                    dataSchema = value;
                    RefreshSchema();
                }
            }
        }

        private const int DefaultSpacing = 10;
        private const int DefaultWidth = 300;
        private const int DefaultHeight = 300;

        private void RefreshSchema()
        {
            ClearShapes();

            int row = 0;
            int col = 0;
            int colsPerRow = this.ClientRectangle.Width / (DefaultSpacing * 2 + DefaultWidth);

            foreach (var table in dataSchema.Tables)
            {
                TableShape tableShape = new TableShape();
                tableShape.Table = table;
                tableShape.Location = new Point
                (
                    DefaultSpacing + col * (DefaultSpacing + DefaultWidth),
                    DefaultSpacing + row * (DefaultSpacing + DefaultHeight)
                );
                tableShape.Size = new Size(DefaultWidth, DefaultHeight);
                tableShape.BackColor = SystemColors.Control;

                this.Controls.Add(tableShape);

                col++;
                if (col == colsPerRow)
                {
                    row++;
                    col = 0;
                }
            }
        }

        private void ClearShapes()
        {
            int index = 0;
            while (index < this.Controls.Count)
            {
                Shape shape = this.Controls[index] as Shape;
                if (shape != null)
                {
                    this.Controls.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
        }
    }
}
