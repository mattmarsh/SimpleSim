using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework.Models
{
    /// <summary>
    /// Represents a generic model of a thruster or engine
    /// </summary>
    public class Thruster : ISimModule
    {
        private MassProperties massProp;
        private RigidBody2D rigidBody;
        private FuelTank fuelTank;
        private double maxThrust; // max thrust in newtons at 100%
        private double maxFuelUsage; // fuel usage in kg/s at max thrust
        private double thrustPercent = 1; // 1 = 100%
        private bool thrustOn;
        private bool throttleable;  // can thrust be controlled?
        private Vector2D mountPointBody; // location where thrust is applied in body coordinates
        private double orientation; // orientation of thrust WRT body, radians
        private Guid thrustId = Guid.NewGuid();

        /// <summary>
        /// Current thrust in Newtons
        /// </summary>
        public double Thrust
        {
            get; private set;
        }

        /// <summary>
        /// Initialize thruster model
        /// </summary>
        /// <param name="massProp"></param>
        /// <param name="rigidBody"></param>
        /// <param name="tank"></param>
        /// <param name="mountPointBody"></param>
        /// <param name="orientation">radians WRT body</param>
        /// <param name="maxThrust">max thrust in Newtons</param>
        /// <param name="maxFuelUsage">fuel usage in kg/s at max thrust</param>
        /// <param name="throttleable">can be throttled</param>
        public Thruster(MassProperties massProp, RigidBody2D rigidBody, FuelTank tank, Vector2D mountPointBody, double orientation, double maxThrust, double maxFuelUsage, bool throttleable)
        {
            this.massProp = massProp;
            this.rigidBody = rigidBody;
            this.fuelTank = tank;
            this.mountPointBody = mountPointBody;
            this.orientation = orientation;
            this.maxThrust = maxThrust;
            this.maxFuelUsage = maxFuelUsage;
            this.throttleable = throttleable;
        }

        /// <summary>
        /// Set thrust percent. If thruster is not throttlable, does nothing.
        /// </summary>
        /// <param name="percent">1 is 100%, 0 is 0%</param>
        public void SetThrottle(double percent)
        {
            if (throttleable)
            {
                thrustPercent = Math.Max(0, percent);
            }
        }

        public void SetThrust(bool on)
        {
            thrustOn = on;
        }

        public void Run(TimeSpan dt)
        {
            double fuelUsage = thrustPercent * maxFuelUsage * dt.TotalSeconds; //kg
            bool fuelAvailable = false;
            double adjustedThrustPercent = thrustPercent;

            // Check if fuel is left
            if(fuelTank.Mass > 0)
            {
                fuelAvailable = true;
                if(fuelTank.Mass < fuelUsage)
                {
                    // Adjust thrust to be less if not enough fuel is left
                    adjustedThrustPercent *= fuelTank.Mass / fuelUsage;
                    fuelUsage = fuelTank.Mass;
                }
            }

            if(thrustOn && fuelAvailable)
            {
                Vector2D thrust = new Vector2D(-maxThrust * adjustedThrustPercent, 0);
                thrust = thrust.Rotate(orientation); // rotate to body coordinates
                thrust = thrust.Rotate(-rigidBody.Orientation); //rotate to x,y coordinates
                Vector2D momentArm = mountPointBody.Rotate(-rigidBody.Orientation); // rotate mount point to x,y coords

                rigidBody.SetForce(thrustId, thrust, momentArm);
                fuelTank.Mass = Math.Max(0, fuelTank.Mass - fuelUsage);
                Thrust = thrust.Magnitude;
            }
            else
            {
                Thrust = 0;
                rigidBody.RemoveForce(thrustId);
            }
        }
    }
}
