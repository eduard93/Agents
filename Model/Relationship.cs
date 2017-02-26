using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Agents.Model
{
    public class Relationship
    {
        public Relationship(Actor principal, Actor agent, double importanceAgent, double importancePrincipal = 1)
        {
            Relationship Relationship = principal.Agents.Find(item => (item.Principal == principal) && (item.Agent == agent));

            if (Relationship == null)
            {
                Principal = principal;
                Agent = agent;
                ImportanceAgent = importanceAgent;
                ImportancePrincipal = importancePrincipal;

                Principal.Agents.Add(this);
                Agent.Principals.Add(this);
            } else
            {
                Relationship.ImportanceAgent += importanceAgent;
                Relationship.ImportancePrincipal += ImportancePrincipal;
            }
        }

        public Actor Principal;

        public Actor Agent;

        [Range(1, 10)]
        /// <summary>
        /// Importance of a relationship to an agent
        /// </summary>
        public double ImportanceAgent;

        [Range(1, 10)]
        /// <summary>
        /// Importance of a relationship to a principal
        /// </summary>
        public double ImportancePrincipal;
    }
}
