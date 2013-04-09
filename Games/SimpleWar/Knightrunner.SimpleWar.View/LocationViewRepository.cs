using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Knightrunner.SimpleWar.View
{
    public class LocationViewRepository
    {
        private static LocationViewRepository instance = new LocationViewRepository();

        private Lazy<Dictionary<string, LocationViewInfo>> lazyDictionary = new Lazy<Dictionary<string, LocationViewInfo>>(ReadLocationInfoRepostiory);

        private LocationViewRepository()
        {
        }

        public static LocationViewRepository Instance
        {
            get
            {
                return instance;
            }
        }

        private static Dictionary<string, LocationViewInfo> ReadLocationInfoRepostiory()
        {
            var dictionary = new Dictionary<string, LocationViewInfo>(StringComparer.OrdinalIgnoreCase);

            var xdoc = XDocument.Parse(Utilities.GetResourceTextFile(Assembly.GetExecutingAssembly(), "TerrainTypesView.xml"));
            foreach (var element in xdoc.Element("TerrainTypes").Elements("TerrainType"))
            {
                var info = new LocationViewInfo
                {
                    Name = element.Element("Name").Value,
                    Color = Color.FromName(element.Element("Color").Value)
                };
                dictionary.Add(info.Name, info);
            }

            return dictionary;
        }

        public LocationViewInfo Get(Location location)
        {
            return lazyDictionary.Value[location.TerrainType.Name];
        }
    }
}
