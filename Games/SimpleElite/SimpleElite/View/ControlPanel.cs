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
    public partial class ControlPanel : UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        public void UpdateView(Model.Ship ship)
        {
            leftControlPanel.UpdateView(ship);
            rightControlPanel.UpdateView(ship);
        }
    }
}
