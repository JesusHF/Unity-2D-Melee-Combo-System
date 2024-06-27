using UnityEngine;

public class GroundFinisherState : MeleeBaseState
{
    public override void OnEnter(StateMachine stateMachine)
    {
        base.OnEnter(stateMachine);

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
             _stateMachine.SetNextStateToMain();
        }
    }
}
