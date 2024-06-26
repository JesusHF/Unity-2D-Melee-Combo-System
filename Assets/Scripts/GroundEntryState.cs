using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        _attackIndex = 1;
        Duration = 0.5f;
        _animator.SetTrigger("Attack" + _attackIndex);
        Debug.Log("Player Attack " + _attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_fixedtime >= Duration)
        {
            if (_shouldCombo)
            {
                StateMachine.SetNextState(new GroundComboState());
            }
            else
            {
                StateMachine.SetNextStateToMain();
            }
        }
    }
}
