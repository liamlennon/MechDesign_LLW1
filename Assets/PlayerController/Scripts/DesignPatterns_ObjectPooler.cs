using System;
using System.Collections.Generic;
using UnityEngine;


public class DesignPatterns_ObjectPooler : MonoBehaviour
{
    // Setup our Pools in the inspector so we can easliy create new ones
    [Serializable]
    private struct PooledObjectData
    {
        public GameObject Prefab;
        public string Name;
        public int PoolSize;
        public bool CanGrow;
    }

    // create an event for cleaning up the objects
    public event Action OnPoolCleanup;


    [SerializeField] private PooledObjectData[] m_ObjectData;
    //We use a list of arrays here, we do this so we can have a dynamic amount of Objects in our "pools" (so they can grow)  with a fixed amount of "Pools" as we don't need to add new "Pools" at runtime
    private List<PooledObject>[] m_PooledObjects;
    private GameObject[] m_Pools;

    // We dont need to do so much here, but what we do is setup our "pools" nicely using empty GameObjects in our hierarchy for some nice organization :)
    private void Awake()
    {
        int poolNum = m_ObjectData.Length;
        m_Pools = new GameObject[poolNum];
        m_PooledObjects = new List<PooledObject>[poolNum];


        for (int poolIndex = 0; poolIndex < poolNum; poolIndex++)
        {
            //Create the pool parents for Hierarchy organisation
            GameObject Pool = new GameObject($"Pool: {m_ObjectData[poolIndex].Name}");
            Pool.transform.parent = transform;
            m_Pools[poolIndex] = Pool;
            m_PooledObjects[poolIndex] = new List<PooledObject>();
            for (int objectIndex = 0; objectIndex < m_ObjectData[poolIndex].PoolSize; objectIndex++)
            {
                SpawnObject(poolIndex, objectIndex);
            }
        }
    }

    // Here is our public function to get our objects during runtime
    public GameObject GetPooledObject(string name)
    {
        int poolCount = m_Pools.Length;
        int targetPool = -1;
        for (int poolIndex = 0; poolIndex < poolCount; poolIndex++)
        {
            if (m_Pools[poolIndex].name == $"Pool: {name}")
            {
                targetPool = poolIndex;
                break;
            }
        }


        Debug.Assert(targetPool >= 0, $"No Pool for objects by the name of {name}");


        int objectCount = m_PooledObjects[targetPool].Count;
        int targetObject = -1;


        for (int objectIndex = 0; objectIndex < objectCount; objectIndex++)
        {
            if (m_PooledObjects[targetPool][objectIndex] != null)
            {
                if (!m_PooledObjects[targetPool][objectIndex].m_Active)
                {
                    targetObject = objectIndex;
                    break;
                }
            }
            else
            {
                SpawnObject(targetPool, objectIndex);
                targetObject = objectIndex;
                break;
            }
        }

        // this next bit is to catch if we run out of Objects in our "pool", if we do and CanGrow is set to true, we can spawn a new object and give it an ID. If its set to false then we just return an warning message
        if (targetObject == -1)
        {
            if (m_ObjectData[targetPool].CanGrow)
            {
                SpawnObject(targetPool, objectCount);
                targetObject = objectCount;
            }
            else
            {
                Debug.LogWarning($"No {name} objects left in pool and no option for pool to grow. Returning NULL.");
                return null;
            }
        }

        // finally wereturn the object from the right pool, and bind it to the clean up event
        PooledObject toReturn = m_PooledObjects[targetPool][targetObject];
        toReturn.m_Active = true;
        OnPoolCleanup += toReturn.RecycleSelf;
        return toReturn.gameObject;
    }

    // Here we catch any objects that are tried to be sent back to the pool incorrectly, we can recylce it if it has a PooledObject component or throw out an error if it doesn't 
    public void RecycleObject(GameObject obj)
    {
        PooledObject poolRef = obj.GetComponent<PooledObject>();
        Debug.Assert(poolRef != null, $"Trying to recycle an object called {obj.name} that didnt come from the Object Pooler");
        RecycleObject(poolRef);
    }

    // This function is to Recycle our Pooled Objects once they have carried out whatever it is they needed to do!
    public void RecycleObject(PooledObject poolRef)
    {
        poolRef.transform.SetParent(m_Pools[poolRef.m_PoolIndex].transform);
        poolRef.gameObject.SetActive(false);
        poolRef.m_Active = false;
        OnPoolCleanup -= poolRef.RecycleSelf;
    }

    // Here we spawn all our objects in to our pools and if its a new object that was added at runtime because we ran out of objects and CanGrow was enabled, we can insert them to the right list
    private PooledObject SpawnObject(int poolIndex, int objectIndex)
    {
        GameObject tempGO = Instantiate(m_ObjectData[poolIndex].Prefab, m_Pools[poolIndex].transform);
        PooledObject pooledRef = tempGO.AddComponent<PooledObject>();
        tempGO.name = m_ObjectData[poolIndex].Name;

        tempGO.SetActive(false);
        if (objectIndex >= m_PooledObjects[poolIndex].Count)
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