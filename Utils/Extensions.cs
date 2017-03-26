using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agents.Model;

namespace Agents.Utils
{
    public static class Extensions
    {
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static T PickRandom<T>(this IEnumerable<T> source, T current)
        {
            return source.Where(item => !EqualityComparer<T>.Default.Equals(item, current)).PickRandom();
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static T PickRandomPrincipal<T>(this IEnumerable<T> source, T current) where T : Actor
        {
            int PrincipalLevel = current.Level - 1;
            return source.Where(item => item.Level == PrincipalLevel).PickRandom(current);
        }

        public static T PickRandomAgent<T>(this IEnumerable<T> source, T current) where T : Actor
        {
            int AgentLevel = current.Level + 1;
            return source.Where(item => item.Level == AgentLevel).PickRandom(current);
        }

        public static IEnumerable<T> PickRandomAgents<T>(this IEnumerable<T> source, int count) where T : Relationship
        {
            return source.Shuffle().Take(count);
        }

        public static T PickRandomActor<T>(this IEnumerable<T> source, int level) where T : Actor
        {
            return source.Where(item => item.Level == level).PickRandom();
        }

        public static IEnumerable<T> ActiveMessages<T>(this IEnumerable<T> source, Actor actor) where T : Message
        {
            return source.Where(item => item.Receiver == actor && (item.Status == Status.Created || item.Status == Status.Accepted || item.Status == Status.InWork || item.Status == Status.Delegated));
        }
    }
}
