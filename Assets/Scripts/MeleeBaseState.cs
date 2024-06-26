using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeBaseState : State
{
    // How long this state should be active for before moving on
    protected float _duration;
    // Cached animator component
    protected Animator _animator;
    // bool to check whether or not the next attack in the sequence should be played or not
    protected bool _shouldCombo;
    // The attack index in the sequence of attacks
    protected int _attackIndex;

    // The cached hit collider component of this attack
    protected Collider2D _hitCollider;
    // Cached already struck objects of said attack to avoid overlapping attacks on same target
    private List<Collider2D> _collidersDamaged;
    // The Hit Effect to Spawn on the afflicted Enemy
    private GameObject _hitEffectPrefab;
    // Input buffer Timer
    private float _attackPressedTimer = 0;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        _animator = GetComponent<Animator>();
        _collidersDamaged = new List<Collider2D>();
        _hitCollider = GetComponent<ComboCharacter>().hitbox;
        _hitEffectPrefab = GetComponent<ComboCharacter>().Hiteffect;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _attackPressedTimer -= Time.deltaTime;

        if (_animator.GetFloat("Weapon.Active") > 0f)
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _attackPressedTimer = 2;
        }

        if (_animator.GetFloat("AttackWindow.Open") > 0f && _attackPressedTimer > 0)
        {
            _shouldCombo = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(_hitCollider, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {

            if (!_collidersDamaged.Contains(collidersToDamage[i]))
            {
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                // Only check colliders with a valid Team Componnent attached
                if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
                {
                    GameObject.Instantiate(_hitEffectPrefab, collidersToDamage[i].transform);
                    Debug.Log("Enemy Has Taken:" + _attackIndex + "Damage");
                    _collidersDamaged.Add(collidersToDamage[i]);
                }
            }
        }
    }
}
