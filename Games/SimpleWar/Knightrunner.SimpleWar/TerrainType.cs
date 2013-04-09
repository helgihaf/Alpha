using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.SimpleWar
{
    public class TerrainType
    {
        public TerrainType()
        {
            MovementCost = new Dictionary<string, MovementCost>(StringComparer.OrdinalIgnoreCase);
        }

        public string Name { get; set; }
        public int Concealment { get; set; }

        public Dictionary<string, MovementCost> MovementCost { get; private set; }
    }
}
