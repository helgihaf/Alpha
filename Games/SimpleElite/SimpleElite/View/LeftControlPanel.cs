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
    public partial class LeftControlPanel : UserControl
    {
        public LeftControlPanel()
        {
            InitializeComponent();
        }

        public void UpdateView(Model.Ship ship)
        {
            measureBarFrontShield.Value = ship.FrontShield;
            measureBarRearShield.Value = ship.RearShield;
            measureBarFuel.Value = ship.Fuel;
            measureBarCabinTemperature.Value = ship.CabinTemperature;
            measureBarLaserTemperature.Value = ship.LaserTemperature;
            measureBarAltitute.Value = ship.Altitute;
        }
    }
}
