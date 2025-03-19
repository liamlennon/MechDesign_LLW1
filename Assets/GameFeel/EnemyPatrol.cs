using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody2D m_EnemyRB;
    public float m_Speed = 3f;
    private bool m_MovingRight = true;

    public Transform m_GroundCheck;
    public LayerMask m_GroundLayer;
    private bool m_IsGrounded;
    private float m_TimeNotGrounded = 0f;
    public float m_DestroyTime = 3f;

    public float m_AttackRange = 5f;
    public GameObject m_BulletPrefab;
    public Transform m_FirePoint;
    public float m_FireRate = 1.5f;
    private float m_NextFireTime = 0f;

    public float m_Damage = 10f;

    void Start()
    {
        m_EnemyRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ground Check
        m_IsGrounded = Physics2D.OverlapCircle(m_GroundCheck.position, 0.2f, m_GroundLayer);

        

        // Movement Logic
        Move();

        // Check for player and fire bullet
        DetectAndFire();
    }

    void Move()
    {
        float moveDirection = m_MovingRight ? 1 : -1;
        m_EnemyRB.linearVelocity = new Vector2(moveDirection * m_Speed, m_EnemyRB.linearVelocity.y);

        // Flip direction if hitting a wall or reaching an edge
        if (!Physics2D.OverlapCircle(m_GroundCheck.position, 0.2f, m_GroundLayer))
        {
            Flip();
        }
    }

    void Flip()
    {
        m_MovingRight = !m_MovingRight;
        transform.Rotate(0f, 180f, 0f); // Flip the sprite
    }

    void DetectAndFire()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= m_AttackRange)
            {
                FireBullet();
               
            }
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(m_BulletPrefab, m_FirePoint.position, Quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        float direction = m_MovingRight ? 1 : -1;
        bulletRB.linearVelocity = new Vector2(direction * 10f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamageable player = collision.GetComponent<IDamageable>();
            if (player != null)
            {
                player.ApplyDamage(m_Damage, this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
    }
}
