using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace emotion_sim
{
    class Actor
    {
        public Actor()
        {
            //Demands = new List<Demand>();
        }
        /// <summary>
        /// Type of the actor: person or group
        /// </summary>
        public string Type;

        /// <summary>
        /// Name of the actor
        /// </summary>
        public string Name;
        
        /// <summary>
        ///  Satisfaction of demamd - emotional satisfaction coefficient
        /// </summary>
        public double EmotionCoefficient;

        /// <summary>
        /// Current level of emotional satisfaction
        /// </summary>
        public double[] Satisfaction = new double[100];

        public List<Demand> Demands = new List<Demand>();

        //public Dictionary<Demand, Func<double, double>> Outputs = new Dictionary<Demand, Func<double, double>>();

        public void ProcessPeriod(int period)
        {            
        }


    }
}
