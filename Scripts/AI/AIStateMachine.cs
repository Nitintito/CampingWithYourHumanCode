public class AIStateMachine
{
    public AIState CurrentState { get; private set; }

    public void Initialie(AIState state)
    {
        CurrentState = state;
        CurrentState.Enter();
    }

    public void ChangeState(AIState state)
    {
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
}
