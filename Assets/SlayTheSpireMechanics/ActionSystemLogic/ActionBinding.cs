using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace SlayTheSpireMechanics
{
    public class ActionBinding<T> : IActionBinding where T : IAction
    {
        private HashSet<Func<T, IEnumerator>> PreReactions = new();
        private Func<T, IEnumerator> Performer;
        private HashSet<Func<T, IEnumerator>> PostReactions = new();

        public Type Type { get; }

        public ActionBinding()
        {
            Type = typeof(T);
        }
        
      
        public List<Func<IEnumerator>> GainReactions(IAction action, ReactionTiming reactionTiming)
        {
            HashSet<Func<T, IEnumerator>> reactions = reactionTiming == ReactionTiming.Pre ? PreReactions : PostReactions;
            
            if (action is T a)
            {
                List<Func<IEnumerator>> gatheredReactions = new();
                foreach (var reaction in reactions)
                {
                    gatheredReactions.Add(() => reaction(a));
                }
                return gatheredReactions;
            }
            return null;
        }
        public Func<IEnumerator> GainPerformer(IAction action)
        {
            {
            if (action is T a)
                if (Performer != null)
                return () => Performer(a);
            }
            return null;
        }
        
        public void RemoveReaction(Delegate reaction, ReactionTiming timing)
        { 
            if (reaction is not Func<T, IEnumerator> action) {return;}
            
            UnbindSubscribtion(action, timing);
        }


        public void UnbindSubscribtion(Func<T, IEnumerator> reaction, ReactionTiming timing)
        {
            if (reaction == null){ return; }
            
            HashSet<Func<T, IEnumerator>> reactions = timing == ReactionTiming.Pre ? PreReactions : PostReactions;
            reactions.Remove(reaction);
        }

        public void BindSubscribtion(Func<T, IEnumerator> reaction, ReactionTiming reactionTiming)
        {
            HashSet<Func<T, IEnumerator>> reactions = reactionTiming == ReactionTiming.Pre ? PreReactions : PostReactions;
            reactions.Add(reaction);
        }
        public void BindPerformer(Func<T, IEnumerator> performer)
        {
            Performer = performer;
        }
        public void UnbindPerformer(Func<T, IEnumerator> performer)
        {
            if (Performer == performer)
            {
                Performer = null;
            }
        }

    }
}
