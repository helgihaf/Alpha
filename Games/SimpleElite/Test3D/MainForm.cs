using Knightrunner.Library.Drawing3D;
using SimpleElite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test3D
{
    public partial class MainForm : Form
    {
        private World world = new World();
        private View2D view = new View2D();
        private ShipModel sm;
        private Ship ship;
        private double UpDownControlAngle = 0;
        private double RollControlAngle = 0;

        public MainForm()
        {
            InitializeComponent();
            Initialize3D();
        }

        private void Initialize3D()
        {
            ship = new Ship { Location = new Vertex(), ForwardDirection = new Vertex(0, 0, -10), RightDirection = new Vertex(10,0,0) };
            sm = new ShipModel(40, 40, 90);
            sm.Location = new Vertex { X = 0, Y = 0, Z = 0 };

            world.WorldObjects.Add(sm);
            world.Eye = new Pose
            {
                Location = new Vertex { X = 0, Y = 0, Z = 230 },
                Rotation = new Rotation()
            };
            SetView();
            world.Initialize();
        }


        private void SetView()
        {
            view = new View2D { Height = this.ClientRectangle.Height, Width = this.ClientRectangle.Width, Perspective = 1024 };
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

            world.ResetCycle();
            world.Transform();
            world.CalculateNormals();
            world.Clip3D();
            world.Project(view);
            world.Draw(e.Graphics, Pens.Black, Brushes.White);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            HandleInput();
            HandleShipMovement();
            this.Invalidate();
        }

        private void HandleShipMovement()
        {
            var newForward = Rotation.RotateArbitrary(ship.ForwardDirection, ship.RightDirection, UpDownControlAngle);
            var newRight = Rotation.RotateArbitrary(ship.RightDirection, ship.ForwardDirection, RollControlAngle);

        }


        private void HandleInput()
        {
            const double ControlTick = Math.PI / 16;

            // Descend/Climb
            if (Keyboard.IsKeyDown(Keys.Up))
            {
                UpDownControlAngle -= ControlTick;
            }
            else if (Keyboard.IsKeyDown(Keys.Down))
            {
                UpDownControlAngle += ControlTick;
            }

            // Left/Right
            if (Keyboard.IsKeyDown(Keys.Left))
            {
                RollControlAngle -= ControlTick;
            }
            else if (Keyboard.IsKeyDown(Keys.Right))
            {
                RollControlAngle += ControlTick;
            }
        }

    }
}
