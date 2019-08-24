using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomataSharp
{
    public class TransitionRuleset<TState, TAction> : List<TransitionRule<TState, TAction>>, ITransitionMechanism<TState, TAction>
    {
        public TransitionRuleset(params TransitionRule<TState, TAction>[] transitionRules) : base(transitionRules)
        {
            for (int i = 0; i < transitionRules.Length - 1; i++)
                if (transitionRules[i].transitionRuleType == TransitionRuleType.LastResort)
                    throw new ArgumentException("Only the very last TransitionRule can be a last resort.", nameof(transitionRules));
        }

        public bool Accepts(TState state, TAction action) => this.Any(rule => rule.Accepts(state, action));

        public TState Transition(TState state, TAction action) => this.First(rule => rule.Accepts(state, action)).Transition(state, action);
    }
}
