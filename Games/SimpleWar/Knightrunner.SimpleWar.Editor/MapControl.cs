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
    public partial class MapControl : UserControl
    {
        public MapView mapView;

        public MapControl()
        {
            InitializeComponent();
        }

        public void LoadMap(Map map)
        {
            mapView = new MapView(map);
            pictureBox.Image = mapView.CreateImage();
			pictureBox.Width = pictureBox.Image.Width;
			pictureBox.Height = pictureBox.Image.Height;
            mapView.Render(Graphics.FromImage(pictureBox.Image), pictureBox.ClientRectangle);
        }

        //internal void Render()
        //{
        //    mapView.Render(Graphics.FromImage(pictureBox.Image), pictureBox.ClientRectangle);
        //    pictureBox.Invalidate();
        //}

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            var location = mapView.GetMouseMapLocation(e.Location);
            if (location != null && LocationSelected != null)
            {
                var eventArgs = new LocationSelectedEventArgs { Location = location };
                LocationSelected(this, eventArgs);
            }
        }

        public void RefreshLocation(Location location)
        {
            mapView.RenderLocation(location, Graphics.FromImage(pictureBox.Image), pictureBox.ClientRectangle);
            pictureBox.Invalidate();
        }

        public event EventHandler<LocationSelectedEventArgs> LocationSelected;

        public Map Map
        {
            get { return mapView.Map; }
        }

    }

    public class LocationSelectedEventArgs : EventArgs
    {
        public Location Location { get; set; }
    }
}
