using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DataSchemaGui.Shapes
{
    public partial class Shape : UserControl
    {
        private int borderWidth = 1;
        private ShapeState shapeState;

        private Point? dragStartLocation;

        private enum DragMethod
        {
            None,
            Move,
            North,
            South,
            West,
            East,
            NW,
            NE,
            SW,
            SE
        }
        private DragMethod dragMethod;

        public string Id { get; set; }

        public Color BorderColor { get; set; }

        private static Pen selectPen = new Pen(new HatchBrush(HatchStyle.Percent50, Color.Black, Color.White));

        public Shape()
        {
            InitializeComponent();
            BorderColor = Color.Black;
            MinimumSize = new Size(4, 4);
        }

        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                if (borderWidth != value)
                {
                    borderWidth = value;
                    Invalidate();
                }
            }
        }

        public ShapeState ShapeState
        {
            get { return shapeState; }
            set
            {
                if (shapeState != value)
                {
                    shapeState = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Background stuff first

            // Then main stuff
            //Draw(e);

            // Then foreground stuff
            //DrawText(e.Graphics, e.ClipRectangle);
            if (ShapeState == ShapeState.Selected)
            {
                DrawSelected(e);
            }
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == System.Windows.Forms.MouseButtons.None)
            {
                this.Cursor = DragMethodToCursor(GetDragMethod(e.Location));
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (dragStartLocation.HasValue)
                {
                    AdjustLocationOrShape(e.Location);
                    if (dragMethod == DragMethod.None)
                    {
                        dragMethod = GetDragMethod(e.Location);
                    }
                }
            }

        }

        private void AdjustLocationOrShape(Point mouseLocation)
        {
            Point mouseScreenLocation = PointToScreen(mouseLocation);
            Point newScreenLocation = new Point(mouseScreenLocation.X - dragStartLocation.Value.X, mouseScreenLocation.Y - dragStartLocation.Value.Y);

            Point mouseClientLocation = this.Parent.PointToClient(mouseScreenLocation);

            switch (dragMethod)
            {
                case DragMethod.None:
                    break;
                case DragMethod.Move:
                    this.Location = this.Parent.PointToClient(newScreenLocation);
                    break;
                case DragMethod.North:
                    {
                        int delta = this.Top - mouseClientLocation.Y;
                        this.Top = mouseClientLocation.Y;
                        this.Height += delta;
                        Invalidate();
                    }
                    break;
                case DragMethod.South:
                    this.Height = mouseLocation.Y;
                    Invalidate();
                    break;
                case DragMethod.West:
                    {
                        int delta = this.Left - mouseClientLocation.X;
                        this.Left = mouseClientLocation.X;
                        this.Width += delta;
                        Invalidate();
                    }
                    break;
                case DragMethod.East:
                    this.Width = mouseLocation.X;
                    Invalidate();
                    break;
                case DragMethod.NW:
                    {
                        int delta = this.Top - mouseClientLocation.Y;
                        this.Top = mouseClientLocation.Y;
                        this.Height += delta;
                        delta = this.Left - mouseClientLocation.X;
                        this.Left = mouseClientLocation.X;
                        this.Width += delta;
                        Invalidate();
                    }
                    break;
                case DragMethod.NE:
                    {
                        int delta = this.Top - mouseClientLocation.Y;
                        this.Top = mouseClientLocation.Y;
                        this.Height += delta;
                        this.Width = mouseLocation.X;
                        Invalidate();
                    }
                    break;
                case DragMethod.SW:
                    {
                        this.Height = mouseLocation.Y;
                        int delta = this.Left - mouseClientLocation.X;
                        this.Left = mouseClientLocation.X;
                        this.Width += delta;
                        Invalidate();
                    }
                    break;
                case DragMethod.SE:
                    this.Height = mouseLocation.Y;
                    this.Width = mouseLocation.X;
                    Invalidate();
                    break;
                default:
                    break;
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (GetDragMethod(e.Location) != DragMethod.None)
                {
                    dragStartLocation = e.Location;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            dragStartLocation = null;
            dragMethod = DragMethod.None;
        }

        private Cursor[] cursorsByDragMethod = new Cursor[]
        {
            // DragMethod.None:
            Cursors.Default,
            // DragMethod.Move:
            Cursors.Hand,
            // DragMethod.North:
            Cursors.SizeNS,
            // DragMethod.South:
            Cursors.SizeNS,
            // DragMethod.West:
            Cursors.SizeWE,
            // DragMethod.East:
            Cursors.SizeWE,
            // DragMethod.NW:
            Cursors.SizeNWSE,
            // DragMethod.NE:
            Cursors.SizeNESW,
            // DragMethod.SW:
            Cursors.SizeNESW,
            // DragMethod.SE:
            Cursors.SizeNWSE
        };

        private System.Windows.Forms.Cursor DragMethodToCursor(DragMethod method)
        {
            return cursorsByDragMethod[(int)method];

        }


        private DragMethod GetDragMethod(Point point)
        {
            DragMethod result = DragMethod.None;

            int rightMargin = this.Width - this.Padding.Right;
            int bottomMargin = this.Height - this.Padding.Bottom;

            if (CanResize())
            {
                if (point.X < this.Padding.Left)
                {
                    if (point.Y < this.Padding.Top)
                    {
                        result = DragMethod.NW;
                    }
                    else if (point.Y >= bottomMargin)
                    {
                        result = DragMethod.SW;
                    }
                    else
                    {
                        result = DragMethod.West;
                    }
                }
                else if (point.X >= rightMargin)
                {
                    if (point.Y < this.Padding.Top)
                    {
                        result = DragMethod.NE;
                    }
                    else if (point.Y >= bottomMargin)
                    {
                        result = DragMethod.SE;
                    }
                    else
                    {
                        result = DragMethod.East;
                    }
                }
                else if (point.Y < this.Padding.Top)
                {
                    result = DragMethod.North;
                }
                else if (point.Y >= bottomMargin)
                {
                    result = DragMethod.South;
                }
            }
            
            if (result == DragMethod.None && CanMove(point))
            {
                if (CanMove(point))
                {
                    result = DragMethod.Move;
                }
            }

            return result;
        }

        protected virtual bool CanMove(Point point)
        {
            return true;
        }

        protected virtual bool CanResize()
        {
            return true;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (dragMethod == DragMethod.None)
            {
                switch (ShapeState)
                {
                    case ShapeState.Normal:
                        ShapeState = ShapeState.Selected;
                        //OnSelected();
                        break;
                    case ShapeState.Selected:
                        ShapeState = ShapeState.Normal;
                        break;
                    default:
                        break;
                }
            }
        }


        protected void DrawRectangle(Graphics g, Pen pen, Rectangle rectangle)
        {
            int penWidth = Convert.ToInt32(pen.Width);
            int adjustment = penWidth / 2;
            int x = rectangle.Left + adjustment;
            int y = rectangle.Top + adjustment;
            int width = rectangle.Size.Width - penWidth;
            int height = rectangle.Size.Height - penWidth;
            g.DrawRectangle(pen, x, y, width, height);
        }

        private void DrawSelected(System.Windows.Forms.PaintEventArgs e)
        {
            //DrawRectangle(e.Graphics, selectPen, this.DisplayRectangle);
            DrawRectangle(e.Graphics, selectPen, this.ClientRectangle);
        }


        protected void AddEventForwarding(Control control)
        {
            control.MouseMove += new MouseEventHandler(ChildControlMouseMove);
            control.MouseDown += new MouseEventHandler(ChildControlMouseDown);
            control.MouseUp += new MouseEventHandler(ChildControlMouseUp);
        }

        protected void RemoveEventForwarding(Control control)
        {
            control.MouseMove -= new MouseEventHandler(ChildControlMouseMove);
            control.MouseDown -= new MouseEventHandler(ChildControlMouseDown);
            control.MouseUp -= new MouseEventHandler(ChildControlMouseUp);
        }

        private void ChildControlMouseMove(object sender, MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }

        void ChildControlMouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        void ChildControlMouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }



    }


    public enum ShapeState
    {
        Normal,
        Selected,
    }

}
