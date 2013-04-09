using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Knightrunner.SimpleWar.View;

namespace Knightrunner.SimpleWar.Editor
{
    public partial class TerrainPallet : UserControl
    {
        public TerrainPallet()
        {
            InitializeComponent();

            foreach (var terrainType in TerrainTypeRepository.Instance.GetAll())
            {
                var location = new Location { TerrainType = terrainType };
                var locationView = LocationViewRepository.Instance.Get(location);
                var palletItem = new TerrainPalletItem();
                palletItem.Height = 64;
                palletItem.Dock = DockStyle.Top;

                palletItem.ItemText = terrainType.Name;
                palletItem.ItemColor = locationView.Color;
                palletItem.Tag = terrainType;
                palletItem.Checked = this.Controls.Count == 0;

                this.Controls.Add(palletItem);
            }
        }

        private Color AutoContrast(Color color)
        {
            var colorValue = ((int)color.R + (int)color.G + (int)color.B) / 3;
            if (colorValue < 128)
            {
                return Color.White;
            }
            else
            {
                return Color.Black;
            }
        }



        public TerrainType SelectedTerrainType
        {
            get
            {
                foreach (Control control in this.Controls)
                {
                    var item = control as TerrainPalletItem;
                    if (item != null && item.Checked)
                    {
                        return (TerrainType)item.Tag;
                    }
                }

                return null;
            }
        }

    }
}
