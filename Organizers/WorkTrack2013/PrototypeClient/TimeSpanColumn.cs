using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace PrototypeClient
{
    public partial class TimeSpanColumn : UserControl
    {
        private const string defaultStartTime = "00:00";
        private const string defaultDuration = "06:00";

        private string title;
        private Color majorColor;
        public TimeSpan startTime;
        public TimeSpan duration;

        public TimeSpanColumn()
        {
            InitializeComponent();
            Zoom = 1;
            LeftMargin = 16;
            TopMargin = 16;
            majorColor = Color.Red;
            startTime = TimeSpan.Parse(defaultStartTime);
            duration = TimeSpan.Parse(defaultDuration);
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                Invalidate();
            }
        }

        [DefaultValue("Red")]
        public Color MajorColor
        {
            get { return majorColor; }
            set
            {
                majorColor = value;
                Invalidate();
            }
        }

        [DefaultValue(defaultStartTime)]
        public TimeSpan StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                Invalidate();
            }
        }

        [DefaultValue(defaultDuration)]
        public TimeSpan Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                Invalidate();
            }
        }

        public double Zoom { get; set; }
        public int LeftMargin { get; set; }
        public int TopMargin { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            int hourStep = 46;
            int x = Width / 4;
            int y = TopMargin + Convert.ToInt32(StartTime.TotalHours * hourStep);
            int width = Width / 2;
            int height = Convert.ToInt32(Duration.TotalHours * hourStep);

            Color borderColor = AdjustColor(majorColor, 0.8);
            Color gradientStartColor = majorColor;
            Color gradientStopColor = AdjustColor(majorColor, 1.5);

            var rectangle = new Rectangle(x, y, width, height);
            using (var brush = new LinearGradientBrush(rectangle, gradientStartColor, gradientStopColor, 315))
            {
                g.FillRectangle(brush, rectangle);
            }
            using (var pen = new Pen(borderColor))
            {
                g.DrawRectangle(pen, x, y, width, height);
            }
        }

        private Color AdjustColor(Color color, double p)
        {
            HSLColor hslColor = (HSLColor)color;
            hslColor.Luminosity *= p;
            return (Color)hslColor;
        }

        private int AdjustColorPart(int part, double p)
        {
            int result = Convert.ToInt32(part * p);
            if (result > 255)
            {
                result = 255;
            }

            Debug.WriteLine(part + " => " + result);
            return result;
        }
    }
}
