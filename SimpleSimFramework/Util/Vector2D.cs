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

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
