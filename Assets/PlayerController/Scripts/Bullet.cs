using UnityEngine;

public class Bullet : MonoBehaviour
{
    //private PooledObject m_PooledObject;
    [SerializeField] private Rigidbody2D m_Rigibody;
    [SerializeField] private float m_Force;
    private void Start()
    {
         //m_PooledObject = GetComponent<PooledObject>();
    }
    private void Update()
    {
       // m_Rigibody.AddForce()
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //m_PooledObject?.RecycleSelf();
    }
}
