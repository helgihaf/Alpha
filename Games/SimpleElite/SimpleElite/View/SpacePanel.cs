using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Knightrunner.Library.Drawing3D;

namespace SimpleElite.View
{
    public partial class SpacePanel : UserControl
    {
        private World world = new World();
        private View2D view = new View2D();
        private Sphere sphere;

        public SpacePanel()
        {
            InitializeComponent();
            Initialize3D();
        }

        private void Initialize3D()
        {
            sphere = new Sphere(30, 30, 30, 30, 30);
            sphere.Location = new Vertex { X = 0, Y = 0, Z = 0 };
            sphere.Rotation = new Rotation { AngleX = (float)Math.PI / 2, AngleY = 0, AngleZ = 0 };


            world.WorldObjects.Add(sphere);
            world.Eye = new Pose
            {
                Location = new Vertex { X = 0, Y = 0, Z = -130 },
                Rotation = new Rotation()
            };
            SetView();
            world.Initialize();
        }

        private void SetView()
        {
            view = new View2D { Height = this.ClientRectangle.Height, Width = this.ClientRectangle.Width, Perspective = 1024 };
        }

        private void SpacePanel_Paint(object sender, PaintEventArgs e)
        {
            world.ResetCycle();
            world.Transform();
            world.CalculateNormals();
            world.Clip3D();
            world.Project(view);
            world.Draw(e.Graphics, Pens.White, Brushes.LightGreen);
        }

        public void UpdateView(Model.Ship ship)
        {
            const double MaxRollAngle = Math.PI / 16;
            const double MaxDescendClimbAngle = Math.PI / 16;

            if (ship.RightLeftRoll != 0)
            {
                // Apply roll
                var deltaAngle = GetDeltaAngle(ship.RightLeftRoll, Model.Ship.MaxRoll, MaxRollAngle);
                var angleZ = TruncateAngle(world.Eye.Rotation.AngleZ + deltaAngle);
                world.Eye.Rotation.AngleZ = (float)angleZ;
            }

            if (ship.DescendClimb != 0)
            {
                // Apply d/c
                var deltaAngle = GetDeltaAngle(ship.DescendClimb, Model.Ship.MaxDescendClimb, MaxDescendClimbAngle);
                var angleX = TruncateAngle(world.Eye.Rotation.AngleX + deltaAngle);
                world.Eye.Rotation.AngleX = (float)angleX;
            }

            this.Invalidate();
        }

        private double GetDeltaAngle(int controlValue, int maxControlValue, double maxAngle)
        {
            int shiftedValue = controlValue + maxControlValue;      // 0 - N
            var deltaAngle = (shiftedValue * 2 * maxAngle) / (2 * maxControlValue);
            return deltaAngle - maxAngle;
        }

        private double TruncateAngle(double angle)
        {
            if (angle < 0)
            {
                angle += Math.PI * 2;
            }
            else if (angle > Math.PI * 2)
            {
                angle -= Math.PI * 2;
            }
            return angle;
        }

        private void SpacePanel_Resize(object sender, EventArgs e)
        {
            SetView();
        }
    }
}
