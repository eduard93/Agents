using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Agents.Model
{
    class Relationship
    {
        public Relationship(Actor principal, Actor agent, double importanceAgent, double importancePrincipal)
        {
            Principal = principal;
            Agent = agent;
            ImportanceAgent = importanceAgent;
            ImportancePrincipal = importancePrincipal;

            Principal.Agents.Add(this);
            Agent.Principals.Add(this);
        }

        public Actor Principal;

        public Actor Agent;

        [Range(0, 1)]
        /// <summary>
        /// Importance of a relationship to an agent
        /// </summary>
        public double ImportanceAgent;

        [Range(0, 1)]
        /// <summary>
        /// Importance of a relationship to a principal
        /// </summary>
        public double ImportancePrincipal;
    }
}
