using SlayTheSpireMechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.SlayTheSpireMechanics.ActionSystemLogic
{
    public interface ICallbackBinding
    {
        public Type Type { get; }
        public List<Func<IEnumerator>> GainReactions(ICallback action);
        public void RemoveReaction(Delegate reaction);
    }
}
