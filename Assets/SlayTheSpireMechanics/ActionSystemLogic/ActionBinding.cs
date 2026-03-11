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
        public HashSet<Func<T, IAction>> Pre = new();
        public HashSet<Func<T, IAction>> Post = new();

        public HashSet<Func<T, IEnumerator>> PreAnimation = new();
        public HashSet<Func<T, IEnumerator>> PostAnimation = new();
        
        public Type Type { get; }

        public ActionBinding()
        {
            Type = typeof(T);
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator Trigger(IAction action, ReactionTiming reactionTiming)
        {
            HashSet<Func<T, IAction>> reactions = reactionTiming == ReactionTiming.Pre ? Pre : Post;
            HashSet<Func<T, IEnumerator>> chainReactions = reactionTiming == ReactionTiming.Pre ? PreAnimation : PostAnimation;
            
            if (action is T a)
            {
                foreach (var reaction in chainReactions)
                {
                    yield return reaction(a);
                }
                foreach (var reaction in reactions)
                {
                    IAction chain = reaction(a);
                    if (chain != null)
                    {
                        ActionSystem.Instance.AddActionToTop(action);
                    }
                    
                }
            }
            
        }
        
        public bool RemoveReaction(Delegate reaction, ReactionTiming timing)
        { 
            if (reaction is not Func<T, IAction> action) {return false;}
            
            return Unbind(action, timing);
        }

        public bool RemoveReactionAsync(Func<T, IEnumerator> reaction, ReactionTiming timing)
        {
            return UnbindAsync(reaction, timing);
        }

        public bool Unbind(Func<T, IAction> reaction, ReactionTiming timing)
        {
            if (reaction == null){ return false; }
            
            HashSet<Func<T, IAction>> reactions = timing == ReactionTiming.Pre ? Pre : Post;
            return reactions.Remove(reaction);
        }

        public bool UnbindAsync(Func<T, IEnumerator> reaction, ReactionTiming reactionTiming)
        {
            HashSet<Func<T, IEnumerator>> reactionSet = reactionTiming == ReactionTiming.Pre ? PreAnimation : PostAnimation;
            return  reactionSet.Remove(reaction);
        }

        public void Bind(Func<T, IAction> reaction, ReactionTiming reactionTiming)
        {
            HashSet<Func<T, IAction>> reactions = reactionTiming == ReactionTiming.Pre ? Pre : Post;
            reactions.Add(reaction);
        }

        public void BindAsync(Func<T, IEnumerator> reaction, ReactionTiming reactionTiming)
        {
            HashSet<Func<T, IEnumerator>> reactions = reactionTiming == ReactionTiming.Pre ? PreAnimation : PostAnimation;
            reactions.Add(reaction);
        }
    }
}
