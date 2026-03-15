using SlayTheSpireMechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.SlayTheSpireMechanics.ActionSystemLogic
{
    public class CallbackBinding<T> : ICallbackBinding where T : ICallback
    {
        private HashSet<Func<T, IEnumerator>> Reactions = new();
        public Type Type => typeof(T);

        public List<Func<IEnumerator>> GainReactions(ICallback action)
        {
            if (action is T a)
            {
                List<Func<IEnumerator>> gatheredReactions = new();
                foreach (var reaction in Reactions)
                {
                    gatheredReactions.Add(() => reaction(a));
                }
                return gatheredReactions;
            }
            return null;
        }

        public void RemoveReaction(Delegate reaction)
        {
            if (reaction is not Func<T, IEnumerator> r) { return; }
            Reactions.Remove(r);
        }
        public void Bind(Func<T, IEnumerator> reaction)
        {
            Reactions.Add(reaction);
        }
    }
}
