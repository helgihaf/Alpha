using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Knightrunner.Library.Controls
{
    public static class PathHelper
    {
        public static GraphicsPath GetRoundRect(RectangleF r, int offset)
        {
            int left = Math.Min((int)r.Left, (int)r.Right);
            int right = Math.Max((int)r.Left, (int)r.Right);

            int top = Math.Min((int)r.Top, (int)r.Bottom);
            int bottom = Math.Max((int)r.Top, (int)r.Bottom);

            GraphicsPath path = new GraphicsPath();

            try
            {
                path.AddArc(right - offset, top, offset, offset, 270.0f, 90.0f);
                path.AddArc(right - offset, bottom - offset, offset, offset, 0.0f, 90.0f);
                path.AddArc(left, bottom - offset, offset, offset, 90.0f, 90.0f);
                path.AddArc(left, top, offset, offset, 180.0f, 90.0f);
                path.CloseFigure();

                return path;
            }
            catch
            {
                path.Dispose();
                throw;
            }
        }

        //public static GraphicsPath GetBottomRoundRect(RectangleF r, int offset)
        //{
        //    int left = Math.Min((int)r.Left, (int)r.Right);
        //    int right = Math.Max((int)r.Left, (int)r.Right);

        //    int top = Math.Min((int)r.Top, (int)r.Bottom);
        //    int bottom = Math.Max((int)r.Top, (int)r.Bottom);

        //    GraphicsPath path = new GraphicsPath();

        //    path.AddLine(r.Right, r.Top, r.Right, r.Top);

        //    path.AddArc(right - offset, bottom - offset, offset, offset, 0.0f, 90.0f);

        //    path.AddArc(left, bottom - offset, offset, offset, 90.0f, 90.0f);

        //    path.AddLine(r.Left, r.Top, r.Left, r.Top);

        //    path.CloseFigure();

        //    return path;
        //}

        public static GraphicsPath GetTopRoundedRect(RectangleF r, int offset)
        {
            int left = Math.Min((int)r.Left, (int)r.Right);
            int right = Math.Max((int)r.Left, (int)r.Right);

            int top = Math.Min((int)r.Top, (int)r.Bottom);
            int bottom = Math.Max((int)r.Top, (int)r.Bottom);



            GraphicsPath path = new GraphicsPath();
            try
            {
                path.AddArc(right - offset, top, offset, offset, 270.0f, 90.0f);
                path.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);
                path.AddLine(r.Left, r.Bottom, r.Left, r.Bottom);
                path.AddArc(left, top, offset, offset, 180.0f, 90.0f);
                path.CloseFigure();

                return path;
            }
            catch
            {
                path.Dispose();
                throw;
            }
        }
    }
}
