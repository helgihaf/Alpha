using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Knightrunner.Library.Drawing3D
{
    public class Triangle
    {
        // Indices into vertex array
        public int A;
        public int B;
        public int C;

        public EdgeMask EdgeMask;

        public WorldObject Owner;

        // A "volatile" normal vector
        public Vertex NormalVector;

        // A volatile visible flag
        public bool Visible;

        public Color Color = Color.LightGreen;
    }

    [Flags]
    public enum EdgeMask
    {
        AB = 1,
        BC = 2,
        AC = 4,
        CA = 8,
        ABC = AB | BC,
        ABCA = AB | BC | CA
    }

}
