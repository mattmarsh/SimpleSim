using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework.Models
{
    public class SimpleGravity : IGravity2D
    {
        public Vector2D Acceleration { get; set; } = Vector2D.Zero;

        SimpleGravity(Vector2D acceleration)
        {
            Acceleration = acceleration;
        }

        public Vector2D GetAcceleration(Vector2D location)
        {
            return Acceleration;
        }
    }
}
