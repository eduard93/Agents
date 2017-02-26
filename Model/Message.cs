using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agents.Model
{
   public class Message
    {
        public Actor Sender;
        public Actor Receiver;

        public int Start;
        public int End;
        public int EndReal;

        /// <summary>
        /// Number of periods required to finish the task
        /// </summary>
        public int Amount;

        public Message Parent;

        public Status Status = Status.Created;

        public List<Message> Children = new List<Message>();
    }
}
