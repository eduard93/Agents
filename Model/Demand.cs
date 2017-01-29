using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace emotion_sim
{
    class Demand
    {
        public Demand()
        {


        }

        /// <summary>
        /// Name of desire
        /// </summary>
        public string Name;

        /// <summary>
        /// Minimum level of desire satisfation.
        /// Lowering below this would not amount to any emotional satisfaction change
        /// </summary>
        public double MinLevel;

        /// <summary>
        /// Maximum level of desire satisfation.
        /// Raising above this would not amount to any emotional satisfaction change
        /// </summary>
        public double MaxLevel;

        /// <summary>
        /// Desired level of desire satisfation.
        /// With that EmoSatisfaction = 0
        /// </summary>
        //public double DesLevel;

        /// <summary>
        /// Current level of demand satisfaction
        /// </summary>
        public double[] CurLevel = new double[100];

        /// <summary>
        /// Satisfaction of demamd - emotional satisfaction coefficient
        /// </summary>
        public double Importance;

        /// <summary>
        /// Current lefel of emotional satisfaction from min-max
        /// </summary>
        public double[] lSatisfaction = new double[100];

        /// <summary>
        /// Current lefel of emotional satisfaction from level change
        /// </summary>
        public double[] dSatisfaction = new double[100];

        public double[] Satisfaction = new double[100];


        public void ProduceNextLevel(Func<double, double> f, int period, double satisfaction)
        {

            CurLevel[period + 1] += f(satisfaction);
        }

        public void CalculateSatisfaction(int period)
        {
            CalculatelSatisfaction(period);
            CalculatedSatisfaction(period);
            Satisfaction[period] = Importance * (dSatisfaction[period] + lSatisfaction[period]) / 2;

        }

        public void CalculatelSatisfaction(int period)
        {
            if (CurLevel[period] < MinLevel)
            {
                lSatisfaction[period] = -1;
            }
            else if (CurLevel[period] < MaxLevel)
            {
                double temp = (CurLevel[period] - MinLevel) / (MaxLevel - MinLevel);
                lSatisfaction[period] = 2 * temp - 1;
            }
            else
            {
                lSatisfaction[period] = 1;
            }

        }

        public void CalculatedSatisfaction(int period)
        {
            double temp=0;
            for (int i = period ; i > 0; i--)
            {
                temp += ((CurLevel[i] - CurLevel[i - 1]) / (MaxLevel - MinLevel)) * (1 / (period - i + 1));
            }
            dSatisfaction[period] = temp;
        }
    }
}
