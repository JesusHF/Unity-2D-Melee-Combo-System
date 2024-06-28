using UnityEngine;

public abstract class State
{
    protected StateMachine _stateMachine { get; private set; }
    protected float _fixedtime { get; private set; }

    public virtual void OnEnter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void OnUpdate() { }

    public virtual void OnFixedUpdate()
    {
        _fixedtime += Time.deltaTime;
    }

    public virtual void OnLateUpdate() { }

    public virtual void OnExit() { }

    #region Passthrough Methods

    /// <summary>
    /// Returns the component of type T if the game object has one attached, null if it doesn't.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetComponent<T>() where T : Component { return _stateMachine.GetComponent<T>(); }

    #endregion
}
