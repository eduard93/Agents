using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agents.Model;

namespace Agents.Utils
{
    public class Log
    {
        public static String SimulationUserInfo(Simulation simulation)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            var numberGroups = simulation.Actors.GroupBy(actor => actor.Level).OrderBy(grp => grp.Key);
            foreach (var grp in numberGroups)
            {
                var level = grp.Key;
                var count = grp.Count();

                sb.AppendLine(String.Format("Level: {0}, Count: {1}", level, count));
            }
            return sb.ToString();
        }

    }
}
