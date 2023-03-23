using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class NPCStateIdle : IState<Enemy>
{
    private float duration;
    public void OnStateEnter(Enemy t)
    {
        t.Animator.CrossFade(Global.AnimIdleIndex, 0.1f);
        duration = Random.Range(1f, 2f);
    }

    public void OnStateExit(Enemy t)
    {
        
    }

    public void OnStateUpdate(Enemy t)
    {
        duration -= Time.deltaTime;
        if(duration <= 0f)
        {
            t.Move();
        }
    }
}
