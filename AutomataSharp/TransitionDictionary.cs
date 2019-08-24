using System.Collections.Generic;

namespace AutomataSharp
{
    public class TransitionDictionary<TState, TAction> : Dictionary<(TState, TAction), TState>, ITransitionMechanism<TState, TAction>
    {
        public bool Accepts(TState state, TAction action)
        {
            return ContainsKey((state, action));
        }

        public TState Transition(TState state, TAction action)
        {
            return this[(state, action)];
        }
    }
}
