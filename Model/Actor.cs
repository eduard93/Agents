using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Agents.Model
{
    class Actor
    {
        public Actor(int level)
        {
            Level = level;
            //Demands = new List<Demand>();
        }

        /*
        /// <summary>
        /// Name of the actor
        /// </summary>
        ///public string Name;
        
        /// <summary>
        /// Current level of emotional satisfaction
        /// </summary>
        public List<double> Satisfaction = new List<double>();

        public List<Demand> Demands = new List<Demand>();
        */

        [Range(1, 5)]
        public int Level;

        public List<Relationship> Agents = new List<Relationship>();

        public List<Relationship> Principals = new List<Relationship>();

        public void ProcessPeriod(int period)
        {
            //double a = Satisfaction.Last();

        }

    }
}
