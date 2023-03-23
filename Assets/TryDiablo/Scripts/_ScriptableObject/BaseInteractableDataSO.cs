using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactable Data", fileName = "Interactable Data")]
public class BaseInteractableDataSO : ScriptableObject
{
    public int spawnAmount;
    public PoolSpawnID spawnedObject;

    public int health;

    public void SpawnInGameMaterial(Vector3 position)
    {
        if (spawnAmount <= 0)
            return;

        if(spawnedObject == null)
            return;

        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject go = PoolSpawn.Instance.Spawn(spawnedObject);
            var rig = go.GetComponent<Rigidbody>();
            go.transform.position = position;

            Vector3 posModifier = Random.insideUnitSphere * 2f;
            posModifier.y = 0f;
            go.transform.position += posModifier;
        }
    }
}
