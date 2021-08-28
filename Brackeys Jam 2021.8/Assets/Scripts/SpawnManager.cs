using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class SpawnManager : MonoBehaviour
{
    private ObjectPooler _objectPooler;

    private float _spawnMaxY = -1.98f;
    private float _spawnMinY = -2.8f;
    private float _spawnXRange = 10f;
    private float _cornMinPositionX = -7f;
    private float _cornMaxPositionX = 0f;
    private float _cornPositionY = 3.29f;

    private float _neutralInterval = 0f;
    private float _hostileInterval = 2f;
    private float _cornInterval = 4f;

    private int _hostileLimit = 7;

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
        Enemy objScript = obj.GetComponent<Enemy>();

        if (obj != null)
        {
            bool isMovingRight = Random.Range(0, 2) == 1;

            float randomX = isMovingRight ? -_spawnXRange : _spawnXRange;
            float randomY = Random.Range(_spawnMinY, _spawnMaxY);
            Vector3 charactersPosition = new Vector3(randomX, randomY, randomY);
            obj.transform.position = charactersPosition;

            objScript.SetFacing(isMovingRight);
        }
    }

    void SpawnHostile()
    {
        GameObject[] hostiles = GameObject.FindGameObjectsWithTag(Tags.Hostile);

        if (hostiles.Length < _hostileLimit)
        {
            GameObject obj = _objectPooler.GetFromPool(Tags.Hostile);

            if (obj != null)
            {
                bool isMovingRight = Random.Range(0, 2) == 1;

                float randomX = isMovingRight ? -_spawnXRange : _spawnXRange;
                float randomY = Random.Range(_spawnMinY, _spawnMaxY);
                Vector3 charactersPosition = new Vector3(randomX, randomY, randomY);
                obj.transform.position = charactersPosition;
            }
        }
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

    void SetCharactersPosition(GameObject character)
    {
        float randomX = Random.Range(0, 1) > 0.5f ? _spawnXRange : -_spawnXRange;
        float randomY = Random.Range(_spawnMinY, _spawnMaxY);
        Vector3 charactersPosition = new Vector3(randomX, randomY, randomY);
        character.transform.position = charactersPosition;
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
                _hostileInterval = 2f;
                _hostileLimit = 6;
                break;

            case 5:
                _hostileInterval = 2f;
                _hostileLimit = 8;
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
