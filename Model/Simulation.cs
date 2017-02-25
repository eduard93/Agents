using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Agents.Model
{
    class Simulation
    {
        public Simulation(int actors)
        {
            Random rnd = new Random();
            for (int i = 0; i < actors; i++)
            {
                Actors.Add(new Actor(rnd.Next(1, 6)));
            }


            foreach (Actor Actor in Actors)
            {
                
            }

        }

        public List<Actor> Actors = new List<Actor>();





        public void ProcessPeriod(int period)
        {
            foreach (Actor Actor in Actors)
            {
                Actor.ProcessPeriod(period);
            }
        }

        public void ProcessSimulation(int period)
        {
            for (int i = 0; i < period; i++)
            {
                ProcessPeriod(i);
            }
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


    }
}
