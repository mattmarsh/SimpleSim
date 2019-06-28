using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleSimFramework;

namespace SimpleSimFrameworkTest
{
    [TestClass]
    public class SchedulerTests
    {
        public class TestModule : ISimModule
        {
            public TimeSpan totalTime = TimeSpan.Zero;
            public int runs = 0;

            public void Run(TimeSpan dt)
            {
                totalTime += dt;
                runs++;
            }
        }

        [TestMethod]
        public void TestEqualScheduler()
        {
            EqualScheduler s = new EqualScheduler(10);
            TestModule m1 = new TestModule();
            TestModule m2 = new TestModule();

            s.Add(m2, 2);
            s.Add(m1, 1);
            s.Run(TimeSpan.FromSeconds(1));

            Assert.AreEqual(10, m1.runs);
            Assert.AreEqual(10, m2.runs);
            Assert.AreEqual(1, m1.totalTime.TotalSeconds, 0.001);
            Assert.AreEqual(1, m2.totalTime.TotalSeconds, 0.001);
        }
    }
}
