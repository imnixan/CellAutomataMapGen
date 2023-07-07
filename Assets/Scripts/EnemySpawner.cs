using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int SpawnEnemyChanse = 30;
    private const int SpawnCarChanse = 30;

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
                .Initialize(layerId);
        }
    }

    public void TrySpawnMovable(Vector2 position, int layerId, System.Random randomGenerator)
    {
        if (randomGenerator.Next(0, 100) < SpawnCarChanse)
        {
            // Instantiate(carPrefab, position, new Quaternion()).GetComponent<Enemy>().Initialize(layerId);
        }
        else
        {
            // Instantiate(tankPrefab, position, new Quaternion()).GetComponent<Enemy>().Initialize(layerId);
        }
    }
}
