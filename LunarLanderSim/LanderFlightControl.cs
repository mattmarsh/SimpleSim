using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework.Models
{
    public class LanderFlightControl : ISimModule
    {
        private RigidBody2D lander;
        private Thruster mainEngine;
        private List<Thruster> rcsThrusters;

        /// <summary>
        /// Initialize flight controls, expects a main engine and 4 thrusters
        /// </summary>
        /// <param name="lander"></param>
        /// <param name="mainEngine"></param>
        /// <param name="rcsThrusters"></param>
        public LanderFlightControl(RigidBody2D lander, Thruster mainEngine, List<Thruster> rcsThrusters)
        {
            this.lander = lander;
            this.mainEngine = mainEngine;
            this.rcsThrusters = rcsThrusters;
        }

        public void Run(TimeSpan dt)
        {
            double altitudeAGL = lander.Position.y;
            double descentRate = -lander.Velocity.y;
            double orientation = lander.Orientation;
            double angularVelocity = lander.AngularVelocity;

            // Set main engine thrust
            double temp = Util.DegToRad(5);
            mainEngine.SetThrust(false);
            if(orientation < temp && orientation > -temp)
            {
                if(altitudeAGL > 1000 && descentRate > 40 ||
                   altitudeAGL <= 1000 && altitudeAGL > 500 && descentRate > 20 ||
                   altitudeAGL <= 500 && altitudeAGL > 100 && descentRate > 10 ||
                   altitudeAGL <= 100 && descentRate > 5)
                {
                    mainEngine.SetThrust(true);
                    mainEngine.SetThrottle(1);
                }
            }

            
            
            double maxAngularVelocity = Util.DegToRad(45); // 45 degrees per second

            // make our desired angular velocity proportional 
            double desiredAngularVelocity = -orientation / Math.PI * maxAngularVelocity;
            double angularVelocityDiff = desiredAngularVelocity - angularVelocity;

            // Actuate thrusters
            if (angularVelocityDiff > 0.01) // allow for some hysteresis
            {
                // Accelerate CCW (positive direction)
                rcsThrusters[0].SetThrust(true);
                rcsThrusters[1].SetThrust(false);
                rcsThrusters[2].SetThrust(false);
                rcsThrusters[3].SetThrust(true);
            }
            else if(angularVelocityDiff < -0.01)
            {
                // Rotate CW (negative direction)
                rcsThrusters[0].SetThrust(false);
                rcsThrusters[1].SetThrust(true);
                rcsThrusters[2].SetThrust(true);
                rcsThrusters[3].SetThrust(false);
            }
            else
            {
                rcsThrusters[0].SetThrust(false);
                rcsThrusters[1].SetThrust(false);
                rcsThrusters[2].SetThrust(false);
                rcsThrusters[3].SetThrust(false);
            }
        }
    }
}
