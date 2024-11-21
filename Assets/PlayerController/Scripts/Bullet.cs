using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigibody;   
    [SerializeField] private float m_Force;
    [SerializeField] private float m_Damage;
    private PooledObject m_PooledObject;
  

    private void Start()
    {
        m_PooledObject = GetComponent<PooledObject>();
    }
    private void Update()
    {
        m_Rigibody.AddForce(Vector2.right * m_Force, ForceMode2D.Force);
    }
    private void OnTriggerEnter2D()
    {
        m_PooledObject.RecycleSelf();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            IDamageable Bullet = collision.gameObject.GetComponent<IDamageable>();
            if (Bullet != null) { return;}
            Bullet.ApplyDamage(m_Damage, this);
        }
    }
}
