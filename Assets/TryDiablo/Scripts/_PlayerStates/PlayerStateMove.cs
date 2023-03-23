using Core;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStateMove : IState<Player>
{
    private NavMeshPath _path;
    public void OnStateEnter(Player t)
    {
        t.Animator.CrossFade(Global.AnimMoveIndex, 0.025f);
        
        t.Agent.SetPath(_path);
        t.Agent.velocity = Vector3.zero;
        t.Agent.isStopped = false;
        t.Agent.speed = 3;
    }

    public void OnStateExit(Player t)
    {

    }

    public void OnStateUpdate(Player t)
    {
        if(t.Agent.remainingDistance <= 0.1f)
        {
            t.Idle();
        }
    }

    public void SetTarget(NavMeshPath path)
    {
        _path = path;
    }
}
