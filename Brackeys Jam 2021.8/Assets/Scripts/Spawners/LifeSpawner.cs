using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpawner : MonoBehaviour
{
    [SerializeField] Score score;
    [SerializeField] GameObject life;
    [SerializeField] PickableCoords pickableCoords;

    private int _lifeSpawnGoal = 3000;

    private const int SPAWN_LIFE_TARGET = 3000;

    private void OnEnable() => Score.OnScoreUpdated += CheckLifeToBeSpawn;

    private void OnDisable() =>  Score.OnScoreUpdated -= CheckLifeToBeSpawn;

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
        life.transform.position = pickableCoords.GetRandomPosition();

        life.SetActive(true);
    }
}
