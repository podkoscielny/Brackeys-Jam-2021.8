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
    [SerializeField] ObjectPool objectPool;
    [SerializeField] PoopSystem poopSystem;
    [SerializeField] Score score;
    [SerializeField] ChaosStarsSystem chaosStarsSystem;

    private readonly Quaternion RIGHT_ROTATION = new Quaternion(0, 0, 0, 1);
    private readonly Quaternion LEFT_ROTATION = new Quaternion(0, 1, 0, 0);

    private float _neutralInterval = 3f;
    private float _hostileInterval = 0f;

    private int _hostileLimit = 0;

    private const float SPAWN_MAX_Y = -1.98f;
    private const float SPAWN_MIN_Y = -2.8f;
    private const float SPAWN_X_RANGE = 16f;

    private void Awake() => objectPool.InitializePool();

    private void OnEnable()
    {
        PlayerHealth.OnGameOver += CancelOngoingInvokes;
        ChaosStarsSystem.OnChaosStarGained += ChangeSpawnIntervals;
        SceneController.OnGameStart += SetInvokes;
    }

    private void OnDisable()
    {
        PlayerHealth.OnGameOver -= CancelOngoingInvokes;
        ChaosStarsSystem.OnChaosStarGained -= ChangeSpawnIntervals;
        SceneController.OnGameStart -= SetInvokes;
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
    }

    private void CancelOngoingInvokes()
    {
        CancelInvoke(nameof(SpawnNeutral));
        CancelInvoke(nameof(SpawnHostile));
    }
}
