using Knightrunner.Library.Drawing3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class Ship
    {
        /// <summary>
        /// The location in space of the ship.
        /// </summary>
        public Vertex Location { get; set; }

        /// <summary>
        /// If the ship is locatated at (0,0,0) the ForwardDirection point marks in which direction the ship is turned. In other words, the line (0,0,0) to (ForwardDirection) goes from the stern of the ship throuhg the bow of the ship.
        /// </summary>
        public Vertex ForwardDirection { get; set; }

        /// <summary>
        /// If the ship is locatated at (0,0,0) the RightDirection point marks the right direction of the ship. RightDirection is perpendicular to ForwardDirection.
        /// </summary>
        public Vertex RightDirection { get; set; }

        ///// <summary>
        ///// The rotation of the ship around its direction axis.
        ///// </summary>
        //public double Rotation { get; set; }
    }
}
