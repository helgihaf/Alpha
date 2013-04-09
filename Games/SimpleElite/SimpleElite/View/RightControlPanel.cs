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
    public partial class RightControlPanel : UserControl
    {
        public RightControlPanel()
        {
            InitializeComponent();
        }

        public void UpdateView(Model.Ship ship)
        {
            measureBarSpeed.Value = ship.Speed;
            measureBarRightLeftRoll.Value = ship.RightLeftRoll + 50;
            measureBarDescendClimb.Value = ship.DescendClimb + 50;

            var energyBanks = CalculateEnergyBanks(ship.Energy);
            measureBarEnergy1.Value = energyBanks[0];
            measureBarEnergy2.Value = energyBanks[1];
            measureBarEnergy3.Value = energyBanks[2];
            measureBarEnergy4.Value = energyBanks[3];
        }

        private int[] CalculateEnergyBanks(int energy)
        {
            int[] energyBanks = new int[4];

            for (int i = 3; i >= 0; i--)
            {
                if (energy >= 25)
                {
                    energyBanks[i] = 100;
                }
                else
                {
                    energyBanks[i] = energy * 100 / 25;
                }
                energy -= 25;
            }

            return energyBanks;
        }
    }
}
