using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Drawing3D
{
    public class Rotation
    {
        public static Rotation Zero = new Rotation();

        // Angles in radians (0 to 2*PI)
        public float AngleX;
        public float AngleY;
        public float AngleZ;

        public bool IsZero()
        {
            return AngleX == 0 && AngleY == 0 && AngleZ == 0;
        }

        public override bool Equals(object obj)
        {
            Rotation other = (Rotation)obj;
            return AngleX == other.AngleX && AngleY == other.AngleY && AngleZ == other.AngleZ;
        }

        public override int GetHashCode()
        {
            return AngleHash(AngleX) ^ AngleHash(AngleX) ^ AngleHash(AngleZ);
        }

        private static int AngleHash(float a)
        {
            return Convert.ToInt32(a / (Math.PI * 2) * int.MaxValue);
        }


        public Matrix GetXMatrix()
        {
            float cos = (float)Math.Cos(AngleX);
            float sin = (float)Math.Sin(AngleX);

            return new Matrix
            (
                new double[,]
                {
                    { 1, 0, 0 },
                    { 0, cos, -sin },
                    { 0, sin, cos }
                }
            );
        }

        public Matrix GetYMatrix()
        {
            float cos = (float)Math.Cos(AngleY);
            float sin = (float)Math.Sin(AngleY);

            return new Matrix
            (
                new double[,]
                {
                    { cos, 0, sin },
                    { 0, 1, 0 },
                    { -sin, 0, cos }
                }
            );
        }


        public Matrix GetZMatrix()
        {
            float cos = (float)Math.Cos(AngleZ);
            float sin = (float)Math.Sin(AngleZ);

            return new Matrix
            (
                new double[,]
                {
                    { cos, -sin, 0 },
                    { sin, cos, 0 },
                    { 0, 0, 1 }
                }
            );
        }

        public static Vertex RotateX(Vertex vertex, double angle)
        {
            Vertex result = new Vertex(vertex);

            if (angle != 0)
            {
                var sin = Math.Sin(angle);
                var cos = Math.Cos(angle);
                result.Y = (float)(vertex.Y * cos - vertex.Z * sin);
                result.Z = (float)(vertex.Z * cos + vertex.Y * sin);
            }

            return result;
        }

        public static Vertex RotateY(Vertex vertex, double angle)
        {
            Vertex result = new Vertex(vertex);

            if (angle != 0)
            {
                var sin = Math.Sin(angle);
                var cos = Math.Cos(angle);
                result.X = (float)(vertex.Z * sin + vertex.X * cos);
                result.Z = (float)(vertex.Z * cos - vertex.X * sin);
            }

            return result;
        }

        public static Vertex RotateZ(Vertex vertex, double angle)
        {
            Vertex result = new Vertex(vertex);

            if (angle != 0)
            {
                var sin = Math.Sin(angle);
                var cos = Math.Cos(angle);
                result.X = (float)(vertex.X * cos - vertex.Y * sin);
                result.Y = (float)(vertex.Y * cos + vertex.X * sin);
            }

            return result;
        }

        public static Vertex RotateArbitrary(Vertex vertex, Vertex axisVertex, double angle)
        {
            double axisX2 = axisVertex.X * axisVertex.X;
            double axisY2 = axisVertex.Y * axisVertex.Y;
            double axisZ2 = axisVertex.Z * axisVertex.Z;
            double axisSumOfSquares = axisX2 + axisY2 + axisZ2;

            Matrix rotationMatrix = new Matrix(4, 4);
            rotationMatrix[0,0] = (axisX2 + (axisY2 + axisZ2) * Math.Cos(angle)) / axisSumOfSquares;
            rotationMatrix[0,1] = (axisVertex.X * axisVertex.Y * (1 - Math.Cos(angle)) - axisVertex.Z * Math.Sqrt(axisSumOfSquares) * Math.Sin(angle)) / axisSumOfSquares;
            rotationMatrix[0,2] = (axisVertex.X * axisVertex.Z * (1 - Math.Cos(angle)) + axisVertex.Y * Math.Sqrt(axisSumOfSquares) * Math.Sin(angle)) / axisSumOfSquares;
            rotationMatrix[0,3] = 0.0;

            rotationMatrix[1,0] = (axisVertex.X * axisVertex.Y * (1 - Math.Cos(angle)) + axisVertex.Z * Math.Sqrt(axisSumOfSquares) * Math.Sin(angle)) / axisSumOfSquares;
            rotationMatrix[1,1] = (axisY2 + (axisX2 + axisZ2) * Math.Cos(angle)) / axisSumOfSquares;
            rotationMatrix[1,2] = (axisVertex.Y * axisVertex.Z * (1 - Math.Cos(angle)) - axisVertex.X * Math.Sqrt(axisSumOfSquares) * Math.Sin(angle)) / axisSumOfSquares;
            rotationMatrix[1,3] = 0.0;

            rotationMatrix[2,0] = (axisVertex.X * axisVertex.Z * (1 - Math.Cos(angle)) - axisVertex.Y * Math.Sqrt(axisSumOfSquares) * Math.Sin(angle)) / axisSumOfSquares;
            rotationMatrix[2,1] = (axisVertex.Y * axisVertex.Z * (1 - Math.Cos(angle)) + axisVertex.X * Math.Sqrt(axisSumOfSquares) * Math.Sin(angle)) / axisSumOfSquares;
            rotationMatrix[2,2] = (axisZ2 + (axisX2 + axisY2) * Math.Cos(angle)) / axisSumOfSquares;
            rotationMatrix[2,3] = 0.0;

            rotationMatrix[3,0] = 0.0;
            rotationMatrix[3,1] = 0.0;
            rotationMatrix[3,2] = 0.0;
            rotationMatrix[3,3] = 1.0;

            var outputMatrix = rotationMatrix * vertex.ToSingleRowMatrix();
            return new Vertex((float)outputMatrix[0, 0], (float)outputMatrix[1, 0], (float)outputMatrix[2, 0]);
        }

        /*

float rotationMatrix[4][4];
float inputMatrix[4][1] = {0.0, 0.0, 0.0, 0.0};
float outputMatrix[4][1] = {0.0, 0.0, 0.0, 0.0}; 

void showPoint(){
    cout<<"("<<outputMatrix[0][0]<<","<<outputMatrix[1][0]<<","<<outputMatrix[2][0]<<")"<<endl;
} 

void multiplyMatrix()
{
    for(int i = 0; i < 4; i++ ){
        for(int j = 0; j < 1; j++){
            outputMatrix[i][j] = 0;
            for(int k = 0; k < 4; k++){
                outputMatrix[i][j] += rotationMatrix[i][k] * inputMatrix[k][j];
            }
        }
    }
}
void setUpRotationMatrix(float angle, float u, float v, float w)
{
    float L = (u*u + v * v + w * w);
    angle = angle * M_PI / 180.0; //converting to radian value
    float u2 = u * u;
    float v2 = v * v;
    float w2 = w * w; 

    rotationMatrix[0][0] = (u2 + (v2 + w2) * cos(angle)) / L;
    rotationMatrix[0][1] = (u * v * (1 - cos(angle)) - w * sqrt(L) * sin(angle)) / L;
    rotationMatrix[0][2] = (u * w * (1 - cos(angle)) + v * sqrt(L) * sin(angle)) / L;
    rotationMatrix[0][3] = 0.0; 

    rotationMatrix[1][0] = (u * v * (1 - cos(angle)) + w * sqrt(L) * sin(angle)) / L;
    rotationMatrix[1][1] = (v2 + (u2 + w2) * cos(angle)) / L;
    rotationMatrix[1][2] = (v * w * (1 - cos(angle)) - u * sqrt(L) * sin(angle)) / L;
    rotationMatrix[1][3] = 0.0; 

    rotationMatrix[2][0] = (u * w * (1 - cos(angle)) - v * sqrt(L) * sin(angle)) / L;
    rotationMatrix[2][1] = (v * w * (1 - cos(angle)) + u * sqrt(L) * sin(angle)) / L;
    rotationMatrix[2][2] = (w2 + (u2 + v2) * cos(angle)) / L;
    rotationMatrix[2][3] = 0.0; 

    rotationMatrix[3][0] = 0.0;
    rotationMatrix[3][1] = 0.0;
    rotationMatrix[3][2] = 0.0;
    rotationMatrix[3][3] = 1.0;
} 

int main()
{
    float angle;
    float u, v, w;
    cout<<"Enter the initial point you want to transform:";
    cin>>points.x>>points.y>>points.z;
    inputMatrix[0][0] = points.x;
    inputMatrix[1][0] = points.y;
    inputMatrix[2][0] = points.z;
    inputMatrix[3][0] = 1.0; 

    cout<<"Enter axis vector: ";
    cin>>u>>v>>w; 

    cout<<"Enter the rotating angle in degree: ";
    cin>>angle; 

    setUpRotationMatrix(angle, u, v, w);
    multiplyMatrix();
    showPoint(); 

    return 0;
}*/

    }
}
