using System;

namespace AutomataSharp
{
    public class MissingTransitionException<TState, TAction> : Exception
    {
        const string ERROR_MESSAGE = "Missing transition rule from state ({0}) on action ({1}).";

        public TState State { get; }
        public TAction Action { get; }

        public MissingTransitionException(TState state, TAction action)
            : base(string.Format(ERROR_MESSAGE, state.ToString(), action.ToString()))
        {
            State = state;
            Action = action;
        }

        public MissingTransitionException(TState state, TAction action, Exception innerException)
            : base(string.Format(ERROR_MESSAGE, state.ToString(), action.ToString()), innerException)
        {
            State = state;
            Action = action;
        }
    }
}
