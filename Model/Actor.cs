using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Agents.Utils;
using Newtonsoft.Json;

namespace Agents.Model
{
    public class Actor
    {
        public Actor(int level, string name)
        {
            Level = level;
            Name = name;
            //Demands = new List<Demand>();
        }


        /// <summary>
        /// Name of the actor
        /// </summary>
        public string Name;
        ///
        /*
        /// <summary>
        /// Current level of emotional satisfaction
        /// </summary>
        public List<double> Satisfaction = new List<double>();

        public List<Demand> Demands = new List<Demand>();
        */

        [Range(1, 5)]
        /// 1 - max, 5 - lowest level 
        public int Level;

        [JsonIgnore]
        public List<Relationship> Agents = new List<Relationship>();

        /*public bool ShouldSerializeAgents()
        {
            return Agents.Count > 0;
        }*/

        [JsonIgnore]
        public List<Relationship> Principals = new List<Relationship>();

        public void ProcessPeriod(int period)
        {
            //double a = Satisfaction.Last();

        }

    }
}
