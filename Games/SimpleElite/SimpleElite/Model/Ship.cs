using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleElite.Model
{
    public class Ship
    {
        public const int MaxRoll = 50;
        public const int MaxDescendClimb = 50;

        public int FrontShield { get; set; }
        public int RearShield { get; set; }
        public int Fuel { get; set; }
        public int CabinTemperature { get; set; }
        public int LaserTemperature { get; set; }
        public int Altitute { get; set; }

        public int Speed { get; set; }
        public int RightLeftRoll { get; set; }
        public int DescendClimb { get; set; }
        public int Energy { get; set; }

        public Point3d Location { get; set; }
        public Point3d Direction { get; set; }



        public Ship()
        {
            FrontShield = 100;
            RearShield = 100;
            Fuel = 100;
            CabinTemperature = 0;
            LaserTemperature = 0;
            Altitute = 100;

            Speed = 100;
            RightLeftRoll = 0;
            DescendClimb = 0;
            Energy = 100;
        }


        public void ApplyControls(ShipControls shipControls)
        {
            DescendClimb += shipControls.DescendClimbDelta;
            if (DescendClimb > MaxDescendClimb)
            {
                DescendClimb = MaxDescendClimb;
            }
            else if (DescendClimb < -MaxDescendClimb)
            {
                DescendClimb = -MaxDescendClimb;
            }

            RightLeftRoll += shipControls.RightLeftRollDelta;
            if (RightLeftRoll > MaxRoll)
            {
                RightLeftRoll = MaxRoll;
            }
            else if (RightLeftRoll < -MaxRoll)
            {
                RightLeftRoll = -MaxRoll;
            }

            Speed += shipControls.AccelerateDelta;
            if (Speed > 100)
            {
                Speed = 100;
            }
            else if (Speed < 0)
            {
                Speed = 0;
            }
        }

    }
}
