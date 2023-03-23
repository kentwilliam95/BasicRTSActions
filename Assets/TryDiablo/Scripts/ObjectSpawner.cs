using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private Coroutine spawnCoroutine;
    [SerializeField] PoolSpawnID[] spawnInfos;
    [SerializeField] private int spawnAmount;
    [SerializeField] private float radius;

    public void Initialize()
    {
        if(spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);

        spawnCoroutine = StartCoroutine(SpawnIenumerator());
    }

    private IEnumerator SpawnIenumerator()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            var randIndex = Random.Range(0, spawnInfos.Length);
            var go = PoolSpawn.Instance.Spawn(spawnInfos[randIndex]);
            var pos = Random.insideUnitSphere * radius;
            pos.y = 0;
            go.transform.position = transform.position + pos;
        }
        yield return null;
    }
}
