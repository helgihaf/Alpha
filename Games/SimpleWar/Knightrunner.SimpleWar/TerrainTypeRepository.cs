using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Knightrunner.SimpleWar
{
    public class TerrainTypeRepository
    {
        private static TerrainTypeRepository instance = new TerrainTypeRepository();

        private Lazy<Dictionary<string, TerrainType>> lazyDictionary = new Lazy<Dictionary<string, TerrainType>>(ReadTerrainTypeRepostiory);

        private TerrainTypeRepository()
        {
        }

        public static TerrainTypeRepository Instance
        {
            get
            {
                return instance;
            }
        }

        private static Dictionary<string, TerrainType> ReadTerrainTypeRepostiory()
        {
            var dictionary = new Dictionary<string, TerrainType>(StringComparer.OrdinalIgnoreCase);

            var xdoc = XDocument.Parse(Utilities.GetResourceTextFile(Assembly.GetExecutingAssembly(), "TerrainTypes.xml"));
            foreach (var element in xdoc.Element("TerrainTypes").Elements("TerrainType"))
            {
                var terrainType = new TerrainType
                {
                    Name = element.Element("Name").Value,
                    Concealment = int.Parse(element.Element("Concealment").Value)
                };
                foreach (var movementElement in element.Elements("MovementCost"))
                {
                    var movementCost = new MovementCost
                    {
                        UnitClass = movementElement.Element("UnitClass").Value,
                        Value = int.Parse(movementElement.Element("Value").Value)
                    };
                    terrainType.MovementCost.Add(movementCost.UnitClass, movementCost);
                }
                dictionary.Add(terrainType.Name, terrainType);
            }

            return dictionary;
        }


        public TerrainType Get(string terrainTypeName)
        {
            return lazyDictionary.Value[terrainTypeName];
        }

        public IEnumerable<TerrainType> GetAll()
        {
            return lazyDictionary.Value.Values;
        }
    }
}
