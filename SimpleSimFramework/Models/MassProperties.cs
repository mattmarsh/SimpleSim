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
    public class MassProperties
    {
        private Dictionary<Guid, double> masses = new Dictionary<Guid, double>();
        private double mass;
        private double momentOfInertia;

        /// <summary>
        /// Mass in kg
        /// </summary>
        public double Mass => mass;

        /// <summary>
        /// Moment of inertia (kg m^2)
        /// </summary>
        public double MomentOfInertia { get { return momentOfInertia; } set { momentOfInertia = value; } }

        /// <summary>
        /// Initialize mass prop with mass
        /// </summary>
        /// <param name="mass">mass in kg</param>
        public MassProperties(double mass, double momentOfInertia)
        {
            this.mass = mass;
            this.momentOfInertia = momentOfInertia;
        }

        /// <summary>
        /// set mass via a label and an amount
        /// </summary>
        /// <param name="name">a label assigned to this mass</param>
        /// <param name="amount">mass in kg</param>
        public void SetMass(Guid id, double amount)
        {
            masses[id] = amount;
            Recalculate();
        }

        public void RemoveMass(Guid id)
        {
            masses.Remove(id);
            Recalculate();
        }

        private void Recalculate()
        {
            // Sum up the masses
            double newMass = 0;
            foreach(var m in masses.Values)
            {
                newMass += m;
            }

            // adjust the MoI some
            // todo: deal with zero mass
            if(mass != 0) momentOfInertia *= newMass / mass;

            mass = newMass;
        }
    }
}
