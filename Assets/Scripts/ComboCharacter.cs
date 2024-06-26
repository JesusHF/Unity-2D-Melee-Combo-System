using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{
    private StateMachine _meleeStateMachine;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;

    void Start()
    {
        _meleeStateMachine = GetComponent<StateMachine>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && _meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            _meleeStateMachine.SetNextState(new GroundEntryState());
        }
    }
}
