using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private Dictionary<InGameMaterialSO, int> _inventory;
    public Dictionary<InGameMaterialSO, int> Inventory => _inventory;
    
    private PlayerStateAttack _stateAttack;
    private PlayerStateMove _stateMove;
    private PlayerStateIdle _stateIdle;
    private PlayerStatePick _statePick;
    private StateMachine<Player> _statemachine;
    private NavMeshPath path;

    [SerializeField] private ActorStatusSO _status;
    public ActorStatusSO Status => _status;

    [SerializeField] private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;

    [SerializeField] private Animator _animator;
    public Animator Animator => _animator;

    private void Start()
    {
        path = new NavMeshPath();
        _statemachine = new StateMachine<Player>(this);
        _stateIdle = new PlayerStateIdle();
        _stateMove = new PlayerStateMove();
        _stateAttack = new PlayerStateAttack();
        _statePick = new PlayerStatePick();

        _inventory = new Dictionary<InGameMaterialSO, int>();
        _statemachine.ChangeState(_stateIdle);
    }

    private void LateUpdate()
    {
        _statemachine.OnUpdate();
    }

    public void Move(Vector3 position)
    {
        if (ValidatePosition(position))
        {
            _stateMove.SetTarget(path);
            _statemachine.ChangeState(_stateMove);
        }
    }

    public bool ValidatePosition(Vector3 position)
    {
        Agent.CalculatePath(position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    public void Attack(IDamage iDamage)
    {
        _stateAttack.SetTarget(iDamage);
        _statemachine.ChangeState(_stateAttack);
    }

    public void Pick(IPickable iPickable)
    {
        _statePick.SetTarget(iPickable);
        _statemachine.ChangeState(_statePick);
    }

    public void PutInInventory(IPickable pickable)
    {
        var inGameMaterial = pickable.Pick();
        if (!_inventory.ContainsKey(inGameMaterial))
        {
            _inventory.Add(inGameMaterial, 1);
        }
        else
        {
            _inventory[inGameMaterial] += 1;
        }

        PoolSpawn.Instance.UnSpawn(pickable.Component.gameObject);
    }

    public void Idle()
    {
        _statemachine.ChangeState(_stateIdle);
    }
}
