using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class SpawnManager : MonoBehaviour
{
    private GameManager _gameManager;
    private ObjectPooler _objectPooler;

    private readonly Quaternion RIGHT_ROTATION = new Quaternion(0, 0, 0, 1);
    private readonly Quaternion LEFT_ROTATION = new Quaternion(0, 1, 0, 0);

    private float _neutralInterval = 3f;
    private float _hostileInterval = 0f;
    private float _cornInterval = 1f; //Set to larger

    private int _hostileLimit = 0;
    private int _cornLimit = 10;

    private const float SPAWN_MAX_Y = -1.98f;
    private const float SPAWN_MIN_Y = -2.8f;
    private const float SPAWN_X_RANGE = 10f;
    private const float CORN_MIN_POSITION_X = -7f;
    private const float CORN_MAX_POSITION_X = 7f;
    private const float CORN_POSITION_Y = 3f;

    void Awake()
    {
        _gameManager = GameManager.Instance;
        _objectPooler = ObjectPooler.Instance;
    }

    void OnEnable() => GameManager.OnChaosStarGained += ChangeSpawnIntervals;

    void OnDisable()
    {
        GameManager.OnChaosStarGained -= ChangeSpawnIntervals;
        CancelOngoingInvokes();
    }

    void Start() => SetInvokes();

    void SpawnNeutral()
    {
        if (_gameManager.IsGameOver) return;

        GameObject nonHostile = _objectPooler.GetFromPoolInActive(Tags.Character);

        if (nonHostile != null)
        {
            SetCharactersPosition(nonHostile);
        }
    }

    void SpawnHostile()
    {
        if (_gameManager.IsGameOver) return;

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
        if (_gameManager.IsGameOver) return;

        GameObject[] corns = GameObject.FindGameObjectsWithTag(Tags.Corn);

        if (corns.Length < _cornLimit)
        {
            GameObject corn = _objectPooler.GetFromPool(Tags.Corn);

            if (corn != null)
            {
                float xPosition = Random.Range(CORN_MIN_POSITION_X, CORN_MAX_POSITION_X);
                corn.transform.position = new Vector2(xPosition, CORN_POSITION_Y);
            }
        }
    }

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

        switch (chaosStarsAmount)
        {
            case 1:
                _neutralInterval = 1.5f;
                _hostileInterval = 6f;
                _hostileLimit = 1;
                break;

            case 2:
                _neutralInterval = 2f;
                _hostileInterval = 4.5f;
                _hostileLimit = 2;
                break;

            case 3:
                _neutralInterval = 4f;
                _hostileInterval = 3f;
                _hostileLimit = 4;
                break;

            case 4:
                _neutralInterval = 7f;
                _hostileInterval = 2f;
                _hostileLimit = 6;
                break;

            case 5:
                _neutralInterval = 0f;
                _hostileInterval = 2f;
                _hostileLimit = 8;
                break;

            default:
                _hostileInterval = 2f;
                _hostileLimit = 8;
                break;
        }

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
