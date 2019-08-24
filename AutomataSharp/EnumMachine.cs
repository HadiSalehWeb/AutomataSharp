using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomataSharp
{
    public class EnumMachine<TState, TAction> : FiniteStateMachine<TState, TAction>
        where TState : Enum
    {
        public EnumMachine(TState initialState, ITransitionMechanism<TState, TAction> transitionFunction, List<TState> acceptedStates)
            : base(initialState, Enum.GetValues(typeof(TState)).Cast<TState>().ToList(), transitionFunction, acceptedStates) { }

        public EnumMachine(TState initialState, ITransitionMechanism<TState, TAction> transitionFunction)
            : base(initialState, Enum.GetValues(typeof(TState)).Cast<TState>().ToList(), transitionFunction) { }

    }
}
