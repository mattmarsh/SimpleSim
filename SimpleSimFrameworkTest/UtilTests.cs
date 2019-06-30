using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleSimFramework;

namespace SimpleSimFrameworkTest
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void TestTo360()
        {
            var tol = 0.001;
            Assert.AreEqual(359, Util.To360(-1), tol);
            Assert.AreEqual(10, Util.To360(10 - 360), tol);
            Assert.AreEqual(359, Util.To360(359), tol);
            Assert.AreEqual(10, Util.To360(10 - 360*2), tol);
            Assert.AreEqual(182, Util.To360(182 + 360), tol);
            Assert.AreEqual(182, Util.To360(182 + 360*2), tol);
        }

        [TestMethod]
        public void TestTo2PI()
        {
            var tol = 0.001;
            Assert.AreEqual(6, Util.To2Pi(6), tol);
            Assert.AreEqual(1, Util.To2Pi(1), tol);
            Assert.AreEqual(6, Util.To2Pi(6 - 2*Math.PI), tol);
            Assert.AreEqual(6, Util.To2Pi(6 - 4 * Math.PI), tol);
            Assert.AreEqual(6, Util.To2Pi(6 + 4 * Math.PI), tol);
        }

        [TestMethod]
        public void TestPi2NegPi()
        {
            var tol = 0.001;
            Assert.AreEqual(6 - Math.PI, Util.ToPiNegPi(6), tol);
        }
    }
}
