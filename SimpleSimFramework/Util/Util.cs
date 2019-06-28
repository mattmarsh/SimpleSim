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
            return angle % 360;
        }
    }
}
