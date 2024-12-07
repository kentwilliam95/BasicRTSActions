using Core;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStateAttack : IState<Player>
{
    private bool isAttack;
    private bool isTriggerAttack;
    private NavMeshPath _path;
    private IDamage _target;
    
    private float attackDelayCountdown;
    private float totalAttackDuration;
    private Vector3 initialRotation;
    public void OnStateEnter(Player t)
    {
        if(_path == null)
            _path = new NavMeshPath();

        t.Animator.CrossFade(Global.AnimMoveIndex, 0.025f);
        t.Agent.CalculatePath(_target.Component.transform.position, _path);
        t.Agent.SetPath(_path);
        t.Agent.speed = 3f;
        attackDelayCountdown = 10f/ 30f;
        totalAttackDuration = 1f;
    }

    public void OnStateExit(Player t)
    {
        t.Agent.updateRotation = true;
        t.Agent.isStopped = false;
        t.Agent.speed = 0f;
        isAttack = false;
        isTriggerAttack = false;
    }

    public void OnStateUpdate(Player t)
    {
        if(t.Agent.remainingDistance <= t.Status.AttackRange && !isAttack)
        {
            t.Agent.isStopped = true;
            t.Animator.CrossFade(Global.AnimAttackIndex, 0.1f);
            t.Agent.updateRotation = false;

            initialRotation = t.transform.forward;

            isAttack = true;
        }
        
        if(isAttack)
        {
            attackDelayCountdown -= Time.deltaTime;
            totalAttackDuration -= Time.deltaTime;

            Vector3 toRotation = (_target.Component.transform.position - t.transform.position).normalized;
            t.transform.rotation = Quaternion.LookRotation(toRotation);

            if(attackDelayCountdown <= 0 && !isTriggerAttack)
            {
                _target.Damage(1);
                isTriggerAttack = true;
                
                if(_target.IsDead())
                    t.Idle();
            }

            if(Vector3.Distance(t.transform.position, _target.Component.transform.position) > t.Status.ChangeTargetRange)
            {
                t.Idle();
            }
            
            if(totalAttackDuration <= 0)
            {
                totalAttackDuration = 1f;
                attackDelayCountdown = 10f/30f;
                isTriggerAttack = false;
            }
        }
    }

    public void SetTarget(IDamage target)
    {
        _target = target;
    }
}
