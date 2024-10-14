using UnityEngine;
using System.Collections.Generic;
using System;

public class DesignPattersoP : MonoBehaviour
{
    [Serializable]
    private struct PooledObjectData
    {
        public GameObject Prefab;
        public string Name;
        public bool CanGrow;
        public bool PoolSize;
    }

    public event Action OnPoolCleanup;

    [SerializeField] private PooledObjectData[] m_ObjectData;
    private List<PooledObject>[] m_PooledObjects;
    private GameObject[] m_Pools;

    private void Awake()
    {
        //never use i or j doesn't tell purpose of for loop, min 3 characters
        int poolNum = m_ObjectData.Length;
        m_Pools = new GameObject[poolNum];
        m_PooledObjects = new List<PooledObject>[poolNum];
        for (int poolIndex = 0; poolIndex < poolNum; poolIndex++) 
        {
            //Create the pool parents for Hierarchy organisation
            GameManager Pool = new GameObject($"Pool: {m_ObjectData[poolIndex].Name}");
            Pool.transform.parent = transform;
            m_Pools[poolIndex] = Pool;
            m_PooledObjects[poolIndex] = new List<PooledObject> ();
            for (int objectIndex = 0; objectIndex < m_ObjectData[poolIndex].PoolSize; objectIndex++)
            {
               SpawnObject(poolIndex, objectIndex);
            }
            
            
            
            //m_Pools[poolIndex] = Pool;
            //m_PooledObjec
        }
    }

    public GameObject GetPoolObject(string name) 
    { 
         int poolCount = m_Pools.Length;
         int targetPool = -1;
        for (int pooledIndex = 0; pooledIndex < poolCount; pooledIndex++;)
        {
            if(m_)
        }
        //search for every object of the pool
          
    }
    
    public void RecycleObject(GameObject m_PooledObjects) 
    { 
    }

    public void RecycleObject(PooledObject poolRef)
    {

    }

    private PooledObject SpawnObject(int poolIndex, int objectIndex)
    {
        GameObject tempGO = Instantiate(m_ObjectData[poolIndex].Prefab, m_Pools[poolIndex].transform);
        PooledObject pooledRef = tempGO.AddComponent<PooledObject>();
        tempGO.name = m_ObjectData[poolIndex].Name;

        tempGO.SetActive(false);
        if(objectIndex >= m_PooledObjects[poolIndex].Count)
        {
            m_PooledObjects[poolIndex].Add(pooledRef);
        }
        else
        {
            m_PooledObjects[poolIndex].Insert(objectIndex, pooledRef);
        }
        pooledRef.Init(poolIndex, this);
        return pooledRef;
    }
}
