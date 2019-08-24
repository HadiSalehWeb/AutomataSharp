namespace AutomataSharp
{

    public partial struct TransitionRule<TState, TAction> : ITransitionMechanism<TState, TAction>
    {
        internal readonly TransitionRuleType transitionRuleType;
        private readonly TState fromState;
        private readonly TAction action;
        private readonly TState toState;

        private TransitionRule(TransitionRuleType transitionRuleType, TState fromState, TAction action, TState toState)
        {
            this.transitionRuleType = transitionRuleType;
            this.fromState = fromState;
            this.action = action;
            this.toState = toState;
        }

        public static TransitionRule<TState, TAction> Regular(TState fromState, TAction action, TState toState)
        {
            return new TransitionRule<TState, TAction>(TransitionRuleType.Regular, fromState, action, toState);
        }

        public static TransitionRule<TState, TAction> StateAgnostic(TAction action, TState toState)
        {
            return new TransitionRule<TState, TAction>(TransitionRuleType.StateAgnostic, default, action, toState);
        }

        public static TransitionRule<TState, TAction> ActionAgnostic(TState fromState, TState toState)
        {
            return new TransitionRule<TState, TAction>(TransitionRuleType.ActionAgnostic, fromState, default, toState);
        }

        public static TransitionRule<TState, TAction> LastResort(TState toState)
        {
            return new TransitionRule<TState, TAction>(TransitionRuleType.LastResort, default, default, toState);
        }

        public bool Accepts(TState state, TAction action)
        {
            return
                (transitionRuleType == TransitionRuleType.Regular && state.Equals(fromState) && action.Equals(this.action)) ||
                (transitionRuleType == TransitionRuleType.StateAgnostic && action.Equals(this.action)) ||
                (transitionRuleType == TransitionRuleType.ActionAgnostic && state.Equals(fromState)) ||
                (transitionRuleType == TransitionRuleType.LastResort);
        }

        public TState Transition(TState state, TAction action)
        {
            if (!Accepts(state, action)) throw new MissingTransitionException<TState, TAction>(state, action);
            return toState;
        }

        /// <summary>
        /// Regular transition.
        /// </summary>
        /// <param name="transition"></param>
        public static implicit operator TransitionRule<TState, TAction>(((TState, TAction), TState) transition)
        {
            return Regular(transition.Item1.Item1, transition.Item1.Item2, transition.Item2);
        }

        /// <summary>
        /// State-agnostic transition.
        /// </summary>
        /// <param name="transition"></param>
        public static implicit operator TransitionRule<TState, TAction>((TAction, TState) transition)
        {
            return StateAgnostic(transition.Item1, transition.Item2);
        }

        /// <summary>
        /// Action-agnostic transition.
        /// </summary>
        /// <param name="transition"></param>
        public static implicit operator TransitionRule<TState, TAction>((TState, TState) transition)
        {
            return ActionAgnostic(transition.Item1, transition.Item2);
        }

        /// <summary>
        /// Last resort transition.
        /// </summary>
        /// <param name="transition"></param>
        public static implicit operator TransitionRule<TState, TAction>(TState transition)
        {
            return LastResort(transition);
        }
    }
}