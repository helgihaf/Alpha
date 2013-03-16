using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Knightrunner.Library.Drawing3D
{
    public class World
    {
        private List<WorldObject> worldObjects = new List<WorldObject>();

        public List<WorldObject> WorldObjects
        {
            get
            {
                return worldObjects;
            }
        }

        public Pose Eye { get; set; }

        public void Initialize()
        {
            foreach (var obj in worldObjects)
            {
                foreach (var triangle in obj.Triangles)
                {
                    triangle.Owner = obj;
                }
            }
        }


        public void ResetCycle()
        {
            foreach (var obj in worldObjects)
            {
                obj.ResetCycle();
            }
        }


        public void Transform()
        {
            //Matrix eyeRotationMatrix = Eye.Rotation.GetRotationMatrix();
            Vertex eyeVertice = Eye.Location.ToInverse();

            foreach (var obj in worldObjects)
            {
                TransformObject(obj, eyeVertice);
            }
        }

        private void TransformObject(WorldObject obj, Vertex eyeVertice)
        {
            obj.BeginTransformation();

            //obj.DumpVertices("Before");
            // Rotate object around it's origin
            obj.Rotate();
            //obj.DumpVertices("After");

            // Apply the object's location to its transformed vertices
            obj.ApplyVertex(obj.Location);

            // Apply the eye's location
            obj.ApplyVertex(eyeVertice);

            // Apply the eye's rotation
            obj.Rotate(Eye.Rotation);

            obj.EndTransformation();
            //obj.DumpVertices("End");
        }

        public void CalculateNormals()
        {
            foreach (var obj in worldObjects)
            {
                obj.CalculateNormals();
            }
        }

        public void Clip3D()
        {
            // NOTE 1: Assumes CalcNormals has been called
            // Note 2: Only does hidden surface checking
            bool anyVisible = false;
            foreach (var obj in worldObjects)
            {
                foreach (Triangle triangle in obj.Triangles)
                {
                    triangle.Visible = triangle.NormalVector.Z > 0;
                    if (!anyVisible && triangle.Visible)
                    {
                        anyVisible = true;
                    }
                }
                obj.Visible = anyVisible;
            }

        }



        private int CompareTriangleDepth(Triangle a, Triangle b)
        {
            Vertex vertexA = a.Owner.TransformedVertices[a.B];
            Vertex vertexB = b.Owner.TransformedVertices[b.B];

            if (vertexA.Z > vertexB.Z)
                return -1;
            else if (vertexA.Z < vertexB.Z)
                return 1;
            else
                return 0;
        }


        public void Project(View2D view)
        {
            foreach (var obj in worldObjects)
            {
                if (obj.Visible)
                {
                    obj.Project(view);
                }
            }
        }

        public void Draw(Graphics g, Pen edgePen, Brush fillBrush)
        {
            //foreach (var obj in WorldObjects)
            //{
            //    if (obj.Visible)
            //    {
            //        obj.Draw(g, pen);
            //    }
            //}

            List<Triangle> visibleTriangles = new List<Triangle>();
            foreach (var obj in worldObjects)
            {
                foreach (var triangle in obj.Triangles)
                {
                    if (triangle.Visible)
                    {
                        visibleTriangles.Add(triangle);
                    }
                }
            }

            // Sort triangles in z order
            visibleTriangles.Sort(CompareTriangleDepth);

            // Draw triangles
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                foreach (var triangle in visibleTriangles)
                {
                    if (brush.Color != triangle.Color)
                    {
                        brush.Color = triangle.Color;
                    }
                    WorldObject.FillTriangle(g, edgePen, brush, triangle);
                }
            }
        }


    }

}
