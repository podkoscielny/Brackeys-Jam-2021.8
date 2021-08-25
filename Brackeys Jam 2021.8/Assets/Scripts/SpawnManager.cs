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

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        InvokeRepeating(nameof(SpawnCharacter), 2f, 1f); // Spawn Characters

        InvokeRepeating(nameof(SpawnCorn), 2f, 4f);
    }

    void SpawnCharacter()
    {
        GameObject obj = _objectPooler.GetFromPool(Tags.Character);

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
}
