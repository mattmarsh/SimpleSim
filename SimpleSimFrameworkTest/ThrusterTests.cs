using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleSimFramework;
using SimpleSimFramework.Models;

namespace SimpleSimFrameworkTest
{
    [TestClass]
    public class ThrusterTests
    {
        public MassProperties mp;
        public RigidBody2D rb;
        public Thruster thrust;
        public FuelTank tank;
        public double tol = 0.01;
        public TimeSpan onesec = TimeSpan.FromSeconds(1);

        [TestInitialize]
        public void TestInit()
        {
            mp = new MassProperties(10, 10);
            rb = new RigidBody2D(mp);
            tank = new FuelTank(mp, 10); // total mass 10 kg
            var mountPoint = new Vector2D(0, -1); // down 1 m
            var orientation = -Math.PI / 2; // pointing "down"
            thrust = new Thruster(mp, rb, tank, mountPoint, orientation, 1, 1, true);
        }

        [TestMethod]
        public void TestFuelUsage()
        {
            thrust.SetThrust(true);
            thrust.Run(onesec);
            Assert.AreEqual(9, tank.Mass, tol);
            Assert.AreEqual(9, mp.Mass, tol);
        }

        [TestMethod]
        public void TestThrust()
        {
            thrust.SetThrust(true);
            thrust.Run(onesec);
            rb.Run(onesec);
            Assert.AreEqual(0.11, rb.Velocity.y, tol);
            Assert.AreEqual(0.0, rb.Position.y, tol);
            thrust.Run(onesec);
            rb.Run(onesec);
            Assert.AreEqual(0.23, rb.Velocity.y, tol);
            Assert.AreEqual(0.11, rb.Position.y, tol);
        }
    }
}
