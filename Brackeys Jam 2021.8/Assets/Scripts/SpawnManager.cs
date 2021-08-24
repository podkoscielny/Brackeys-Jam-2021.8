using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private ObjectPooler _objectPooler;
    private Vector2 _spawnPosition = new Vector2(-10f, -2f);

    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        InvokeRepeating(nameof(SpawnCharacter), 2f, 1f);
    }

    void SpawnCharacter()
    {
        GameObject obj = _objectPooler.GetFromPool("Character");
        obj.transform.position = _spawnPosition;
    }
}
