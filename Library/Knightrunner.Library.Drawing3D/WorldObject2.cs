using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Knightrunner.Library.Drawing3D
{
    public class WorldObject2
    {
        public Vertex Location;
        public Rotation Rotation = new Rotation();

        public Vertex[] Vertices { get; set; }
        public Triangle[] Triangles { get; set; }

        public Vertex[] TransformedVertices;
        public bool Visible = true;		// ...as result of clipping
        public Point[] Points;


        public void ResetCycle()
        {
            Visible = true;
            foreach (var triangle in Triangles)
            {
                triangle.Visible = true;
            }
        }


        public void BeginTransformation()
        {
            if (TransformedVertices == null || TransformedVertices.Length != Vertices.Length)
            {
                TransformedVertices = new Vertex[Vertices.Length];
            }
            for (int i = 0; i < Vertices.Length; i++)
            {
                TransformedVertices[i] = new Vertex
                {
                    X = Vertices[i].X,
                    Y = Vertices[i].Y,
                    Z = Vertices[i].Z
                };
            }
        }


        public void EndTransformation()
        {
            // Nothing yet
        }





        public void Rotate()
        {
            Rotate(Rotation);
        }


        public void Rotate(Rotation rotation)
        {
            for (int i = 0; i < TransformedVertices.Length; i++)
            {
                TransformedVertices[i] = Rotation.RotateX(TransformedVertices[i], rotation.AngleX);
                TransformedVertices[i] = Rotation.RotateY(TransformedVertices[i], rotation.AngleY);
                TransformedVertices[i] = Rotation.RotateZ(TransformedVertices[i], rotation.AngleZ);
            }
        }

        public void RotateBlah(Rotation rotation)
        {
            var rotateX = rotation.GetXMatrix();
            var rotateY = rotation.GetYMatrix();
            var rotateZ = rotation.GetZMatrix();

            var result = rotateZ * rotateX * rotateY;
            for (int i = 0; i < TransformedVertices.Length; i++)
            {
                var verticeMatrix = TransformedVertices[i].ToSingleRowMatrix();
                var verticeResult = verticeMatrix * result;
                TransformedVertices[i].X = (float)verticeResult[0, 0];
                TransformedVertices[i].Y = (float)verticeResult[0, 1];
                TransformedVertices[i].Z = (float)verticeResult[0, 2];
            }
        }

        public void RotateOriginal(Rotation rotation)
        {
            float sin;
            float cos;
            float x, y, z;

            for (int i = 0; i < TransformedVertices.Length; i++)
            {
                x = TransformedVertices[i].X;
                y = TransformedVertices[i].Y;
                z = TransformedVertices[i].Z;

                if (rotation.AngleX != 0)
                {
                    sin = (float)Math.Sin(rotation.AngleX);
                    cos = (float)Math.Cos(rotation.AngleX);
                    y = TransformedVertices[i].Y * cos - TransformedVertices[i].Z * sin;
                    z = TransformedVertices[i].Z * cos + TransformedVertices[i].Y * sin;

                    TransformedVertices[i].Y = y;
                    TransformedVertices[i].Z = z;
                }
                if (rotation.AngleY != 0)
                {
                    sin = (float)Math.Sin(rotation.AngleY);
                    cos = (float)Math.Cos(rotation.AngleY);
                    x = TransformedVertices[i].Z * sin + TransformedVertices[i].X * cos;
                    z = TransformedVertices[i].Z * cos - TransformedVertices[i].X * sin;

                    TransformedVertices[i].X = x;
                    TransformedVertices[i].Z = z;
                }
                if (rotation.AngleZ != 0)
                {
                    sin = (float)Math.Sin(rotation.AngleZ);
                    cos = (float)Math.Cos(rotation.AngleZ);
                    x = TransformedVertices[i].X * cos - TransformedVertices[i].Y * sin;
                    y = TransformedVertices[i].Y * cos + TransformedVertices[i].X * sin;

                    TransformedVertices[i].X = x;
                    TransformedVertices[i].Y = y;
                }
            }
        }


        public void ApplyVertex(Vertex vertex)
        {
            for (int i = 0; i < TransformedVertices.Length; i++)
            {
                TransformedVertices[i].Add(vertex);
            }
        }



        public void Project(View2D view)
        {
            if (Points == null || Points.Length != TransformedVertices.Length)
            {
                Points = new Point[TransformedVertices.Length];
            }

            int screenX = view.Width / 2;
            int screenY = view.Height / 2;

            for (int i = 0; i < TransformedVertices.Length; i++)
            {
                Vertex vertex = TransformedVertices[i];

                if (Math.Abs(vertex.Z) > 0.0001)
                {
                    Points[i] = new Point
                    (
                        Convert.ToInt32(vertex.X * view.Perspective / vertex.Z) + screenX,
                        Convert.ToInt32(vertex.Y * view.Perspective / vertex.Z) + screenY
                    );
                }
                else
                {
                    Points[i] = Point.Empty;
                }
            }
        }


        public void Draw(Graphics g, Pen pen)
        {
            //for (int i = 0; i < Pairs.Length; i++)
            //{
            //    g.DrawLine(pen, Points[Pairs[i].A], Points[Pairs[i].B]);
            //}
            for (int i = 0; i < Triangles.Length; i++)
            {
                Triangle triangle = Triangles[i];
                if (triangle.Visible)
                {
                    DrawTriangle(g, pen, triangle);
                }
            }
        }

        public static void DrawTriangle(Graphics g, Pen pen, Triangle triangle)
        {
            if (triangle.A == triangle.B && triangle.B == triangle.C)
            {
                Point endPoint = new Point(triangle.Owner.Points[triangle.A].X + 1, triangle.Owner.Points[triangle.A].Y + 1);
                g.DrawLine(pen, triangle.Owner.Points[triangle.A], endPoint);
            }
            else
            {
                if ((triangle.EdgeMask & EdgeMask.AB) != 0)
                    g.DrawLine(pen, triangle.Owner.Points[triangle.A], triangle.Owner.Points[triangle.B]);

                if ((triangle.EdgeMask & EdgeMask.BC) != 0)
                    g.DrawLine(pen, triangle.Owner.Points[triangle.B], triangle.Owner.Points[triangle.C]);

                if ((triangle.EdgeMask & EdgeMask.AC) != 0)
                    g.DrawLine(pen, triangle.Owner.Points[triangle.A], triangle.Owner.Points[triangle.C]);

                if ((triangle.EdgeMask & EdgeMask.CA) != 0)
                    g.DrawLine(pen, triangle.Owner.Points[triangle.C], triangle.Owner.Points[triangle.A]);
            }
        }

        public static void FillTriangle(Graphics g, Pen edgePen, Brush fillBrush, Triangle triangle)
        {
            if (triangle.A == triangle.B && triangle.B == triangle.C)
            {
                Point endPoint = new Point(triangle.Owner.Points[triangle.A].X + 1, triangle.Owner.Points[triangle.A].Y + 1);
                g.DrawLine(edgePen, triangle.Owner.Points[triangle.A], endPoint);
            }
            else
            {
                Point[] points = new Point[]
                {
                    triangle.Owner.Points[triangle.A],
                    triangle.Owner.Points[triangle.B],
                    triangle.Owner.Points[triangle.C]
                };
                g.FillPolygon(fillBrush, points);
            }
            DrawTriangle(g, edgePen, triangle);
        }

        public void CalculateNormals()
        {
            foreach (Triangle triangle in this.Triangles)
            {
                Vertex vertexA = this.TransformedVertices[triangle.A];
                Vertex vertexB = this.TransformedVertices[triangle.B];
                Vertex vertexC = this.TransformedVertices[triangle.C];

                Vertex vectorAlpha = vertexA - vertexB;
                Vertex vectorBeta = vertexC - vertexB;
                triangle.NormalVector = vectorAlpha * vectorBeta;
            }
        }

        //public void DumpVertices(string msg)
        //{
        //    Debug.WriteLine(msg + "---------");
        //    for (int i = 0; i < TransformedVertices.Length; i++)
        //    {
        //        Debug.WriteLine("{0}: ({1:0.0}, {2:0.0}, {3:0.0})",
        //            i, TransformedVertices[i].X, TransformedVertices[i].Y, TransformedVertices[i].Z);
        //    }
        //}

    }
}
