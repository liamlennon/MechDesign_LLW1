using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [HideInInspector] public int m_PoolIndex {  get; private set; }
    private bool m_Active;
    private DesignPattersoP m_PoolerRef;

    public void Init(int poolIndex, DesignPattersoP poolRef)
    {
        m_PoolIndex = poolIndex;
        m_PoolerRef = poolRef;
        m_Active = false;
    }
    public void RecycleSelf()
    {
        m_PoolerRef.RecycleObject(this);
    }
    private void OnDestroy()
    {
        m_PoolerRef.OnPoolCleanup -= this.RecycleSelf;
    }
}
