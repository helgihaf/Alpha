using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Drawing3D
{
    public class Box : WorldObject
    {
        public Box(float width, float height, float depth)
        {
            Vertices = new Vertex[]
            {
                new Vertex { X = -width/2, Y = height/2, Z = -depth/2 },
                new Vertex { X = width/2, Y = height/2, Z = -depth/2 },
                new Vertex { X = width/2, Y = -height/2, Z = -depth/2 },
                new Vertex { X = -width/2, Y = -height/2, Z = -depth/2 },
                new Vertex { X = -width/2, Y = height/2, Z = depth/2 },
                new Vertex { X = width/2, Y = height/2, Z = depth/2 },
                new Vertex { X = width/2, Y = -height/2, Z = depth/2 },
                new Vertex { X = -width/2, Y = -height/2, Z = depth/2 },
            };

            Triangles = new Triangle[]
            {
                // Front
                new Triangle { A = 0, B = 1, C = 2, EdgeMask = EdgeMask.ABC },
                new Triangle { A = 2, B = 3, C = 0, EdgeMask = EdgeMask.ABC },

                // Back
                new Triangle { A = 5, B = 4, C = 7, EdgeMask = EdgeMask.ABC },
                new Triangle { A = 7, B = 6, C = 5, EdgeMask = EdgeMask.ABC },
                
                // Top
                new Triangle { A = 0, B = 4, C = 5, EdgeMask = EdgeMask.ABC },
                new Triangle { A = 5, B = 1, C = 0, EdgeMask = EdgeMask.ABC },
                
                // Right side
                new Triangle { A = 1, B = 5, C = 6, EdgeMask = EdgeMask.ABC },
                new Triangle { A = 6, B = 2, C = 1, EdgeMask = EdgeMask.ABC },
                
                // Bottom
                new Triangle { A = 3, B = 2, C = 6, EdgeMask = EdgeMask.ABC },
                new Triangle { A = 6, B = 7, C = 3, EdgeMask = EdgeMask.ABC },
                
                // Right
                new Triangle { A = 4, B = 0, C = 3, EdgeMask = EdgeMask.ABC },
                new Triangle { A = 3, B = 7, C = 4, EdgeMask = EdgeMask.ABC },
            };
        }
   }
}
