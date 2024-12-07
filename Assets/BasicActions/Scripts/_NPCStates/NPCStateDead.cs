using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class NPCStateDead : IState<Enemy>
{
    private float deadDuration;
    public void OnStateEnter(Enemy t)
    {
        t.Agent.isStopped = true;
        t.Animator.Play(Global.AnimDeadIndex);   
        deadDuration = 2.5f;
    }

    public void OnStateExit(Enemy t)
    {
        
    }

    public void OnStateUpdate(Enemy t)
    {
        deadDuration -= Time.deltaTime;
        if(deadDuration <= 0)
        {
            PoolSpawn.Instance.UnSpawn(t.gameObject);
        }
    }
}
