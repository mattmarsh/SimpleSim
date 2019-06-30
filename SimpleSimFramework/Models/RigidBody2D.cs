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

        public IGravity2D Gravity { get; set; }

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
        /// Orientation in rad
        /// </summary>
        public double Orientation { get { return orientation; } }

        /// <summary>
        /// Ang vel in rad/s
        /// </summary>
        public double AngularVelocity { get { return angularVelocity; } }

        /// <summary>
        /// Ang accel in rad/s^2
        /// </summary>
        public double AngularAcceleration { get { return angularAcceleration; } }

        public void SetForce(Guid id, Vector2D force, Vector2D momentArm)
        {
            forces[id] = new Tuple<Vector2D, Vector2D>(force, momentArm);
        }

        public void RemoveForce(Guid id)
        {
            forces.Remove(id);
        }

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
        private Dictionary<Guid, Tuple<Vector2D, Vector2D>> forces = new Dictionary<Guid, Tuple<Vector2D, Vector2D>>();

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

            Vector2D totalForce = new Vector2D(); //Newtons, in x,y
            double totalTorque = 0; // N*m 

            // Calculate x, y forces and torque
            foreach (var forceMoment in forces.Values)
            {
                var force = forceMoment.Item1;
                var momentArm = forceMoment.Item2;

                totalForce.x += force.x;
                totalForce.y += force.y;

                // calculate length of moment arm in meters     
                double r = momentArm.Magnitude; 

                // determine angle between force and moment arm (rad)
                //double theta = Util.ToPiNegPi(force.AngleRad - momentArm.AngleRad);
                double theta = force.AngleRad - momentArm.AngleRad;

                // torque = the length of moment arm * force parallel to moment arm
                double torque = force.Magnitude * r * Math.Sin(theta);
                totalTorque += torque;
            }

            // calc X, Y acceleration
            acceleration.x = totalForce.x / MassProp.Mass;
            acceleration.y = totalForce.y / MassProp.Mass;

            // calc angular acceleration
            // todo: deal with zero MoI
            angularAcceleration = totalTorque / MassProp.MomentOfInertia;

            // add gravity
            if(Gravity != null)
            {
                var gravAccel = Gravity.GetAcceleration(position);
                acceleration.x += gravAccel.x;
                acceleration.y += gravAccel.y;
            }

            // Update position
            position.x += velocity.x * seconds;
            position.y += velocity.y * seconds;
            velocity.x += acceleration.x * seconds;
            velocity.y += acceleration.y * seconds;

            // Update orientation
            orientation = Util.To180(orientation + angularVelocity * seconds);
            angularVelocity += angularAcceleration * seconds;
        }
    }
}
