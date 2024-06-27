using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine stateMachine)
    {
        base.OnEnter(stateMachine);

        //Attack
        _attackIndex = 1;
        _duration = 0.5f;
        _animator.SetTrigger("Attack" + _attackIndex);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_fixedtime >= _duration)
        {
            if (_shouldCombo)
            {
                _stateMachine.SetNextState(new GroundComboState());
            }
            else
            {
                _stateMachine.SetNextStateToMain();
            }
        }
    }
}
