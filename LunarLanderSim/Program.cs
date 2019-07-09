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
            double initialOrientation = Util.DegToRad(45);
            RigidBody2D lander = new RigidBody2D(massProp, initalPosition, initialVelocity, initialOrientation, 0);
            lander.Gravity = gravity;

            // Initialize fuel tanks
            FuelTank rcsTank = new FuelTank(massProp, 633);
            FuelTank mainTank = new FuelTank(massProp, 10334);

            // Initialize engines
            Vector2D engineMountPoint = new Vector2D(0, -4); // m
            double orientation = Util.DegToRad(-90);
            double maxThrust = 45040; // N
            double fuelUsage = 13; // kg/s, guess
            Thruster mainEngine = new Thruster(massProp, lander, mainTank, engineMountPoint, orientation, maxThrust, fuelUsage, true);

            // Thrusters
            List<Vector2D> rcsMountPoints = new List<Vector2D>() { new Vector2D(-5,0), new Vector2D(-5, 0), new Vector2D(5, 0), new Vector2D(5, 0) };
            List<double> rcsOrientations = new List<double>() { 90, -90, 90, -90 };
            List<Thruster> rcsThrusters = new List<Thruster>();
            double rcsMaxThrust = 1760;
            double rcsFuelUsage = 15; // kg/s
            for(int i=0; i<4; i++)
            {
                rcsThrusters.Add(new Thruster(massProp, lander, rcsTank, rcsMountPoints[i], rcsOrientations[i], rcsMaxThrust, rcsFuelUsage, false));
            }

            // Flight control
            LanderFlightControl flightControl = new LanderFlightControl(lander, mainEngine, rcsThrusters);
            
            // logging
            Logger log = new Logger(5);
            log.MainEngine = mainEngine;
            log.RigidBody = lander;

            // Initialize scheduler
            EqualScheduler scheduler = new EqualScheduler(40); //hz
            scheduler.Add(flightControl, 0);
            scheduler.Add(mainEngine, 10);

            for(int i=0; i<4; i++)
            {
                scheduler.Add(rcsThrusters[i], 20 + i);
            }

            scheduler.Add(lander, 40);
            scheduler.Add(log, 50);

            // run sim
            scheduler.Run(TimeSpan.FromSeconds(125));
            log.WriteCSV("simdata_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".csv");
        }
    }
}
