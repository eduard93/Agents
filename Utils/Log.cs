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


        public static String SimulationMessageInfo(Simulation simulation)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            double count = simulation.Messages.Count;

            sb.AppendLine("Messages");
            sb.AppendLine(String.Format("Total messages: {0}", count));
            sb.AppendLine(String.Format("Completed messages: {0} %", Math.Round(100 * simulation.Messages.Count(msg => msg.Status == Status.Completed) / count, 2)));
            sb.AppendLine(String.Format("Completed in time messages: {0} %", Math.Round(100 * simulation.Messages.Count(msg => msg.Status == Status.Completed && msg.EndReal <= msg.End) / count, 2)));
            sb.AppendLine(String.Format("Discarded messages: {0} %", Math.Round(100 * simulation.Messages.Count(msg => msg.Status == Status.Discarded) / count, 2)));
            sb.AppendLine(String.Format("Delegated messages: {0} %", Math.Round(100 * simulation.Messages.Count(msg => msg.Status == Status.Delegated) / count, 2)));
            return sb.ToString();
        }

        public static String SimulationMessageInfoCSV(Simulation simulation)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            double count = simulation.Messages.Count;

            var numberGroups = simulation.Messages.GroupBy(msg => msg.Status).ToDictionary(el => el.Key, el => el.Count());


            foreach (Status status in Enum.GetValues(typeof(Status)).Cast<Status>())
            {
                if (!numberGroups.ContainsKey(status))
                {
                    numberGroups[status] = 0;
                }
            }

            foreach (Status status in Enum.GetValues(typeof(Status)).Cast<Status>())
            {
                sb.AppendFormat("{0};", numberGroups[status]);
            }

            sb.Append(count.ToString());
            return sb.ToString();
        }

        public static String SimulationMessageInfoCSV()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
    
            foreach (Status status in Enum.GetValues(typeof(Status)).Cast<Status>())
            {
                sb.AppendFormat("{0};",  status.ToString());
            }

            sb.Append("Total");
            return sb.ToString();
        }

    }
}
