using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = TagSystem.Tags;

public class CornSpawner : MonoBehaviour
{
    [SerializeField] PoopSystem poopSystem;
    [SerializeField] ObjectPool objectPool;
    [SerializeField] Score score;
    [SerializeField] PickableCoords[] pickableSpawnPositions;

    private int _cornsSpawned = 0;

    private const float CORN_INTERVAL = 2f; 
    private const int CORN_LIMIT = 3;

    private void OnEnable()
    {
        PlayerHealth.OnGameOver += CancelOnGoingInterval;
        PoopSystem.OnPoopUpgrade += ResetPickedUpCorns;
    }

    private void OnDisable()
    {
        PlayerHealth.OnGameOver -= CancelOnGoingInterval;
        PoopSystem.OnPoopUpgrade -= ResetPickedUpCorns;
    }

    private void Start() => InvokeRepeating(nameof(SpawnCorn), 0f, CORN_INTERVAL);

    private void SpawnCorn()
    {
        int spawnedCorns = TagSystem.FindAllGameObjectsWithTag(Tags.Corn).Count;

        if (spawnedCorns >= CORN_LIMIT || _cornsSpawned >= poopSystem.ChargeGoal || !CanCornBeSpawned() || !objectPool.IsTagInDictionary(Tags.Corn)) return;

        int cornCoordsIndex = Random.Range(0, pickableSpawnPositions.Length);
        PickableCoords cornSpawnBounds = pickableSpawnPositions[cornCoordsIndex];

        float xPosition = Random.Range(cornSpawnBounds.leftBound.x, cornSpawnBounds.rightBound.x);
        Vector2 cornsPosition = new Vector2(xPosition, cornSpawnBounds.leftBound.y);

        _cornsSpawned++;

        objectPool.GetFromPool(Tags.Corn, cornsPosition);
    }

    private bool CanCornBeSpawned() => score.Value > poopSystem.CurrentPoop.PointsWorth * 3 && poopSystem.PoopChargeLevel < poopSystem.MAX_POOP_CHARGE_LEVEL;

    private void ResetPickedUpCorns() => _cornsSpawned = 0;

    private void CancelOnGoingInterval() => CancelInvoke(nameof(SpawnCorn));
}