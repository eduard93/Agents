using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Agents.Utils;
using Microsoft.Msagl.Drawing;

namespace Agents.Model
{
    public class Simulation
    {
        public List<Actor> Actors = new List<Actor>();

        public List<Message> Messages = new List<Message>();

        public List<Relationship> Relationships = new List<Relationship>();

        public int amountPer = 55;

        public int minAmount = 10;

        public int maxAmount = 10;

        public string LogFile = @"D:\C#_projects\Agents\Agents\Logs\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".csv";


        public Simulation(int actors, int messages = 5)
        {
            if (actors<5)
            {
                actors = 5;
            }

            for (int i = 1; i <= actors; i++)
            {
                int level = 5;
                if (i <= 5)
                {
                    level = i;
                }
                else
                {
                    level = Rnd.GetRandomLevel();
                }
                Actors.Add(new Actor(level, "Actor "+i.ToString()));
            }

            
            //P
            foreach (Actor Actor in Actors)
            {
                if (Actor.Level > 1)
                {
                    int PrincipalPoints = 5;

                    while (PrincipalPoints > 0)
                    {
                        Actor Principal = Actors.PickRandomPrincipal(Actor);
                        int ImportanceActor = Rnd.GetRandomNumber(PrincipalPoints);
                        Relationships.Add(new Relationship(Principal, Actor, 2 * ImportanceActor));
                        PrincipalPoints -= ImportanceActor;
                    }
                }
            }

            /*for (int i = 0; i < messages; i++)
            {
                Actor Principal = Actors.PickRandomActor(1);
                Actor Agent = Actors.PickRandomAgent(Principal);
                Messages.Add(Message.CreateRandom(Principal, Agent));
            }*/

            File.WriteAllText(LogFile, String.Empty);
            string log = Log.SimulationMessageInfoCSV();
            AddToLog(log);
            
        }

        public void ProcessPeriod(int period)
        {
            GenerateExternal(period);
            string log = Log.SimulationMessageInfoCSV(this);
            AddToLog(log);
            foreach (Actor Actor in Actors)
            {
                Actor.ProcessPeriod(period, Messages);
            }
        }

        private void GenerateExternal(int period)
        {
            int currentAmount = amountPer;

            while (currentAmount > 0)
            {
                int msgAmount = Rnd.GetRandomNumber(minAmount, maxAmount);
                currentAmount -= msgAmount;

                if (currentAmount > 0)
                {
                    Actor Principal = Actors.PickRandomActor(1);
                    Actor Agent = Actors.PickRandomAgent(Principal);
                    Messages.Add(Message.CreateRandom(Principal, Agent, currentAmount));
                }
            }
            
        }

        public void ProcessSimulation(int period)
        {
            for (int i = 0; i < period; i++)
            {
                ProcessPeriod(i);
            }
        }

        #region Utils

        public Graph ToGraph()
        {
            Graph graph = new Graph("graph");

            //  graph.AddEdge("A", "B");
            foreach (Actor Actor in Actors)
            {
                foreach (Relationship AgentRel in Actor.Agents)
                {
                    graph.AddEdge(Actor.Name, AgentRel.Agent.Name);
                }
            }

            return graph;
        }






        public void ToCSV(int period, string file)
        {
            File.Delete(file);
            string d = ";";
            WritePureStringToFile("sep=;", file);

            string temp = "Period;";
            /*foreach (Actor Actor in Actors)
            {
                temp += Actor.Name + " Sat"+d;
                foreach (Demand Demand in Actor.Demands)
                {
                    temp += Demand.Name + d;
                }
            }
            WritePureStringToFile(temp, file);

            for (int i = 0; i < period; i++)
            {
                temp = i.ToString()+d;
                foreach (Actor Actor in Actors)
                {
                    temp += Actor.Satisfaction[i] + d;
                    foreach (Demand Demand in Actor.Demands)
                    {
                        temp += Demand.CurLevel[i] + d;
                    }

                }
                WritePureStringToFile(temp, file);
            }*/

        }

        public static void WritePureStringToFile(string text, string file)
        {
            using (TextWriter tw = new StreamWriter(@file, true))
            {
                tw.WriteLine(text);
            }
        }


        public void AddToLog(string text)
        {
            using (TextWriter tw = new StreamWriter(@LogFile, true))
            {
                tw.WriteLine(text);
            }
        }

        #endregion


    }
}
