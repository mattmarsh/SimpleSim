using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleSimFramework.Models;

namespace SimpleSimFrameworkTest
{
    [TestClass]
    public class Model2DTests
    {
        [TestMethod]
        public void TestRigidBody2D()
        {
            var zero = new Vector2D();
            RigidBody2D m = new RigidBody2D(zero, zero, new Vector2D(1,2), 0, 0, 0);
            m.Run(TimeSpan.FromSeconds(1));

            Assert.AreEqual(1, m.Velocity.x, 0.001);
            Assert.AreEqual(2, m.Velocity.y, 0.001);

            m.Run(TimeSpan.FromSeconds(1));

            Assert.AreEqual(1, m.Position.x, 0.001);
            Assert.AreEqual(2, m.Position.y, 0.001);
            Assert.AreEqual(2, m.Velocity.x, 0.001);
            Assert.AreEqual(4, m.Velocity.y, 0.001);
        }
    }
}
