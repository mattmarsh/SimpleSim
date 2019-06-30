using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework.Models
{
    public class FuelTank
    {
        private MassProperties massProp;
        private double mass; // kg

        /// <summary>
        /// Fuel mass in kg
        /// </summary>
        public double Mass { get; set; }

        public FuelTank(MassProperties massProp)
        {
            this.massProp = massProp;
        }

        /// <summary>
        /// Initialize fuel tank
        /// </summary>
        /// <param name="massProp"></param>
        /// <param name="mass">mass of fuel in kg</param>
        public FuelTank(MassProperties massProp, double mass)
        {
            this.massProp = massProp;
            this.mass = mass;
        }
    }
}
