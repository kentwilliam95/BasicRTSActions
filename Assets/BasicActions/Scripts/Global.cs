using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    public static int AnimAttackIndex;
    public static int AnimMoveIndex;
    public static int AnimPickIndex;
    public static int AnimIdleIndex;
    public static int AnimDeadIndex;

    public static void InitializeAnimationID()
    {
        AnimAttackIndex = Animator.StringToHash("Attack");
        AnimMoveIndex = Animator.StringToHash("Move");
        AnimPickIndex = Animator.StringToHash("Pick");
        AnimIdleIndex = Animator.StringToHash("Idle");
        AnimDeadIndex = Animator.StringToHash("Dead");
    }
}