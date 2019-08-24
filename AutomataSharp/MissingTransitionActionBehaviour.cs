namespace AutomataSharp
{
    /// <summary>
    /// How the state machine should behave when its transition mechanism is undefined on a certain input.
    /// </summary>
    public enum MissingTransitionActionBehaviour
    {
        /// <summary>
        /// When the transition function isn't defined on the current state with the current actions, throw an exception.
        /// </summary>
        Throw,
        /// <summary>
        /// When the transition function isn't defined on the current state with the current actions, stay in the same state.
        /// </summary>
        Ignore
    }
}
