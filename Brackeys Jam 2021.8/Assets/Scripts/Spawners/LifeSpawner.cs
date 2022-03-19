using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpawner : MonoBehaviour
{
    [SerializeField] Score score;
    [SerializeField] GameObject life;
    [SerializeField] PickableCoords[] pickableSpawnPositions;

    private int _lifeSpawnGoal = 3000;

    private const int SPAWN_LIFE_TARGET = 3000;

    private void OnEnable()
    {
        Score.OnScoreUpdated += CheckLifeToBeSpawn;
    }

    private void OnDisable()
    {
        Score.OnScoreUpdated -= CheckLifeToBeSpawn;
    }

    private void CheckLifeToBeSpawn()
    {
        if (score.Value >= _lifeSpawnGoal)
        {
            _lifeSpawnGoal += SPAWN_LIFE_TARGET;
            SpawnLife();
        }
    }

    private void SpawnLife()
    {
        int lifeCoordsIndex = Random.Range(0, pickableSpawnPositions.Length);
        PickableCoords cornSpawnBounds = pickableSpawnPositions[lifeCoordsIndex];

        float xPosition = Random.Range(cornSpawnBounds.leftBound.x, cornSpawnBounds.rightBound.x);
        life.transform.position = new Vector2(xPosition, cornSpawnBounds.leftBound.y);

        life.SetActive(true);
    }
}