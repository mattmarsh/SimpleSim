using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework
{
    public static class Util
    {
        /// <summary>
        ///  Convert to -180 <= x < 179.999
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double To180(double angle)
        {
            double res = To360(angle);
            if(angle >= 180)
            {
                angle -= 360;
            }
            return angle;
        }

        /// <summary>
        /// Convert to 0 <= x < 360
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double To360(double angle)
        {
            var res =  angle % 360.0;
            if (res < 0) res += 360;
            return res;
        }

        public static double To2Pi(double angle)
        {
            var res =  angle % (2 * Math.PI);
            if (res < 0) res += (2 * Math.PI);
            return res;
        }

        public static double ToPiNegPi(double angle)
        {
            double res = To2Pi(angle);
            if(res >= Math.PI)
            {
                res -= Math.PI;
            }
            return res;
        }

        public static double DegToRad(double deg)
        {
            return deg / 180 * Math.PI;
        }

        public static double RadToDeg(double rad)
        {
            return rad * 180 / Math.PI;
        }
    }
}
