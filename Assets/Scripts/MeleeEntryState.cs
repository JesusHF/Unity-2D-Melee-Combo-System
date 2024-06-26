public class MeleeEntryState : State
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        State nextState = (State)new GroundEntryState();
        StateMachine.SetNextState(nextState);
    }
}
