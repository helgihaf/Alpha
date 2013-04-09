using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knightrunner.SimpleWar.Editor
{
    public partial class TerrainPalletItem : UserControl
    {
        private bool isChecked;

        public TerrainPalletItem()
        {
            InitializeComponent();
        }

        public bool Checked
        {
            get { return isChecked; }
            set
            {
                if (isChecked != value)
                {
                    InternalSetChecked(value);
                    FlipSiblings();
                }
            }
        }

        private void InternalSetChecked(bool value)
        {
            isChecked = value;
            this.Invalidate();
        }

        private void FlipSiblings()
        {
            if (Parent == null)
            {
                return;
            }

            foreach (Control control in Parent.Controls)
            {
                var palletItem = control as TerrainPalletItem;
                if (palletItem != null && !object.ReferenceEquals(palletItem, this))
                {
                    palletItem.InternalSetChecked(!isChecked);
                }
            }
        }

        private void TerrainPalletItem_Paint(object sender, PaintEventArgs e)
        {
            if (isChecked)
            {
                using (var pen = new Pen(Color.Black, 4))
                {
                    e.Graphics.DrawRectangle(pen, ClientRectangle);
                }
            }
        }


        public string ItemText
        {
            get { return labelText.Text; }
            set { labelText.Text = value; }
        }

        public Color ItemColor
        {
            get { return panelColor.BackColor; }
            set { panelColor.BackColor = value; }
        }

        private void TerrainPalletItem_Click(object sender, EventArgs e)
        {
            Checked = true;
        }

        private void panelColor_Click(object sender, EventArgs e)
        {
            TerrainPalletItem_Click(this, e);
        }

        private void labelText_Click(object sender, EventArgs e)
        {
            TerrainPalletItem_Click(this, e);
        }
    }
}
