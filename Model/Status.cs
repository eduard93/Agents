using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agents.Model
{
    public enum Status
    {
        Discarded = -1,
        Created = 0,
        Accepted = 1,
        InWork = 2,
        Delegated = 3,
        Completed = 4       
    }
}
