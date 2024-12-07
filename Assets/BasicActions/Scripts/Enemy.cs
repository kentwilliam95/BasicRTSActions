using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : DamageableItem
{
    private NPCStateIdle _stateIdle;
    private NPCStateMove _stateMove;
    private NPCStateGetHit _stateGetHit;
    private NPCStateDead _stateDead;

    private StateMachine<Enemy> _stateMachine;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    public Animator Animator => _animator;
    public NavMeshAgent Agent => _agent;

    protected override void Start()
    {
        base.Start();
        _stateIdle = new NPCStateIdle();
        _stateMove = new NPCStateMove();
        _stateGetHit = new NPCStateGetHit();
        _stateDead = new NPCStateDead();

        _stateMachine = new StateMachine<Enemy>(this);
        _stateMachine.ChangeState(_stateIdle);
    }

    private void LateUpdate()
    {
        _stateMachine.OnUpdate();
    }

    public void Move()
    {
        if(!IsDead())
            _stateMachine.ChangeState(_stateMove);
    }

    public void Idle()
    {
        if(!IsDead())
            _stateMachine.ChangeState(_stateIdle);
    }

    public override void Damage(float value)
    {
        health = Mathf.Clamp(health - value, 0, 100);
        
        var go = PoolSpawn.Instance.Spawn(_getHitParticle);
        go.transform.position = transform.position;

        if (IsDead() && !(_stateMachine.CurrentState is NPCStateDead))
        {
            data.SpawnInGameMaterial(transform.position);
            _stateMachine.ChangeState(_stateDead);

            var deadParticle = PoolSpawn.Instance.Spawn(_deadParticle);
            deadParticle.transform.position = transform.position;
        }
    }
}