using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Knightrunner.SimpleWar
{
    public class MapSerializer
    {
        public Map Deserialize(Stream stream)
        {
            var xdoc = XDocument.Load(stream);
            var mapElement = xdoc.Element("Map");
            int width = int.Parse(mapElement.Element("Width").Value);
            int height = int.Parse(mapElement.Element("Height").Value);
            var map = new Map(width, height);

            foreach (var locationElement in mapElement.Elements("Location"))
            {
                int row = int.Parse(locationElement.Element("Row").Value);
                int col = int.Parse(locationElement.Element("Column").Value);
                string terrainTypeName = locationElement.Element("TerrainType").Value;
                map.GetLocation(row, col).TerrainType = TerrainTypeRepository.Instance.Get(terrainTypeName);
            }

            return map;
        }

        public void Serialize(Stream stream, Map map)
        {
            var locationElements = new List<XElement>();
            foreach (var location in map.Locations)
            {
                if (!string.Equals(location.TerrainType.Name, Map.DefaultTerrainTypeName, StringComparison.OrdinalIgnoreCase))
                {
                    locationElements.Add(
                        new XElement("Location",
                            new XElement("Row", location.Row),
                            new XElement("Column", location.Column),
                            new XElement("TerrainType", location.TerrainType.Name)));
                }
            }
            var xdoc = new XDocument();
            xdoc.Add(new XElement("Map",
                new XElement("Version", "1.0.0"),
                new XElement("Width", map.Width),
                new XElement("Height", map.Height),
                locationElements));

            xdoc.Save(stream);
        }
    }
}
