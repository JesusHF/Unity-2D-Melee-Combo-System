using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }
    public string CustomName;

    private State _mainStateType;
    private State _nextState;

    private void Awake()
    {
        SetNextStateToMain();
    }

    void Update()
    {
        if (_nextState != null)
        {
            SetState(_nextState);
        }

        CurrentState?.OnUpdate();
    }

    private void SetState(State _newState)
    {
        _nextState = null;
        CurrentState?.OnExit();
        CurrentState = _newState;
        CurrentState.OnEnter(this);
    }

    public void SetNextState(State _newState)
    {
        if (_newState != null)
        {
            _nextState = _newState;
        }
    }

    private void LateUpdate()
    {
        CurrentState?.OnLateUpdate();
    }

    private void FixedUpdate()
    {
        CurrentState?.OnFixedUpdate();
    }

    public void SetNextStateToMain()
    {
        _nextState = _mainStateType;
    }

    private void OnValidate()
    {
        if (_mainStateType == null)
        {
            if (CustomName == "Combat")
            {
                _mainStateType = new IdleCombatState();
            }
        }
    }
}