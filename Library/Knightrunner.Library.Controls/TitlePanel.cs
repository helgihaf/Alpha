using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Knightrunner.Library.Core;

namespace Knightrunner.Library.Controls
{
    /// <summary>
    /// Represents a Windows control that displays a rounded frame around a group of controls with a gradiant
    /// background and a highly visible title bar.
    /// </summary>
    [Docking(DockingBehavior.Ask)]
    [DefaultEvent("Paint")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [DefaultProperty("Text")]
    [Description("A group box with a thick title bar")]
    public class TitlePanel : ScrollableControl
    {
        const int FixedTextPadding = 12;
        private const int TextLeft = 10;

        private int fontHeight = -1;
        private Font cachedFont;

        private PictureBox titlePictureBox;
        private Label titleLabel;

        // Frequently used graphic objects
        private LinearGradientBrush headerBrush;
        private Pen headerPen;
        private SolidBrush solidBrush;
        private LinearGradientBrush gradientBrush;
        private Pen categoryPen;


        public TitlePanel()
        {
            this.TabStop = false;
            base.SetStyle(ControlStyles.Selectable, false);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            titleLabel = new Label();
            this.Controls.Add(titleLabel);
            titleLabel.AutoSize = true;
            titleLabel.Location = new System.Drawing.Point(6, 11);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(35, 13);
            titleLabel.BackColor = Color.Transparent;

            titlePictureBox = new PictureBox();
            this.Controls.Add(titlePictureBox);
            titlePictureBox.Location = new System.Drawing.Point(6, 3);
            titlePictureBox.Name = "titlePictureBox";
            titlePictureBox.Size = new System.Drawing.Size(16, 16);
            titlePictureBox.TabStop = false;
            titlePictureBox.Visible = false;
            titlePictureBox.BackColor = Color.Transparent;

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "solidBrush"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "headerPen"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "headerBrush"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "gradientBrush"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "categoryPen")]
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                DisposeFrequentlyUsedGraphicObjects();
            }
        }

        [Description("Image to show in front of title text"), Category("Appearance"), Localizable(true), DefaultValue(null)]
        public Image Image
        {
            get { return titlePictureBox.Image; }
            set
            {
                if (object.ReferenceEquals(titlePictureBox.Image, value))
                {
                    return;
                }

                titlePictureBox.Image = value;
                OnResize(EventArgs.Empty);
            }

        }


        [Localizable(true)]
        public override string Text
        {
            get
            {
                return titleLabel.Text;
            }
            set
            {
                if (titleLabel.Text == value)
                {
                    return;
                }
                titleLabel.Text = value;
                OnResize(EventArgs.Empty);
            }
        }

        [Description("The font used to display the title of the control"), Category("Appearance"), Localizable(true)]
        public Font TitleFont
        {
            get
            {
                return titleLabel.Font;
            }

            set
            {
                if (object.ReferenceEquals(titleLabel.Font, value))
                {
                    return;
                }

                titleLabel.Font = value;
            }
        }


        protected override Padding DefaultPadding
        {
            get
            {
                return new Padding(3);
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 100);
            }
        }


        [DefaultValue(false)]
        public new bool TabStop
        {
            get
            {
                return base.TabStop;
            }
            set
            {
                base.TabStop = value;
            }
        }



        public override Rectangle DisplayRectangle
        {
            get
            {
                Size clientSize = base.ClientSize;
                AssertTitleFontHeight();
                Padding padding = base.Padding;
                return new Rectangle(padding.Left, this.fontHeight + padding.Top + FixedTextPadding, Math.Max(clientSize.Width - padding.Horizontal, 0), Math.Max(clientSize.Height - this.fontHeight - padding.Vertical - FixedTextPadding, 0));
            }
        }

        private void AssertTitleFontHeight()
        {
            if (this.fontHeight == -1)
            {
                this.fontHeight = this.TitleFont.Height;
                this.cachedFont = this.TitleFont;
            }
            else if (!object.ReferenceEquals(this.cachedFont, this.TitleFont))
            {
                this.fontHeight = this.TitleFont.Height;
                this.cachedFont = this.TitleFont;
            }

            if (this.headerPen == null)
            {
                CalculateSizes();
                CreateFrequentlyUsedGraphicObjects();
            }
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            AssertTitleFontHeight();

            PaintBackground(g);
            PaintHeader(g);
        }

        private void PaintBackground(Graphics g)
        {
            g.DrawPath(categoryPen, categoryPath);
        }


        private void PaintHeader(Graphics g)
        {
            g.FillPath(headerBrush, headerPath);

            g.DrawPath(headerPen, headerPath);
        }

        private RectangleF bounds;
        private GraphicsPath headerPath;
        private int headerHeight;

        private GraphicsPath categoryPath;

        private void CalculateSizes()
        {
            this.bounds = new RectangleF(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);

            this.headerHeight = fontHeight + FixedTextPadding;
            this.headerPath = PathHelper.GetTopRoundedRect(new RectangleF(bounds.Left, bounds.Top, bounds.Width, headerHeight), 8);

            categoryPath = PathHelper.GetRoundRect(bounds, 8);

        }

        private void CreateFrequentlyUsedGraphicObjects()
        {
            this.headerBrush = new LinearGradientBrush(
                new Point((int)bounds.Left, (int)bounds.Top),
                new Point((int)bounds.Left, (int)bounds.Top + headerHeight),
                Color.FromArgb(240, 240, 240),
                Color.FromArgb(222, 222, 222));

            this.headerPen = new Pen(Color.FromArgb(164, 164, 164));

            this.solidBrush = new SolidBrush(this.BackColor);
            this.gradientBrush = new LinearGradientBrush(
                            new Point((int)bounds.Left, (int)bounds.Top),
                            new Point((int)bounds.Left, (int)bounds.Bottom),
                            Color.White,
                            Color.FromArgb(240, 240, 240));
            this.categoryPen = new Pen(Color.FromArgb(164, 164, 164));
        }


        private void DisposeFrequentlyUsedGraphicObjects()
        {
            Utilities.DisposeAndNull(ref this.headerBrush);
            Utilities.DisposeAndNull(ref this.headerPen);
            Utilities.DisposeAndNull(ref this.solidBrush);
            Utilities.DisposeAndNull(ref this.gradientBrush);
            Utilities.DisposeAndNull(ref this.categoryPen);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CalculateSizes();

            int textInset = TextLeft;
            if (Image != null)
            {
                textInset += Image.Width + 6;
                this.titlePictureBox.Left = TextLeft;
                this.titlePictureBox.Top = FixedTextPadding / 2;
                this.titlePictureBox.Visible = true;
            }
            else
            {
                this.titlePictureBox.Visible = false;
            }

            this.titleLabel.Location = new Point(textInset, FixedTextPadding / 2);
            this.titleLabel.Size = new Size((int)bounds.Width - textInset, this.fontHeight);
        }

    }

}
