using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

[System.Serializable]
public struct CornCoords
{
    public Vector2 leftBound;
    public Vector2 rightBound;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] CornCoords[] cornSpawnPositions;

    private GameManager _gameManager;
    private ObjectPooler _objectPooler;

    private readonly Quaternion RIGHT_ROTATION = new Quaternion(0, 0, 0, 1);
    private readonly Quaternion LEFT_ROTATION = new Quaternion(0, 1, 0, 0);

    private float _neutralInterval = 3f;
    private float _hostileInterval = 0f;
    private float _cornInterval = 1f; //Set to larger

    private int _hostileLimit = 0;
    private int _cornsSpawned = 0;
    private int _cornLimit = 3;

    private const float SPAWN_MAX_Y = -1.98f;
    private const float SPAWN_MIN_Y = -2.8f;
    private const float SPAWN_X_RANGE = 21.5f;
    private const float CORN_MIN_POSITION_X = -7f;
    private const float CORN_MAX_POSITION_X = 7f;
    private const float CORN_POSITION_Y = 3f;

    void OnEnable()
    {
        GameManager.OnGameOver += CancelOngoingInvokes;
        GameManager.OnChaosStarGained += ChangeSpawnIntervals;
        GameManager.OnPoopUpgrade += ResetPickedUpCorns;
        SceneController.OnGameStart += SetInvokes;
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= CancelOngoingInvokes;
        GameManager.OnChaosStarGained -= ChangeSpawnIntervals;
        GameManager.OnPoopUpgrade -= ResetPickedUpCorns;
        SceneController.OnGameStart -= SetInvokes;
        CancelOngoingInvokes();
    }

    void Start()
    {
        _gameManager = GameManager.Instance;
        _objectPooler = ObjectPooler.Instance;
    }

    void SpawnNeutral()
    {
        GameObject nonHostile = _objectPooler.GetFromPoolInActive(Tags.Character);

        if (nonHostile != null)
        {
            SetCharactersPosition(nonHostile);
        }
    }

    void SpawnHostile()
    {
        GameObject[] hostiles = GameObject.FindGameObjectsWithTag(Tags.Hostile);

        if (hostiles.Length < _hostileLimit)
        {
            GameObject hostile = _objectPooler.GetFromPoolInActive(Tags.Hostile);

            if (hostile != null)
            {
                SetCharactersPosition(hostile);
            }
        }
    }

    void SpawnCorn()
    {
        GameObject[] corns = GameObject.FindGameObjectsWithTag(Tags.Corn);

        if (corns.Length < _cornLimit && _cornsSpawned < _gameManager.ChargeGoal && _gameManager.CanCornBeSpawn())
        {
            GameObject corn = _objectPooler.GetFromPool(Tags.Corn);

            if (corn != null)
            {
                int cornCoordsIndex = Random.Range(0, cornSpawnPositions.Length);
                CornCoords cornSpawnBounds = cornSpawnPositions[cornCoordsIndex];

                float xPosition = Random.Range(cornSpawnBounds.leftBound.x, cornSpawnBounds.rightBound.x);
                corn.transform.position = new Vector2(xPosition, cornSpawnBounds.leftBound.y);

                _cornsSpawned++;
            }
        }
    }

    void ResetPickedUpCorns() => _cornsSpawned = 0;

    void SetCharactersPosition(GameObject character)
    {
        bool isMovingRight = Random.Range(0, 2) == 1;

        float randomX = isMovingRight ? -SPAWN_X_RANGE : SPAWN_X_RANGE;
        float randomY = Random.Range(SPAWN_MIN_Y, SPAWN_MAX_Y);
        Vector3 charactersPosition = new Vector3(randomX, randomY, randomY);

        character.transform.position = charactersPosition;
        character.transform.rotation = isMovingRight ? RIGHT_ROTATION : LEFT_ROTATION;

        character.SetActive(true);
    }

    void ChangeSpawnIntervals(int chaosStarsAmount)
    {
        CancelOngoingInvokes();

        _neutralInterval = _gameManager.CurrentChaosStar.neutralSpawnRate;
        _hostileInterval = _gameManager.CurrentChaosStar.hostileSpawnRate;
        _hostileLimit = _gameManager.CurrentChaosStar.hostilesLimit;
        Debug.Log($"netural: {_neutralInterval} | hostileInterval: {_hostileInterval} | hostileLimit: {_hostileLimit}");

        SetInvokes();
    }

    void SetInvokes()
    {
        if (_neutralInterval != 0)
            InvokeRepeating(nameof(SpawnNeutral), 2f, _neutralInterval);

        if (_hostileInterval != 0)
            InvokeRepeating(nameof(SpawnHostile), 2f, _hostileInterval);

        if (_cornInterval != 0)
            InvokeRepeating(nameof(SpawnCorn), 2f, _cornInterval);
    }

    void CancelOngoingInvokes()
    {
        CancelInvoke(nameof(SpawnNeutral));
        CancelInvoke(nameof(SpawnHostile));
        CancelInvoke(nameof(SpawnCorn));
    }
}
