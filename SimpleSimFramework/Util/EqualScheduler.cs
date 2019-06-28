using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework
{
    /// <summary>
    /// This module runs all modules in a specified order and at an equal rate (hz)
    /// </summary>
    public class EqualScheduler
    {
        private static int CompareScheduledModule(ScheduledModule x, ScheduledModule y)
        {
            return x.order.CompareTo(y.order);
        }

        private class ScheduledModule
        {
            public ISimModule module;
            public int order;
            public ScheduledModule(ISimModule m, int o) { module = m; order = o; }
        }

        private List<ScheduledModule> moduleList = new List<ScheduledModule>();
        private int rate;
        private TimeSpan dt;

        public EqualScheduler(int rate)
        {
            this.rate = rate;
            this.dt = TimeSpan.FromSeconds(1.0 / rate);
        }

        /// <summary>
        /// Add a module to run at specified rate and priority
        /// </summary>
        /// <param name="module"></param>
        /// <param name="order">0 is highest</param>
        /// <param name="rate">rate in hz (not used)</param>
        public void Add(ISimModule module, int order)
        {
            moduleList.Add(new ScheduledModule(module, order));
        }

        /// <summary>
        /// Run all modules for specified time
        /// </summary>
        /// <param name="runTime"></param>
        public void Run(TimeSpan runTime)
        {
            TimeSpan timeLeft = runTime;

            // Sort the list by order, first
            moduleList.Sort(CompareScheduledModule);

            while(timeLeft > TimeSpan.Zero)
            {
                TimeSpan dtAdjusted = dt;
                if (dtAdjusted > timeLeft) dtAdjusted = timeLeft;

                foreach(ScheduledModule module in moduleList)
                {
                    module.module.Run(dtAdjusted);
                }

                timeLeft -= dtAdjusted;
            }
        }
    }
}
