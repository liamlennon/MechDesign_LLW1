using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigibody;
    [SerializeField] private float m_Force;
    private PooledObject m_PooledObject;

    private void Start()
    {
        m_PooledObject = GetComponent<PooledObject>();
    }
    private void Update()
    {
        m_Rigibody.AddForce(Vector2.up * m_Force, ForceMode2D.Force);
    }
    private void OnTriggerEnter2D()
    {
        m_PooledObject.RecycleSelf();   
    }
}
