using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleElite.View
{
    public partial class MeasureBar : UserControl
    {
        private BarType barType = BarType.Bar;
        private int value;

        public MeasureBar()
        {
            InitializeComponent();
            UpdateBarType();
        }

        public BarType BarType
        {
            get
            {
                return barType;
            }
            set
            {
                barType = value;
                UpdateBarType();
            }
        }

        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value != this.value && value >= 0 && value <= 100)
                {
                    this.value = value;
                    UpdateBar();
                }
            }
        }

        private void UpdateBarType()
        {
            if (barType == BarType.Bar)
            {
                panelValue.Left = 0;
            }
            else
            {
                panelValue.Width = 4;
            }
            UpdateBar();
        }

        private void UpdateBar()
        {
            if (barType == BarType.Bar)
            {
                panelValue.Width = this.Width * value / 100;
            }
            else
            {
                panelValue.Left = this.Width * value / 100;
            }
        }
    }

    public enum BarType
    {
        Bar,
        Tick
    }
}
