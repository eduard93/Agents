using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agents.Utils
{
    public static class Rnd
    {
        private static readonly Random rnd = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return rnd.Next(min, max+1);
        }

        public static int GetRandomNumber(int max)
        {
            return GetRandomNumber(1, max);
        }

        /// <summary>
        /// Get random boolean
        /// </summary>
        /// <param name="probability">Probability, that returntd bool is true</param>
        /// <returns></returns>
        public static bool GetRandomBool(int probability = 50)
        {
            return GetRandomNumber(0, 99) < probability;
        }

        public static int GetRandomLevel()
        {
            int num = GetRandomNumber(100);
            int level = 5;

            // 1 -  2 -  3 -  4 -  5
            // 5 - 10 - 15 - 20 - 50 
            if (num > 95)
            {
                level = 1;
            }
            else if (num > 85)
            {
                level = 2;
            }
            else if (num > 70)
            {
                level = 3;
            }
            else if (num > 50)
            {
                level = 4;
            }
            else
            {
                level = 5;
            }

            return level;
        }
    }
}
