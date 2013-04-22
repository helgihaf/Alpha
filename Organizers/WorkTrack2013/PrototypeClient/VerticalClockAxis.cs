using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototypeClient
{
    public partial class VerticalClockAxis : UserControl
    {
        private Font hourFont;
        private Brush hourFontBrush;
        private Font minuteFont;
        private Pen majorLinePen;
        //private Pen minorLinePen;

        public VerticalClockAxis()
        {
            InitializeComponent();

            Zoom = 1;
            LeftMargin = 16;
            TopMargin = 16;

            var fontFamily = new FontFamily("Segoe UI");
            hourFont = new Font(fontFamily, 11);
            hourFontBrush = new SolidBrush(Color.FromArgb(59, 59, 59));

            minuteFont = new Font(fontFamily, 8);
            majorLinePen = new Pen(Color.FromArgb(168, 176, 184));
        }

        public double Zoom { get; set; }

        public int LeftMargin { get; set; }
        public int TopMargin { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            int hourStep = 46;
            int top = TopMargin;

            var hourSize = g.MeasureString("00", hourFont);

            for (int hour = 0; hour < 24; hour++)
            {
                g.DrawLine(majorLinePen, 0, top - 4, e.ClipRectangle.Width, top - 4);
                g.DrawString(string.Format("{0:00}", hour), hourFont, Brushes.Black, new PointF(LeftMargin, top));
                g.DrawString("00", minuteFont, Brushes.Black, new PointF(LeftMargin + hourSize.Width, top));
                top += hourStep;
            }

        }
    }
}
