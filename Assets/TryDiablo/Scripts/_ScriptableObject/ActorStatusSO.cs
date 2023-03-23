using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Status", fileName = "Status")]
public class ActorStatusSO : ScriptableObject
{
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float AttackDamage { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float ChangeTargetRange { get; private set; }
    [field: SerializeField] public float AttackInterval { get; private set; }
}