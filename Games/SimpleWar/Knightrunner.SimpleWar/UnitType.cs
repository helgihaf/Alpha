using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.SimpleWar
{
    public class UnitType
    {
        public string Name { get; set; }
        public UnitClass UnitClass { get; set; }

        public int Attack { get; set; }
        public int Range { get; set; }
        public int Defense { get; set; }
        public int Movement { get; set; }
    }
}
