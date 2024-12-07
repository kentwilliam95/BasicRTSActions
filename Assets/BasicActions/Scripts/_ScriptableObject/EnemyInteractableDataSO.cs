using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Enemy Status", fileName = "Enemy SO")]
public class EnemyInteractableDataSO : BaseInteractableDataSO
{
    [field: SerializeField] public float AttackDamage;
    [field: SerializeField] public float AttackInterval;
}
