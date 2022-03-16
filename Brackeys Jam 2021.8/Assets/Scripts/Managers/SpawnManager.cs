using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = TagSystem.Tags;

[System.Serializable]
public struct PickableCoords
{
    public Vector2 leftBound;
    public Vector2 rightBound;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] PickableCoords[] pickableSpawnPositions;
    [SerializeField] GameObject life;
    [SerializeField] ObjectPool objectPool;
    [SerializeField] PoopSystem poopSystem;
    [SerializeField] Score score;
    [SerializeField] ChaosStarsSystem chaosStarsSystem;

    private readonly Quaternion RIGHT_ROTATION = new Quaternion(0, 0, 0, 1);
    private readonly Quaternion LEFT_ROTATION = new Quaternion(0, 1, 0, 0);

    private float _neutralInterval = 3f;
    private float _hostileInterval = 0f;
    private float _cornInterval = 1f; //Set to larger

    private int _hostileLimit = 0;
    private int _cornsSpawned = 0;
    private int _cornLimit = 3;
    private int _lifeSpawnGoal = 3000;

    private const int SPAWN_LIFE_TARGET = 3000;
    private const int LIVES_LIMIT = 1;
    private const float SPAWN_MAX_Y = -1.98f;
    private const float SPAWN_MIN_Y = -2.8f;
    private const float SPAWN_X_RANGE = 16f;
    private const float CORN_MIN_POSITION_X = -7f;
    private const float CORN_MAX_POSITION_X = 7f;
    private const float CORN_POSITION_Y = 3f;

    private void Awake() => objectPool.InitializePool();

    void OnEnable()
    {
        PlayerHealth.OnGameOver += CancelOngoingInvokes;
        ChaosStarsSystem.OnChaosStarGained += ChangeSpawnIntervals;
        PoopSystem.OnPoopUpgrade += ResetPickedUpCorns;
        SceneController.OnGameStart += SetInvokes;
        Score.OnScoreUpdated += CheckLifeToBeSpawn;
    }

    void OnDisable()
    {
        PlayerHealth.OnGameOver -= CancelOngoingInvokes;
        ChaosStarsSystem.OnChaosStarGained -= ChangeSpawnIntervals;
        PoopSystem.OnPoopUpgrade -= ResetPickedUpCorns;
        SceneController.OnGameStart -= SetInvokes;
        Score.OnScoreUpdated -= CheckLifeToBeSpawn;
        CancelOngoingInvokes();
    }

    private void SpawnNeutral()
    {
        if (!objectPool.IsTagInDictionary(Tags.Character)) return;

        GetCharacterFromPool(Tags.Character);
    }

    private void SpawnHostile()
    {
        int spawnedHostiles = TagSystem.FindAllGameObjectsWithTag(Tags.Hostile).Count;

        if (!objectPool.IsTagInDictionary(Tags.Hostile) || spawnedHostiles >= _hostileLimit) return;

        GetCharacterFromPool(Tags.Hostile);
    }

    private void SpawnCorn()
    {
        int spawnedCorns = TagSystem.FindAllGameObjectsWithTag(Tags.Hostile).Count;

        if (spawnedCorns >= _cornLimit || _cornsSpawned >= poopSystem.ChargeGoal || !CanCornBeSpawned() || !objectPool.IsTagInDictionary(Tags.Corn)) return;

        int cornCoordsIndex = Random.Range(0, pickableSpawnPositions.Length);
        PickableCoords cornSpawnBounds = pickableSpawnPositions[cornCoordsIndex];

        float xPosition = Random.Range(cornSpawnBounds.leftBound.x, cornSpawnBounds.rightBound.x);
        Vector2 cornsPosition = new Vector2(xPosition, cornSpawnBounds.leftBound.y);

        _cornsSpawned++;

        objectPool.GetFromPool(Tags.Corn, cornsPosition);
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

    private void ResetPickedUpCorns() => _cornsSpawned = 0;

    private bool CanCornBeSpawned() => score.Value > poopSystem.CurrentPoop.pointsWorth * 3 && poopSystem.PoopChargeLevel < poopSystem.MAX_POOP_CHARGE_LEVEL;

    private void GetCharacterFromPool(Tags tag)
    {
        bool isMovingRight = Random.Range(0, 2) == 1;

        float randomX = isMovingRight ? -SPAWN_X_RANGE : SPAWN_X_RANGE;
        float randomY = Random.Range(SPAWN_MIN_Y, SPAWN_MAX_Y);
        Vector3 charactersPosition = new Vector3(randomX, randomY, randomY);
        Quaternion charactersRotation = isMovingRight ? RIGHT_ROTATION : LEFT_ROTATION;

        objectPool.GetFromPool(tag, charactersPosition, charactersRotation);
    }

    private void ChangeSpawnIntervals()
    {
        CancelOngoingInvokes();

        ChaosStar currentChaosStar = chaosStarsSystem.CurrentChaosStar;

        _neutralInterval = currentChaosStar.neutralSpawnRate;
        _hostileInterval = currentChaosStar.hostileSpawnRate;
        _hostileLimit = currentChaosStar.hostilesLimit;

        SetInvokes();
    }

    private void SetInvokes()
    {
        if (_neutralInterval != 0)
            InvokeRepeating(nameof(SpawnNeutral), 2f, _neutralInterval);

        if (_hostileInterval != 0)
            InvokeRepeating(nameof(SpawnHostile), 2f, _hostileInterval);

        if (_cornInterval != 0)
            InvokeRepeating(nameof(SpawnCorn), 2f, _cornInterval);
    }

    private void CancelOngoingInvokes()
    {
        CancelInvoke(nameof(SpawnNeutral));
        CancelInvoke(nameof(SpawnHostile));
        CancelInvoke(nameof(SpawnCorn));
    }
}