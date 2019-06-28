using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework.Models
{
    /// <summary>
    /// Simple mass properties, assumes that mass is always centered at the center of mass
    /// </summary>
    public class MassProperties : ISimModule
    {
        private Dictionary<string, double> masses = new Dictionary<string, double>();
        private double mass;

        public double Mass => mass;

        /// <summary>
        /// Initialize mass prop with mass
        /// </summary>
        /// <param name="mass">mass in kg</param>
        public MassProperties(double mass)
        {
            this.mass = mass;
        }

        /// <summary>
        /// set mass via a label and an amount
        /// </summary>
        /// <param name="name">a label assigned to this mass</param>
        /// <param name="amount">mass in kg</param>
        public void SetMass(string name, double amount)
        {
            masses[name] = amount;
        }

        public void Run(TimeSpan dt)
        {
            // Sum up the masses
            mass = 0;
            foreach(var m in masses.Values)
            {
                mass += m;
            }
        }
    }
}
