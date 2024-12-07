using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStatePick : IState<Player>
{
    private IPickable _pickable;
    private bool _isPlayPickAnimation;
    private bool _isAlreadyPickupToInventory;
    private float totalAnimationDuration;
    private float _pickTriggerDuration;
    private NavMeshPath _path;
    public void OnStateEnter(Player t)
    {
        if (_path == null)
            _path = new NavMeshPath();

        t.Agent.CalculatePath(_pickable.Component.transform.position, _path);
        t.Agent.SetPath(_path);
        t.Animator.CrossFade(Global.AnimMoveIndex, 0.025f);

        t.Agent.speed = 3f;
        t.Agent.isStopped = false;

        _pickTriggerDuration = 32f/30f;
        totalAnimationDuration = 86f/30f;
    }

    public void OnStateExit(Player t)
    {
        _isPlayPickAnimation = false;
        _isAlreadyPickupToInventory = false;
        totalAnimationDuration = 86f/30f;
    }

    public void OnStateUpdate(Player t)
    {
        if (t.Agent.remainingDistance < 0.1f && !_isPlayPickAnimation)
        {
            t.Animator.CrossFade(Global.AnimPickIndex, 0.1f);
            _isPlayPickAnimation = true;
        }

        if(_isPlayPickAnimation)
        {
            _pickTriggerDuration -= Time.deltaTime;
            if(_pickTriggerDuration <= 0 && !_isAlreadyPickupToInventory)
            {
                t.PutInInventory(_pickable);
                _isAlreadyPickupToInventory = true;
            }

            totalAnimationDuration -= Time.deltaTime;
            if(totalAnimationDuration <= 0f)
            {
                t.Idle();
            }
        }
    }

    public void SetTarget(IPickable pickable)
    {
        _pickable = pickable;
    }
}
