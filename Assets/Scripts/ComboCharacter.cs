using UnityEngine;

public class ComboCharacter : MonoBehaviour
{
    private StateMachine _meleeStateMachine;

    [SerializeField] public Collider2D Hitbox;
    [SerializeField] public GameObject Hiteffect;

    private void Start()
    {
        _meleeStateMachine = GetComponent<StateMachine>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            _meleeStateMachine.SetNextState(new GroundEntryState());
        }
    }
}
