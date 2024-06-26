using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }
    [SerializeField] private string _customName;

    private State _mainStateType;
    private State _nextState;

    private void Awake()
    {
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

    private void LateUpdate()
    {
        CurrentState?.OnLateUpdate();
    }

    private void FixedUpdate()
    {
        CurrentState?.OnFixedUpdate();
    }

    private void OnValidate()
    {
        if (_mainStateType == null)
        {
            if (_customName == "Combat")
            {
                _mainStateType = new IdleCombatState();
            }
        }
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
        _nextState = _mainStateType;
    }
}