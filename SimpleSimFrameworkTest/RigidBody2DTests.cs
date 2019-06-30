using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleSimFramework.Models;
using SimpleSimFramework;
namespace SimpleSimFrameworkTest
{
    [TestClass]
    public class RigidBody2DTests
    {
        [TestMethod]
        public void TestForceNoTorqueNoGrav()
        {
            MassProperties mp = new MassProperties(10, 1);
            RigidBody2D m = new RigidBody2D(mp);
            Guid id = Guid.NewGuid();
            m.SetForce(id, new Vector2D(1, 2), Vector2D.Zero);
            var onesec = TimeSpan.FromSeconds(1);
            var tol = 0.001;

            m.Run(onesec);
            Assert.AreEqual(0.1, m.Velocity.x, tol);
            Assert.AreEqual(0, m.Position.x, tol);
            Assert.AreEqual(0.2, m.Velocity.y, tol);
            Assert.AreEqual(0, m.Position.y, tol);

            m.Run(onesec);
            Assert.AreEqual(0.2, m.Velocity.x, tol);
            Assert.AreEqual(0.1, m.Position.x, tol);
            Assert.AreEqual(0.4, m.Velocity.y, tol);
            Assert.AreEqual(0.2, m.Position.y, tol);

            m.SetForce(id, Vector2D.Zero, Vector2D.Zero);
            m.Run(onesec);
            Assert.AreEqual(0.2, m.Velocity.x, tol);
            Assert.AreEqual(0.3, m.Position.x, tol);
            Assert.AreEqual(0.4, m.Velocity.y, tol);
            Assert.AreEqual(0.6, m.Position.y, tol);
        }

        [TestMethod]
        public void TestForceTorqueNoGrav()
        {
            MassProperties mp = new MassProperties(10, 10);
            RigidBody2D m = new RigidBody2D(mp);
            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();

            // Add two opposing forces on opp sides of moment arm
            m.SetForce(id1, new Vector2D(0, 1), new Vector2D(1, 0));
            m.SetForce(id2, new Vector2D(0, -1), new Vector2D(-1, 0));

            var onesec = TimeSpan.FromSeconds(1);
            var tol = 0.001;

            m.Run(onesec);
            Assert.AreEqual(0.2, m.AngularVelocity, tol);
            Assert.AreEqual(0.0, m.Orientation, tol);
            m.Run(onesec);
            Assert.AreEqual(0.4, m.AngularVelocity, tol);
            Assert.AreEqual(0.2, m.Orientation, tol);

            // remove forces
            m.SetForce(id1, Vector2D.Zero, Vector2D.Zero);
            m.SetForce(id2, Vector2D.Zero, Vector2D.Zero);
            m.Run(onesec);
            Assert.AreEqual(0.4, m.AngularVelocity, tol);
            Assert.AreEqual(0.6, m.Orientation, tol);
        }

        [TestMethod]
        public void TestGrav()
        {
            MassProperties mp = new MassProperties(10, 1);
            RigidBody2D m = new RigidBody2D(mp);
            m.Gravity = new SimpleGravity(new Vector2D(0, -1));

            var onesec = TimeSpan.FromSeconds(1);
            m.Run(onesec);
            Assert.AreEqual(-1, m.Velocity.y, 0.001);
            Assert.AreEqual(0, m.Position.y, 0.001);
            m.Run(onesec);
            Assert.AreEqual(-2, m.Velocity.y, 0.001);
            Assert.AreEqual(-1, m.Position.y, 0.001);
            m.Run(onesec);
            Assert.AreEqual(-3, m.Velocity.y, 0.001);
            Assert.AreEqual(-3, m.Position.y, 0.001);
            Assert.AreEqual(0, m.Velocity.x, 0.001);
            Assert.AreEqual(0, m.Position.x, 0.001);
        }
    }
}
