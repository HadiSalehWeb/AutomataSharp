using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomataSharp
{
    public class FiniteStateMachine<TState, TAction>
    {
        #region Properties

        #region Immutable

        public TState InitialState { get; }
        public List<TState> States { get; }
        public ITransitionMechanism<TState, TAction> TransitionMechanism { get; }
        public List<TState> AcceptedStates { get; }
        public MissingTransitionActionBehaviour Behaviour { get; }

        #endregion

        #region Mutable

        public TState CurrentState { get; private set; }
        public List<TAction> History { get; private set; }

        #endregion

        #endregion

        #region Constructors

        public FiniteStateMachine(TState initialState, List<TState> states, ITransitionMechanism<TState, TAction> transitionMechanism,
            List<TState> acceptedStates, MissingTransitionActionBehaviour behaviour = MissingTransitionActionBehaviour.Ignore)
        {
            if (!states.Contains(initialState))
                throw new ArgumentException("The initial state must be an element of the set of states.", nameof(initialState));

            if (acceptedStates.Except(states).Any())
                throw new ArgumentException("Accpedted states must be a subset of the set of states.", nameof(acceptedStates));

            InitialState = initialState;
            States = states;
            TransitionMechanism = transitionMechanism;
            AcceptedStates = acceptedStates;
            Behaviour = behaviour;

            ResetState();
            ResetHistory();
        }

        public FiniteStateMachine(TState initialState, List<TState> states, ITransitionMechanism<TState, TAction> transitionMechanism,
            MissingTransitionActionBehaviour behaviour = MissingTransitionActionBehaviour.Ignore)
            : this(initialState, states, transitionMechanism, new List<TState>(), behaviour) { }

        #endregion

        #region Methods

        public void ResetState()
        {
            CurrentState = InitialState;
        }

        public void ResetHistory()
        {
            History = new List<TAction>();
        }

        public void Feed(TAction action)
        {
            if (TransitionMechanism.Accepts(CurrentState, action))
                CurrentState = TransitionMechanism.Transition(CurrentState, action);
            else if (Behaviour == MissingTransitionActionBehaviour.Throw)
                throw new MissingTransitionException<TState, TAction>(CurrentState, action);

            History.Add(action);
        }

        public void Feed(IEnumerable<TAction> sequence)
        {
            foreach (var symbol in sequence)
                Feed(symbol);
        }

        public bool Accepts(IEnumerable<TAction> sequence)
        {
            ResetState();

            Feed(sequence);

            return AcceptedStates.Contains(CurrentState);
        }

        #endregion
    }
}
