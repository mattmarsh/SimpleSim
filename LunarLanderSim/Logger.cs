using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleSimFramework;
using SimpleSimFramework.Models;

namespace LunarLanderSim
{
    /// <summary>
    /// Just a quick logger class to output data to csv
    /// </summary>
    public class Logger : ISimModule
    {
        public RigidBody2D RigidBody { get; set; }
        public Thruster MainEngine { get; set; }

        private Dictionary<string, List<double>> data = new Dictionary<string, List<double>>();
        private double simtime = 0;

        public Logger()
        {
            var keys = new List<string> { "t", "X Position", "Y Position", "X Velocity", "Y Velocity", "X Acceleration", "Y Acceleration", "Main Engine Thrust" };
            foreach(var key in keys)
            {
                data[key] = new List<double>();
            }
        }

        public void Run(TimeSpan dt)
        {
            data["t"].Add(simtime);
            data["X Position"].Add(RigidBody.Position.x);
            data["Y Position"].Add(RigidBody.Position.y);
            data["X Velocity"].Add(RigidBody.Velocity.x);
            data["Y Velocity"].Add(RigidBody.Velocity.y);
            data["X Acceleration"].Add(RigidBody.Acceleration.x);
            data["Y Acceleration"].Add(RigidBody.Acceleration.y);
            data["Main Engine Thrust"].Add(MainEngine.Thrust);
            simtime += dt.TotalSeconds;
        }

        public void WriteCSV(string filename)
        {
            try {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
                {
                    var labels = data.Keys;
                    file.WriteLine(string.Join(",", labels));

                    StringBuilder line = new StringBuilder();
                    for (int i = 0; i < data["t"].Count; i++)
                    {
                        foreach (var label in labels)
                        {
                            line.Append(data[label][i]);
                            line.Append(",");
                        }
                        line.Remove(line.Length - 1, 1);
                        file.WriteLine(line.ToString());
                        line.Clear();
                    }
                }
            }
            catch (IOException)
            {
                //todo something went wrong
            }
        }
    }
}
