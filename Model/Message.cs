using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Agents.Utils;

namespace Agents.Model
{
   public class Message
    {
        public Actor Sender;
        public Actor Receiver;

        /// <summary>
        /// Period in which message was created
        /// </summary>
        public int Start;

        /// <summary>
        /// Period, in which work started on message
        /// </summary>
        public int Begin;

        /// <summary>
        /// Period, in which work should have ended on message
        /// </summary>
        public int End;

        /// <summary>
        /// Period, in which work ended on message
        /// </summary>
        public int EndReal;

        /// <summary>
        /// Number of periods required to finish the task
        /// </summary>
        public int Amount;

        /// <summary>
        /// Current status of completion from  0 to 1
        /// </summary>
        [Range(0, 1)]
        public double Completion = 0;

        /// <summary>
        /// Importance in the mind of Sender
        /// </summary>
        [Range(0, 1)]
        public double Importance;

        public Message Parent;

        public Status Status = Status.Created;

        public List<Message> Children = new List<Message>();

        public Message(Actor sender, Actor receiver, double importance, int start, int end, int amount)
        {
            Sender = sender;
            Receiver = receiver;
            Importance = importance;
            Start = start;
            End = end;
            Amount = amount;
        }

        public Message(Actor receiver, double importance, int start, int end, int amount)
        {
            Receiver = receiver;
            Importance = importance;
            Start = start;
            End = end;
            Amount = amount;
        }

        public static Message CreateRandom(Actor sender, Actor receiver, int start=0)
        {
            double importance = Rnd.GetRandomNumber(100)/100;
            int amount = Rnd.GetRandomNumber(10);
            int end = start + amount + Rnd.GetRandomNumber(10);

            Message Message = new Message(sender, receiver, importance, start, end, amount);
            return Message;
        }
    }
}
