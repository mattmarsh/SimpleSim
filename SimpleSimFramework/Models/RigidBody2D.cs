using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework.Models
{
    public class RigidBody2D : ISimModule
    {
        public MassProperties MassProp { get; set; }

        /// <summary>
        /// Position in 2D space, meters
        /// </summary>
        public Vector2D Position { get { return position; } }

        /// <summary>
        /// Velocity in meters per second
        /// </summary>
        public Vector2D Velocity { get { return velocity; } }

        /// <summary>
        /// Acceleration in m/s^2
        /// </summary>
        public Vector2D Acceleration { get { return acceleration; } }

        /// <summary>
        /// Orientation in deg
        /// </summary>
        public double Orientation { get { return orientation; } }

        /// <summary>
        /// Ang vel in deg/s
        /// </summary>
        public double AngularVelocity { get { return angularVelocity; } }

        /// <summary>
        /// Ang accel in deg/s^2
        /// </summary>
        public double AngularAcceleration { get { return angularAcceleration; } }

        private Vector2D position;
        private Vector2D velocity;
        private Vector2D acceleration;
        private double orientation;
        private double angularVelocity;
        private double angularAcceleration;
        private double mass; // kg

        /// <summary>
        /// Forces (in x,y Newtons) and moment arms (in x,y) WRT center of mass acting on rigid body
        /// </summary>
        private Dictionary<Guid, Tuple<Vector2D, Vector2D>> forces;

        public RigidBody2D(MassProperties massProp)
        {
            this.MassProp = massProp;
        }

        public RigidBody2D(MassProperties massProp, Vector2D position, Vector2D velocity,
            double orientation, double angularVelocity)
        {
            this.MassProp = massProp;
            this.position = position;
            this.velocity = velocity;
            //this.acceleration = acceleration;
            this.orientation = orientation;
            //this.angularAcceleration = angularAcceleration;
            this.angularVelocity = angularVelocity;
        }

        public void Run(TimeSpan dt)
        {
            double seconds = dt.TotalSeconds;

            // Stand at a safe distance, we are about to do physics

            Vector2D totalForce = new Vector2D();

            // Calculate x, y forces
            foreach (var force in forces.Values)
            {
                totalForce.x += force.Item1.x;
                totalForce.y += force.Item1.y;
            }
            acceleration.x = totalForce.x / MassProp.Mass;
            acceleration.y = totalForce.y / MassProp.Mass;

            // TODO: calculate torque and angular acceleration
            angularAcceleration = 0;

            // TODO: add gravity

            // Update position
            position.x += velocity.x * seconds;
            position.y += velocity.y * seconds;
            velocity.x += acceleration.x * seconds;
            velocity.y += acceleration.y * seconds;

            // Update orientation
            angularVelocity = angularAcceleration * seconds;
            orientation = Util.To180(angularVelocity * seconds);
            
        }
    }
}
