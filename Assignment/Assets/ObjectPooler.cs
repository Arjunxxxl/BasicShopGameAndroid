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
        public int size = 4;
    }

    [Header("Normal Pool")]
    public List<Pool> pool;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region SingleTon
    public static ObjectPooler Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SetUpPool();
    }
    #endregion

    private void Start()
    {
    }

    #region Normal pool
    void SetUpPool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);

                obj.SetActive(false);
                obj.transform.SetParent(transform);

                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFormPool(string tileTag, Vector3 pos, Quaternion rot)
    {

        if (!poolDictionary.ContainsKey(tileTag))
        {
            Debug.Log(tileTag);
            return null;
        }

        GameObject objToSpawn = poolDictionary[tileTag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos;
        objToSpawn.transform.localRotation = rot;
        poolDictionary[tileTag].Enqueue(objToSpawn);

        return objToSpawn;
    }
    #endregion
}