using UnityEngine;


public class PooledObject : MonoBehaviour
{
    [HideInInspector] public int m_PoolIndex { get; private set; }
    public bool m_Active;
    DesignPatterns_ObjectPooler m_PoolerRef;

    // Here is our init chain to initialize our object 
    public void Init(int poolIndex, DesignPatterns_ObjectPooler poolerRef)
    {
        m_PoolIndex = poolIndex;
        m_PoolerRef = poolerRef;
        m_Active = false;
    }

    // our recycle function which we want to call to put this object back in the right pool
    public void RecycleSelf()
    {
        m_PoolerRef.RecycleObject(this);
    }

    // clean up for our object
    private void OnDestroy()
    {
        m_PoolerRef.OnPoolCleanup -= this.RecycleSelf;
    }
}