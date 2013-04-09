using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Knightrunner.SimpleWar.Editor
{
    public partial class MapEditor : UserControl
    {
        private bool isChanged = false;
        private MapSerializer mapSerializer = new MapSerializer();

        public MapEditor()
        {
            InitializeComponent();
        }

        public void LoadMap(Map map)
        {
            mapControl.LoadMap(map);
            IsChanged = false;
            FilePath = null;
        }

        public void LoadMap(string filePath)
        {
            Map map;
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                map = mapSerializer.Deserialize(fileStream);
            }
            mapControl.LoadMap(map);
            IsChanged = false;
            this.FilePath = filePath;
        }

        public void SaveMap()
        {
            if (FilePath == null)
            {
                throw new InvalidOperationException("FilePath has not been set");
            }

            using (var fileStream = new FileStream(FilePath, FileMode.Create))
            {
                mapSerializer.Serialize(fileStream, mapControl.Map);
            }
            IsChanged = false;
        }

        private void mapControl_LocationSelected(object sender, LocationSelectedEventArgs e)
        {
            var terrainType = terrainPallet.SelectedTerrainType;
            if (e.Location.TerrainType != terrainType)
            {
                e.Location.TerrainType = terrainType;
                mapControl.RefreshLocation(e.Location);
                IsChanged = true;
            }
        }

        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                if (isChanged != value)
                {
                    isChanged = value;
                    OnIsChangedChanged();
                }
            }
        }

        public string FilePath { get; set; }

        private void OnIsChangedChanged()
        {
            if (IsChangedChanged != null)
            {
                IsChangedChanged(this, EventArgs.Empty);
            }
        }
        
        public event EventHandler<EventArgs> IsChangedChanged;

        public Map Map
        {
            get { return mapControl.Map; }
        }
    }
}
