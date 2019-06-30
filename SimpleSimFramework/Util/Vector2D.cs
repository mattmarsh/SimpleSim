using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework
{
    public struct Vector2D
    {
        public double x;
        public double y;

        public static Vector2D Zero = new Vector2D(0, 0);
        public static Vector2D Unit = new Vector2D(1, 1);

        public double Magnitude { get { return Math.Sqrt(x * x + y * y); } }

        public double AngleDeg { get { return Util.RadToDeg(AngleRad) ; } }
        public double AngleRad { get { return Math.Atan2(y, x); } }

        /// <summary>
        /// Rotate vector in radians, returns rotated vector
        /// </summary>
        public Vector2D Rotate(double radians)
        {
            Vector2D res = new Vector2D();
            res.x = x * Math.Cos(radians) - y * Math.Sin(radians);
            res.y = x * Math.Sin(radians) + y * Math.Cos(radians);
            return res;
        }

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
