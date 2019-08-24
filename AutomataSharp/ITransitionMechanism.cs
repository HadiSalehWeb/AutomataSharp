namespace AutomataSharp
{
    public interface ITransitionMechanism<TState, TAction>
    {
        bool Accepts(TState state, TAction action);
        TState Transition(TState state, TAction action);
    }
}
