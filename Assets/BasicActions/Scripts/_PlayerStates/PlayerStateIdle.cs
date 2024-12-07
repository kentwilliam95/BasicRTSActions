using Core;
using UnityEngine;

public class PlayerStateIdle : IState<Player>
{
   
    public void OnStateEnter(Player t)
    {
        t.Animator.CrossFade(Global.AnimIdleIndex, 0.25f);
    }

    public void OnStateExit(Player t)
    {
        
    }

    public void OnStateUpdate(Player t)
    {
        
    }
}
