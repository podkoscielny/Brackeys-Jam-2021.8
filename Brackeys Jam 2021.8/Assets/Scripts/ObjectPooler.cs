using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] List<Pool> pools;
    [SerializeField] Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPooler Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    void Start() => InitializePool();

    void InitializePool()
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
        Queue<GameObject> pool = poolDictionary[tag];

        instance.SetActive(false);
        pool.Enqueue(instance);
    }


    public GameObject GetFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag) || poolDictionary[tag].Count < 1)
        {
            return null;
        }

        Queue<GameObject> pool = poolDictionary[tag];
        GameObject objectToSpawn = pool.Dequeue();
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    public GameObject GetFromPoolInActive(string tag)
    {
        if (!poolDictionary.ContainsKey(tag) || poolDictionary[tag].Count < 1)
        {
            return null;
        }

        Queue<GameObject> pool = poolDictionary[tag];
        GameObject objectToSpawn = pool.Dequeue();

        return objectToSpawn;
    }

    public bool IsTagInDictionary(string tag) => poolDictionary.ContainsKey(tag);
}
