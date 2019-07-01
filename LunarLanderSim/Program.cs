using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleSimFramework.Models;
using SimpleSimFramework;

namespace LunarLanderSim
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize mass properties
            MassProperties massProp = new MassProperties(1,1);
            Guid structureMassId = Guid.NewGuid();
            massProp.SetMass(structureMassId, 4280); // kg, structure weight without fuel
            SimpleGravity gravity = new SimpleGravity(new Vector2D(0, -1.62)); // m/s^2

            // Initialize lander
            Vector2D initalPosition = new Vector2D(0, 3000);
            Vector2D initialVelocity = new Vector2D(0, -20);
            RigidBody2D lander = new RigidBody2D(massProp, initalPosition, initialVelocity, 0, 0);
            lander.Gravity = gravity;

            // Initialize fuel tanks
            FuelTank rcsTank = new FuelTank(massProp, 633);
            FuelTank mainTank = new FuelTank(massProp, 10334);

            // Initialize engines
            Vector2D engineMountPoint = new Vector2D(0, -10); // m
            double orientation = Util.DegToRad(-90);
            double maxThrust = 45040;
            double fuelUsage = 13; // kg/s, guess
            Thruster mainEngine = new Thruster(massProp, lander, mainTank, engineMountPoint, orientation, maxThrust, fuelUsage, true);

            // logging
            Logger log = new Logger();
            log.MainEngine = mainEngine;
            log.RigidBody = lander;

            // Initialize scheduler
            EqualScheduler scheduler = new EqualScheduler(40); //hz
            scheduler.Add(mainEngine, 0);
            scheduler.Add(lander, 1);
            scheduler.Add(log, 2);

            // run sim
            mainEngine.SetThrottle(0.6);
            mainEngine.SetThrust(true);
            scheduler.Run(TimeSpan.FromSeconds(60));
            log.WriteCSV("simdata.csv");
        }
    }
}
