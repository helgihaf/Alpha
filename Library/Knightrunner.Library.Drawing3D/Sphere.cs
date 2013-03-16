using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Drawing3D
{
    public class Sphere : WorldObject
    {
        public Sphere(float width, float height, float depth, int meridians, int parallels)
        {
            this.Vertices = new Vertex[meridians * parallels];

            // We work our way from the north pole to the south pole, down the Y axis
            double phi = 0;
            for (int indexParallel = 0; indexParallel < parallels; indexParallel++)
            {
                double theta = 0;
                for (int indexMeridians = 0; indexMeridians < meridians; indexMeridians++)
                {
                    this.Vertices[indexParallel * meridians + indexMeridians] = new Vertex
                    {
                        X = (float)(Math.Cos(theta) * Math.Sin(phi) * width),
                        Y = (float)(Math.Sin(theta) * Math.Sin(phi) * height),
                        Z = (float)(Math.Cos(phi) * depth)
                    };

                    theta += 2 * Math.PI / meridians;
                }
                phi += Math.PI / (parallels - 1);
            }

            CreateTriangles(meridians, parallels);
        }

        private void CreateTriangles(int meridians, int parallels)
        {
            List<Triangle> triangles = new List<Triangle>();
            for (int indexParallel = 0; indexParallel < parallels - 1; indexParallel++)
            {
                for (int indexMeridians = 0; indexMeridians < meridians; indexMeridians++)
                {
                    int currentIndex = indexParallel * meridians + indexMeridians;
                    int currentIndexPlusOne = indexParallel * meridians + (indexMeridians + 1) % meridians;
                    int nextParallelIndex = (indexParallel + 1) * meridians + indexMeridians;
                    int nextParallelIndexPlusOne = (indexParallel + 1) * meridians + (indexMeridians + 1) % meridians;
                    triangles.Add(new Triangle { A = currentIndex, B = nextParallelIndex, C = nextParallelIndexPlusOne, EdgeMask = EdgeMask.ABC });
                    triangles.Add(new Triangle { A = nextParallelIndexPlusOne, B = currentIndexPlusOne, C = currentIndex, EdgeMask = EdgeMask.ABC });
                }
            }

            this.Triangles = triangles.ToArray();
        }


        private void CreateTrianglesAsDots(int meridians, int parallels)
        {
            this.Triangles = new Triangle[this.Vertices.Length];
            for (int i = 0; i < this.Triangles.Length; i++)
            {
                this.Triangles[i] = new Triangle { A = i, B = i, C = i, EdgeMask = EdgeMask.AB | EdgeMask.BC | EdgeMask.CA };
            }
        }

    }
}
