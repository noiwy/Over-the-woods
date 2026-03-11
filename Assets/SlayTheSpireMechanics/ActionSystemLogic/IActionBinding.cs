using System;
using System.Collections;
using System.Threading.Tasks;

namespace SlayTheSpireMechanics
{
    public interface IActionBinding
    {
        public Type Type { get; }
        public IEnumerator Trigger(IAction action, ReactionTiming reactionTiming);
        public bool RemoveReaction(Delegate reaction, ReactionTiming timing);
    }
}
