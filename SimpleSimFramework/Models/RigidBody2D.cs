﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework
{
    public class RigidBody2D : ISimModule
    {
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

        public RigidBody2D()
        {

        }

        public RigidBody2D(Vector2D position, Vector2D velocity,  Vector2D acceleration, 
            double orientation, double angularVelocity, double angularAcceleration)
        {
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.orientation = orientation;
            this.angularAcceleration = angularAcceleration;
            this.angularVelocity = angularVelocity;
        }

        public void Run(TimeSpan dt)
        {
            double seconds = dt.TotalSeconds;

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
