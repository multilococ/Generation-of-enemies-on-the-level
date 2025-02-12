using System.Collections.Generic;
using UnityEngine;

public class OrientationRandomazer
{
    public Vector3 GetRandomSpawnPointPosition(List<SpawnPoint> spawnPoints)
    {
        Vector3 spawnPosition = Vector3.zero;

        if (spawnPoints.Count > 0)
        {
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);

            spawnPosition = spawnPoints[randomSpawnPointIndex].transform.position;
        }

        return spawnPosition;
    }

    public Vector3 GetRandomDerection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
    }
}
