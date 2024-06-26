using UnityEngine;

public class GroundComboState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        _attackIndex = 2;
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
                StateMachine.SetNextState(new GroundFinisherState());
            }
            else
            {
                StateMachine.SetNextStateToMain();
            }
        }
    }
}
