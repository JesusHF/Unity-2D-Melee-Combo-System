using UnityEngine;

public class GroundFinisherState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        _attackIndex = 3;
        _duration = 0.5f;
        _animator.SetTrigger("Attack" + _attackIndex);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_fixedtime >= _duration)
        {
             StateMachine.SetNextStateToMain();
        }
    }
}
