using Knightrunner.Library.Drawing3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3D
{
    public class ShipModel : WorldObject
    {
        public ShipModel(float width, float height, float depth)
        {
            Vertices = new Vertex[]
            {
                new Vertex { X = -width/2, Y = -height/2, Z = depth/2 },
                new Vertex { X = 0, Y = height/2, Z = depth/2 },
                new Vertex { X = width/2, Y = -height/2, Z = depth/2 },
                new Vertex { X = 0, Y = -height/2, Z = -depth/2 },
            };

            Triangles = new Triangle[]
            {
                // Back
                new Triangle { A = 0, B = 1, C = 2, EdgeMask = EdgeMask.ABCA },
                // Top right
                new Triangle { A = 0, B = 3, C = 1, EdgeMask = EdgeMask.ABCA },
                // Top left
                new Triangle { A = 3, B = 2, C = 1, EdgeMask = EdgeMask.ABCA },
                // Bottom
                new Triangle { A = 0, B = 2, C = 3, EdgeMask = EdgeMask.ABCA },
            };
        }

        //public ShipModel(float width, float height, float depth)
        //{
        //    Vertices = new Vertex[]
        //    {
        //        new Vertex { X = -width/2, Y = -height/2, Z = 0 },
        //        new Vertex { X = 0, Y = height/2, Z = 0 },
        //        new Vertex { X = width/2, Y = -height/2, Z = 0 },
        //    };

        //    Triangles = new Triangle[]
        //    {
        //        new Triangle { A = 0, B = 1, C = 2, EdgeMask = EdgeMask.ABCA },
        //    };
        //}

    }

}
