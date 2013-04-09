using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knightrunner.SimpleWar.View
{
    public class MapView
    {
        private readonly Map map;
        private readonly PointF[] hexPoints = new PointF[]
        {
            //			X		 Y
            new PointF(0.0f,	0.0f),			// 0
            new PointF(8.66f,	5.0f),			// 1
            new PointF(8.66f,	15.0f),			// 2
            new PointF(0.0f,	20.0f),			// 3
            new PointF(-8.66f,	15.0f),			// 4
            new PointF(-8.66f,	5.0f)			// 5
        };

        private float mapScale = 4;

        //
        // Cached display data, updated in OnPaint as needed, and
        // objects frequently used to minimize object allocation.
        //
        private PointF[] scaledHexPoints;		// Scaled version of hexPoints
        // Dimensions
        private float hexWidth;			// Total width of a single hex in pixels
        private float hexStepHeight;	// The step height of a single hex, i.e. the height of a single row of hexes
        private float hexHeight; 		// Total height of a single hex

        public MapView(Map map)
        {
            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            this.map = map;
            //CalcHexPointsEastWest();
        }

        //private void CalcHexPointsNorthSouth()
        //{
        //    float c = 10;
        //    float a = c / 2;
        //    float b = (float)Math.Sin(60 * Math.PI / 180);

        //    hexPoints = new PointF[6];
        //    hexPoints[0] = new PointF(0, a + c);
        //    hexPoints[1] = new PointF(0, a);
        //    hexPoints[2] = new PointF(b, 0);
        //    hexPoints[3] = new PointF(2 * b, a);
        //    hexPoints[4] = new PointF(2 * b, a + c);
        //    hexPoints[5] = new PointF(b, 2 * c);
        //}


        //private void CalcHexPointsEastWest()
        //{
        //    float c = 10;
        //    float a = c / 2;
        //    float b = (float)Math.Sin(60 * Math.PI / 180);

        //    hexPoints = new PointF[6];
        //    hexPoints[0] = new PointF(0, b);
        //    hexPoints[1] = new PointF(a, 0);
        //    hexPoints[2] = new PointF(a + c, 0);
        //    hexPoints[3] = new PointF(2 * c, b);
        //    hexPoints[4] = new PointF(a + c, 2 * b);
        //    hexPoints[5] = new PointF(a, 2 * b);
        //}

        public Image CreateImage()
        {
            UpdateScaleAndDimensions();
            int bitmapWidth = Convert.ToInt32(hexWidth * (map.Width + 0.6));
            int bitmapHeight = Convert.ToInt32(hexStepHeight * (map.Height + 0.6));
            return new Bitmap(bitmapWidth, bitmapHeight);
        }

        public void Render(Graphics graphics, Rectangle rectangle)
        {
            UpdateScaleAndDimensions();
            graphics.FillRectangle(Brushes.Maroon, rectangle);

            float origX = hexWidth / 2;
            float origY = 0;

            float transX, transY;
            Location location;

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    transX = origX + x * hexWidth;
                    if (y % 2 == 0)
                        transX += hexWidth / 2;
                    transY = origY + y * hexStepHeight;

                    GraphicsState state = graphics.Save();
                    graphics.TranslateTransform(transX, transY);
                    location = map.GetLocation(y, x);
                    DrawLocation(graphics, location);
                    graphics.Restore(state);
                }
            }
        }

        public void RenderLocation(Location location, Graphics graphics, Rectangle rectangle)
        {
            float origX = hexWidth / 2;
            float origY = 0;

            float transX = origX + location.Column * hexWidth;
            if (location.Row % 2 == 0)
                transX += hexWidth / 2;
            float transY = origY + location.Row * hexStepHeight;

            GraphicsState state = graphics.Save();
            graphics.TranslateTransform(transX, transY);
            DrawLocation(graphics, location);
            graphics.Restore(state);
        }

        private void UpdateScaleAndDimensions()
        {
            //
            // Scale points
            //
            scaledHexPoints = (PointF[])(hexPoints.Clone());
            for (int i = 0; i < scaledHexPoints.Length; i++)
            {
                scaledHexPoints[i].X *= mapScale;
                scaledHexPoints[i].Y *= mapScale;
            }

            //
            // Hex dimensions
            //
            hexWidth = scaledHexPoints[1].X - scaledHexPoints[4].X;
            hexStepHeight = scaledHexPoints[2].Y;
            hexHeight = scaledHexPoints[3].Y;
        }

        private void DrawLocation(Graphics graphics, Location location)
        {
            //
            // Draw hexes as filled polygones
            //
            var locationColor = LocationViewRepository.Instance.Get(location).Color;
            using (var locationBrush = new SolidBrush(locationColor))
            {
                graphics.FillPolygon(locationBrush, scaledHexPoints);
            }

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawPolygon(Pens.Black, scaledHexPoints);
            graphics.SmoothingMode = SmoothingMode.Default;
        }

        public Location GetMouseMapLocation(Point point)
        {
            float hexWidth = scaledHexPoints[1].X - scaledHexPoints[4].X;
            float hexStepHeight = scaledHexPoints[2].Y;
            float origX = hexWidth / 2;
            float origY = 0;

            PointF[] currentPoints;

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    currentPoints = (PointF[])(scaledHexPoints.Clone());

                    for (int i = 0; i < currentPoints.Length; i++)
                    {
                        currentPoints[i].X += origX + x * hexWidth;
                        if (y % 2 == 0)
                            currentPoints[i].X += hexWidth / 2;
                        currentPoints[i].Y += origY + y * hexStepHeight;
                    }

                    if (PointInPolygon(currentPoints, point))
                    {
                        return map.GetLocation(y, x);
                    }
                }
            }

            return null;
        }

        private bool PointInPolygon(PointF[] polygon, PointF point)
        {
            // PNPOLY - Point Inclusion in Polygon Test
            // W. Randolph Franklin (WRF) 
            // Taken from http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
            int i, j;
            bool c = false;
            for (i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if ((((polygon[i].Y <= point.Y) && (point.Y < polygon[j].Y)) ||
                     ((polygon[j].Y <= point.Y) && (point.Y < polygon[i].Y))) &&
                    (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))

                    c = !c;
            }
            return c;
        }

        public Map Map
        {
            get { return map; }
        }

    }
}
