using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSimFramework
{
    public interface ISimModule
    {
        void Run(TimeSpan dt);
    }
}
