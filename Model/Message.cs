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
        public int Create;

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
        public double Amount;

        /// <summary>
        /// Number of periods done to finish the task
        /// </summary>
        public double AmountDone = 0;

        /// <summary>
        /// Number of periods left to finish the task
        /// </summary>
        public double AmountLeft { get { return Amount - AmountDone; } }

        /// <summary>
        /// Importance in the mind of Sender
        /// </summary>
        [Range(0, 1)]
        public double Importance;

        public Message Parent;

        public Status Status = Status.Created;

        public List<Message> Children = new List<Message>();

        public Message(Actor sender, Actor receiver, double importance, int create, int end, int amount)
        {
            Sender = sender;
            Receiver = receiver;
            Importance = importance;
            Create = create;
            End = end;
            Amount = amount;
        }

        public Message(Actor receiver, double importance, int create, int end, int amount)
        {
            Receiver = receiver;
            Importance = importance;
            Create = create;
            End = end;
            Amount = amount;
        }

        public static Message CreateRandom(Actor sender, Actor receiver, int create= 0)
        {
            double importance = Rnd.GetRandomNumber(100)/100;
            int amount = Rnd.GetRandomNumber(10);
            int end = create + amount + Rnd.GetRandomNumber(10);

            Message Message = new Message(sender, receiver, importance, create, end, amount);
            return Message;
        }

        public bool IsDelegatedCompleted()
        {
            return Children.All(message => (message.Status == Status.Completed) || (message.Status == Status.Discarded));
        }

        public void MarkComplete(int period)
        {
            Status = Status.Completed;
            EndReal = period;
        }
    }
}
