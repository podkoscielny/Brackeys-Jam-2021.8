using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPool", menuName = "ScriptableObjects/ObjectPool")]
public class ObjectPool : ScriptableObject
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] List<Pool> pools;
    [SerializeField] Dictionary<string, Queue<GameObject>> poolDictionary;

    private void OnDisable() => poolDictionary = new Dictionary<string, Queue<GameObject>>();

    public void InitializePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public void AddToPool(string tag, GameObject instance)
    {
        if (!poolDictionary.ContainsKey(tag)) return;

        Queue<GameObject> pool = poolDictionary[tag];

        instance.SetActive(false);
        pool.Enqueue(instance);
    }

    public GameObject GetFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        else if (poolDictionary[tag].Count < 1)
        {
            GameObject desiredPrefab = pools.Find(p => p.tag == tag).prefab;
            GameObject desiredObject = Instantiate(desiredPrefab);

            return desiredObject;
        }
        else
        {
            Queue<GameObject> pool = poolDictionary[tag];
            GameObject objectToSpawn = pool.Dequeue();
            objectToSpawn.SetActive(true);

            return objectToSpawn;
        }
    }

    public bool IsTagInDictionary(string tag) => poolDictionary.ContainsKey(tag);
}