using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleElite.View
{
    public partial class MainForm : Form
    {
        private Model.Ship ship = new Model.Ship();
        private Model.ShipControls shipControls = new Model.ShipControls();

        public MainForm()
        {
            InitializeComponent();

            controlPanel.UpdateView(ship);

            timer.Enabled = true;                 
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            HandleInput();
            HandleShipMovement();
            controlPanel.UpdateView(ship);
            spacePanel.UpdateView(ship);
        }

        private Model.ShipControls HandleInput()
        {
            // Descend/Climb
            if (Keyboard.IsKeyDown(Keys.Up))
            {
                shipControls.DescendClimbDelta -= 1;
            }
            else if (Keyboard.IsKeyDown(Keys.Down))
            {
                shipControls.DescendClimbDelta += 1;
            }
            else
            {
                // Apply descend/climb autocenter
                shipControls.DescendClimbDelta = -CalculateAdjustedHalf(ship.DescendClimb);
            }

            // Left/Right
            if (Keyboard.IsKeyDown(Keys.Left))
            {
                shipControls.RightLeftRollDelta -= 1;
            }
            else if (Keyboard.IsKeyDown(Keys.Right))
            {
                shipControls.RightLeftRollDelta += 1;
            }
            else
            {
                // Apply right/left autocenter
                shipControls.RightLeftRollDelta = -CalculateAdjustedHalf(ship.RightLeftRoll);
            }

            // Accelerate
            if (Keyboard.IsKeyDown(Keys.Space))
            {
                shipControls.AccelerateDelta += 1;
            }
            else if (Keyboard.IsKeyDown(Keys.ShiftKey))
            {
                shipControls.AccelerateDelta -= 1;
            }
            else
            {
                shipControls.AccelerateDelta = 0;
            }

            return shipControls;
        }

        private void HandleShipMovement()
        {
            ship.ApplyControls(shipControls);
        }


        private int CalculateAdjustedHalf(int origin)
        {
            int half = origin / 2;
            if (half == 0)
            {
                if (origin > 0)
                {
                    half = 1;
                }
                else if (origin < 0)
                {
                    half = -1;
                }
            }

            return half;
        }
    }
}
