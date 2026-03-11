using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


namespace SlayTheSpireMechanics
{
    public class ActionSystem : Singleton<ActionSystem>
    {
        [SerializeField] public bool isPerforming = false;
        private static Dictionary<Type, Action<IAction>> PerformerDictionary = new();
        private static Dictionary<Type, HashSet<IActionBinding>> BindingDictionary = new();
        
        private LinkedList<IAction> ActionQueue = new();
        
        public static void AttachPerformer<T>(Action<T> performer) where T : IAction
        {
            Type actionType = typeof(T);
            AttachPerformer(actionType, a => performer((T)a));
        }

        public static void AttachPerformer(Type actionType, Action<IAction> performer)
        {
            PerformerDictionary[actionType] = performer;
        }

        public static void DetachPerformer<T>() where T : IAction
        {
            Type actionType = typeof(T);
            DetachPerformer(actionType);
        }
        public static void DetachPerformer(Type actionType)
        {
            if (PerformerDictionary.ContainsKey(actionType))
            {
                PerformerDictionary.Remove(actionType);
            }
        }

        public static ActionBinding<T> GetBinding<T>() where T : IAction
        {
            Type type = typeof(T);
            if (BindingDictionary.TryGetValue(type, out var set))
            {
                foreach (IActionBinding binding in set)
                {
                    if (binding is ActionBinding<T> rBinding)
                    {
                        return rBinding;
                    }
                }
            }
            ActionBinding<T> newActionBinding = new ActionBinding<T>();

            if (!BindingDictionary.TryGetValue(type, out var hashSet))
            {
                hashSet = new HashSet<IActionBinding>();
                BindingDictionary[type] = hashSet;
            }
            hashSet.Add(newActionBinding);
            return newActionBinding;
        }

        public static void Subscribe<T>(Func<T, IAction> action, ReactionTiming timing) where T : IAction
        {
            var binding = GetBinding<T>();
            if (binding != null)
            {
                binding.Bind(action, timing);
            }
        }
        public static void SubscribeAsync<T>(Func<T, IEnumerator> action, ReactionTiming timing) where T : IAction
        {
            var binding = GetBinding<T>();
            if (binding != null)
            {
                binding.BindAsync(action, timing);
            }
        }
        public static void Unsubscribe<T>(Func<T, IAction> action, ReactionTiming timing) where T : IAction
        {
            var binding = GetBinding<T>();
            if (binding != null)
            {
                binding.Unbind(action, timing);
            }
        }
        public static void UnsubscribeAsync<T>(Func<T, IEnumerator> action, ReactionTiming timing) where T : IAction
        {
            var binding = GetBinding<T>();
            if (binding != null)
            {
                binding.UnbindAsync(action, timing);
            }
        }

        public void PerformPerformer(IAction action)
        {
            if (PerformerDictionary.TryGetValue(action.GetType(), out var performer))
            {
                 performer(action);
            }
            
        }

        public IEnumerable<IEnumerator> PerformSubscribers(IAction action, ReactionTiming timing)
        {
            
            if (BindingDictionary.TryGetValue(action.GetType(), out var hashSet))
            {
                foreach (var binding in hashSet)
                {
                    yield return binding.Trigger(action, timing);
                }
            }
            
        }

        public void AddActionToTop(IAction action)
        {
            ActionQueue.AddFirst(action);
        }

        public void AddActionToBottom(IAction action)
        {
            ActionQueue.AddLast(action);
        }

        public void AddActionAfter(IAction action, IAction afterAction)
        {
            if (ActionQueue.Contains(action))
            {
                ActionQueue.AddAfter(ActionQueue.Find(action), afterAction);
            }
        }
        
        public void AddActionBefore(IAction action, IAction beforeAction)
        {
            if (ActionQueue.Contains(action))
            {
                ActionQueue.AddAfter(ActionQueue.Find(action), beforeAction);
            }
        }

        public void CheckQueue()
        {
            if (ActionQueue?.First?.Value != null && !isPerforming)
            {
                IAction action = ActionQueue.First.Value;
                ActionQueue.RemoveFirst();
                isPerforming = true;
                StartCoroutine(Flow(action, () => isPerforming = false));
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator Flow(IAction action, Action onComplete = null) 
        {
            
            if (action == null) { yield break;}
            
            
            yield return StartCoroutine(WaitForAllCoroutines(PerformSubscribers(action, ReactionTiming.Pre)));
            PerformPerformer(action);
            yield return StartCoroutine(WaitForAllCoroutines(PerformSubscribers(action, ReactionTiming.Post)));
            
            
            onComplete?.Invoke();
        }
        private IEnumerator WaitForAllCoroutines(IEnumerable<IEnumerator> coroutines)
        {
            List<Coroutine> activeCoroutines = new List<Coroutine>();
            
            foreach (var coroutine in coroutines)
            {
                if (coroutine != null)
                {
                    activeCoroutines.Add(StartCoroutine(coroutine));
                }
            }
            
            // Ждём завершения всех запущенных корутин
            foreach (var activeCoroutine in activeCoroutines)
            {
                if (activeCoroutine != null)
                {
                    yield return activeCoroutine;
                }
            }
        }

        public void Update()
        {
            CheckQueue();
        }
    }
}