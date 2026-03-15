using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlayTheSpireMechanics
{
    public interface IActionBinding
    {
        public Type Type { get; }
        public List<Func<IEnumerator>> GainReactions(IAction action, ReactionTiming reactionTiming);
        public Func<IEnumerator> GainPerformer(IAction action);
        public void RemoveReaction(Delegate reaction, ReactionTiming timing);
    }
}
