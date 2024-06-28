using UnityEngine;
using UnityEngine.Assertions;

public enum StateMachineType
{
    Player
}

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }
    [SerializeField] private StateMachineType _type;

    private State _mainState = null;
    private State _nextState = null;

    private void Awake()
    {
        Validate();
        SetNextStateToMain();
    }

    private void Update()
    {
        if (_nextState != null)
        {
            SetState(_nextState);
        }

        CurrentState?.OnUpdate();
    }

    private void FixedUpdate()
    {
        CurrentState?.OnFixedUpdate();
    }

    private void LateUpdate()
    {
        CurrentState?.OnLateUpdate();
    }

    private void Validate()
    {
        if (_mainState == null)
        {
            if (_type == StateMachineType.Player)
            {
                _mainState = new IdleCombatState();
            }
        }
        Assert.IsNotNull(_mainState);
    }

    private void SetState(State newState)
    {
        _nextState = null;
        CurrentState?.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter(this);
    }

    public void SetNextState(State newState)
    {
        if (newState != null)
        {
            _nextState = newState;
        }
    }

    public void SetNextStateToMain()
    {
        _nextState = _mainState;
    }
}
