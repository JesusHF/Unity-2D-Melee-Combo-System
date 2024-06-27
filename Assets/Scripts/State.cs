using UnityEngine;

public abstract class State
{
    public StateMachine StateMachine { get; private set; }

    protected float _fixedtime { get; set; }

    public virtual void OnEnter(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
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
    protected T GetComponent<T>() where T : Component { return StateMachine.GetComponent<T>(); }

    #endregion
}