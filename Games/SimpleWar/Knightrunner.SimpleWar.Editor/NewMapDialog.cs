using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knightrunner.SimpleWar.Editor
{
    public partial class NewMapDialog : Form
    {
        public class NewMapValue
        {
            public string Name { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }

        private readonly NewMapValue defaultNewMapValue = new NewMapValue
        {
            Name = string.Empty,
            Width = 20,
            Height = 20
        };

        public NewMapDialog()
        {
            InitializeComponent();
        }

        public NewMapValue Value
        {
            get
            {
                return new NewMapValue
                {
                    Width = (int)numericUpDownWidth.Value,
                    Height = (int)numericUpDownHeight.Value
                };
            }
            set
            {
                NewMapValue newValue = value != null ? value : defaultNewMapValue;
                numericUpDownWidth.Value = newValue.Width;
                numericUpDownHeight.Value = newValue.Height;
            }
        }
    }
}
