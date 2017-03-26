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

        [JsonIgnore]
        public List<Relationship> Principals = new List<Relationship>();

        public void ProcessPeriod(int period, List<Message> messages)
        {
           
            double workAvailable = GetWorkAvailable(period);
            IEnumerable<Message> activeMessages = OrderActiveMessages(messages.ActiveMessages(this));

            foreach (Message message in activeMessages)
            {
                if (message.Status == Status.Created)
                {
                    workAvailable -= InitialProcessing(period, workAvailable, message, messages);
                    if (workAvailable <= 0) break;
                }

                workAvailable -= ProcessMessage(period, workAvailable, message);
                if (workAvailable<=0) break;

            }

            //double a = Satisfaction.Last();

        }

        /// <summary>
        /// Either do work on a message or check if delegates are complete
        /// TODO discard in a middle of work
        /// </summary>
        /// <param name="period">Current period</param>
        /// <param name="workAvailable">Amount of work actor currently able to perform</param>
        /// <param name="message">Current message</param>
        /// <returns>Work spent on current message</returns>
        private double ProcessMessage(int period, double workAvailable, Message message)
        {
            double workSpent = 0;

            // First time agents works on a message
            if (message.Status == Status.Accepted)
            {
                message.Status = Status.InWork;
                message.Begin = period;
            }

            if (message.Status == Status.InWork)
            { 

                // we can do everything right now
                if (message.AmountLeft <= workAvailable)
                {
                    workSpent = message.AmountLeft;
                    message.AmountDone += workSpent;

                    message.MarkComplete(period);
                } else
                {
                    // we can't do everything now '
                    workSpent = workAvailable;
                    message.AmountDone += workAvailable;
                    
                }
            }  else if (message.Status == Status.Delegated)
            {
                if (message.IsDelegatedCompleted())
                {
                    // Spend the rest of the period finalizing work on delegated message
                    message.AmountDone += workAvailable;
                    workAvailable = 0;

                    message.MarkComplete(period);
                }
            }

            // fix for any wrong computation
            if (workAvailable < workSpent)
            {
                workSpent = workAvailable;
            }
            return workSpent;
        }


        /// <summary>
        /// We can:
        /// - Discard
        /// - Delegate
        /// - Do work on it
        /// </summary>
        /// <param name="period">Current period</param>
        /// <param name="workAvailable">Amount of work actor currently able to perform</param>
        /// <param name="message">Current message</param>
        /// <param name="messages">All messages in a simulation</param>
        /// <returns></returns>
        private double InitialProcessing(int period, double workAvailable, Message message, List<Message> messages)
        {
            double workSpent = 0;

            // Can move mesasge status to Accepted, Discarded, Delegated
            InitialRouteMessage(message);


            if (message.Status == Status.Delegated)
            {
                DelegateMessage(period, message, messages);
                workSpent = 0.3;
            } else
            {
                workSpent = 0.1;
            }

            // fix for any wrong computation
            if (workAvailable < workSpent)
            {
                workSpent = workAvailable;
            }
            return workSpent;
        }

        private void DelegateMessage(int period, Message message, List<Message> messages)
        {
            int maxParts = Agents.Count;
            int parts = CalculateDelegationParts(message, maxParts);
            double amount = message.Amount / parts;
            int end = CalculateChilrenEnd(period, message, amount);

            foreach (Relationship rel in Agents.PickRandomAgents(parts))
            {
                Message newMsg = new Message(this, rel.Agent, message.Importance, period, end, amount);
                messages.Add(newMsg);
                newMsg.Parent = message;
                message.Children.Add(newMsg);
            }

        }

        private int CalculateChilrenEnd(int period, Message message, double amount)
        {
            int end = message.End;
            if (period < message.End)
            {
               end = Rnd.GetRandomNumber(period + 1, message.End);
            } else
            {
                end = period + Convert.ToInt32(Rnd.GetRandomNumber(100, 200) / 100 * amount);
            }

            return end;
        }

        private int CalculateDelegationParts(Message message, int maxParts)
        {
            int parts = maxParts;
            if (message.Amount < maxParts) {
                parts = 1;
            }

            return parts;

        }

        /// <summary>
        /// Set mesasge status to Accepted, Discarded, Delegated
        /// </summary>
        /// <param name="message"></param>
        private void InitialRouteMessage(Message message)
        {
            // acceptmessage with a probability of 95%
            if (Rnd.GetRandomBool(95))
            {
                // !!!!!!!!!! Delegate with probability of 80%
                if (Agents.Count > 0 && Rnd.GetRandomBool(80))
                {
                    message.Status = Status.Delegated;
                }
                else
                {
                    message.Status = Status.Accepted;
                }
                
            } else
            {
                message.Status = Status.Discarded;
            }
        }

        /// <summary>
        /// Work actor can perform during this period
        /// TODO tie to motivation, etc
        /// </summary>
        public double GetWorkAvailable(int period)
        {
            return 1;
        }

        /// <summary>
        /// Sort incoming active messages from most important to least
        /// </summary>
        /// <typeparam name="T">Currently messages</typeparam>
        /// <param name="source">Messages to sort</param>
        /// <returns></returns>
        public IEnumerable<T> OrderActiveMessages<T>(IEnumerable<T> source) where T: Message
        {
            return source.OrderByDescending(item => item.Importance);
        }

    }
}
