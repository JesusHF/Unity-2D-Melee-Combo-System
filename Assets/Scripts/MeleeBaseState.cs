using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeBaseState : State
{
    protected float _duration;
    protected bool _shouldCombo;
    protected int _attackIndex;
    protected Animator _animator;
    protected Collider2D _hitCollider;

    private List<Collider2D> _collidersDamaged;
    private GameObject _hitEffectPrefab;
    private float _attackPressedTimer = 0; // Input buffer Timer

    public override void OnEnter(StateMachine stateMachine)
    {
        base.OnEnter(stateMachine);
        _animator = GetComponent<Animator>();
        _collidersDamaged = new List<Collider2D>();
        _hitCollider = GetComponent<ComboCharacter>().Hitbox;
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
