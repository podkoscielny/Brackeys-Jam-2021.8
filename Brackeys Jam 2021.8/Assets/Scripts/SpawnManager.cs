using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class SpawnManager : MonoBehaviour
{
    private ObjectPooler _objectPooler;

    private Vector2 _spawnPosition = new Vector2(-10f, -2f);
    private float _cornMinPositionX = -7f;
    private float _cornMaxPositionX = 0f;
    private float _cornPositionY = 2.15f;

    private float _neutralInterval = 2f;
    private float _hostileInterval = 0f;
    private float _cornInterval = 4f;

    void OnEnable()
    {
        GameManager.OnChaosStarGained += ChangeSpawnIntervals;
    }

    void OnDisable()
    {
        GameManager.OnChaosStarGained -= ChangeSpawnIntervals;
        CancelOngoingInvokes();
    }

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        SetInvokes();
    }

    void SpawnNeutral()
    {
        GameObject obj = _objectPooler.GetFromPool(Tags.Character);

        if (obj != null)
            obj.transform.position = _spawnPosition;
    }

    void SpawnHostile()
    {
        GameObject obj = _objectPooler.GetFromPool(Tags.Hostile);

        if (obj != null)
            obj.transform.position = _spawnPosition;
    }

    void SpawnCorn()
    {
        GameObject obj = _objectPooler.GetFromPool(Tags.Corn);
        if (obj != null)
        {
            float xPosition = Random.Range(_cornMinPositionX, _cornMaxPositionX);
            obj.transform.position = new Vector2(xPosition, _cornPositionY);
        }
    }

    void ChangeSpawnIntervals(int chaosStarsAmount)
    {
        CancelOngoingInvokes();

        switch (chaosStarsAmount)
        {
            case 1:
                _neutralInterval = 1.5f;
                _hostileInterval = 6f;
                break;

            case 2:
                _neutralInterval = 2f;
                _hostileInterval = 4.5f;
                break;

            case 3:
                _neutralInterval = 4f;
                _hostileInterval = 3f;
                break;

            case 4:
                _hostileInterval = 2f;
                break;

            case 5:
                _hostileInterval = 2f;
                break;

            default:
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
