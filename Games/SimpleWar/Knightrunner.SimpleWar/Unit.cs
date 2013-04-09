using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.SimpleWar
{
    public class Unit
    {
        public UnitType UnitType { get; set; }
        public Position Position { get; set; }
        public UnitState State { get; set; }
    }
}
