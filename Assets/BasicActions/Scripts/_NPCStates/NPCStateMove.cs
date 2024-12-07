using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.AI;

public class NPCStateMove : IState<Enemy>
{
    private bool _isFoundPath;
    private NavMeshPath _path;
    public void OnStateEnter(Enemy t)
    {
        if (_path == null)
            _path = new NavMeshPath();
        _isFoundPath = false;
    }

    public void OnStateExit(Enemy t)
    {

    }

    public void OnStateUpdate(Enemy t)
    {
        if(!_isFoundPath)
        {
            _isFoundPath = CalculatePath(t.Agent);
            if(_isFoundPath)
            {
                t.Agent.SetPath(_path);
                t.Animator.CrossFade(Global.AnimMoveIndex, 0.1f);
            }
            return;
        }

        if(t.Agent.remainingDistance <= 0.25f)
        {
            t.Idle();
        }
    }

    private bool CalculatePath(NavMeshAgent agent)
    {
        var nextDirection = Random.insideUnitCircle;
        float length = Random.Range(3f, 7f);

        var nextPos = agent.transform.position + new Vector3(nextDirection.x, 0f, nextDirection.y) * length;
        agent.CalculatePath(nextPos, _path);
        return _path.status == NavMeshPathStatus.PathComplete || _path.status == NavMeshPathStatus.PathPartial;
    }
}
