using System;

namespace AutomataSharp
{
    public class TransitionFunction<TState, TAction> : ITransitionMechanism<TState, TAction>
    {
        public Func<TState, TAction, TState> Function { get; }

        public TransitionFunction(Func<TState, TAction, TState> function)
        {
            Function = function;
        }

        public bool Accepts(TState state, TAction action)
        {
            try
            {
                Function(state, action);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public TState Transition(TState state, TAction action)
        {
            return Function(state, action);
        }

        //public static implicit operator TransitionFunction<TState, TAction>(Func<TState, TAction, TState> func)
        //{
        //    return new TransitionFunction<TState, TAction>(func);
        //}
    }
}
