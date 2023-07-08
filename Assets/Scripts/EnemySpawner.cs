using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int SpawnEnemyChanse = 5;
    private const int SpawnCarChanse = 50;

    [SerializeReference]
    private GameObject gunPrefab,
        tankPrefab,
        carPrefab;

    private GameObject currentPrefab;

    public void SpawnGun(Vector2 position, int layerId, System.Random randomGenerator)
    {
        currentPrefab = gunPrefab;
        InstanceEnemy(position, layerId, randomGenerator);
    }

    private void InstanceEnemy(Vector2 position, int layerId, System.Random randomGenerator)
    {
        if (randomGenerator.Next(0, 100) < SpawnEnemyChanse)
        {
            Instantiate(currentPrefab, position, new Quaternion())
                .GetComponent<Enemy>()
                .Initialize(layerId, randomGenerator);
        }
    }

    public void TrySpawnMovable(Vector2 position, int layerId, System.Random randomGenerator)
    {
        if (randomGenerator.Next(0, 100) < SpawnCarChanse)
        {
            currentPrefab = carPrefab;
            InstanceEnemy(position, layerId, randomGenerator);
        }
        else
        {
            currentPrefab = tankPrefab;
            InstanceEnemy(position, layerId, randomGenerator);
        }
    }
}
