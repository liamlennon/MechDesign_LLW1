using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject m_PointA;
    public GameObject m_PointB;
    private Rigidbody2D m_EnemyRB;
    private Transform m_CurrentPoint;
    public StatefulRaycastSensor2D m_EnemySensor;
    public float m_Speed;
    [SerializeField] private float m_Damage;

    void Start()
    {
         m_EnemyRB = GetComponent<Rigidbody2D>();
        m_CurrentPoint = m_PointB.transform;
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 point = m_CurrentPoint.position - transform.position;
        if(m_CurrentPoint == m_PointB.transform)
        {
            m_EnemyRB.linearVelocity = new Vector2(m_Speed, 0);
        }
        else
        {
            m_EnemyRB.linearVelocity = new Vector2(m_Speed, 0);
        }
        if (Vector2.Distance(transform.position, m_CurrentPoint.position) < 0.5f && m_CurrentPoint == m_PointB.transform) 
        {
            m_CurrentPoint = m_PointB.transform;
        }
        if (Vector2.Distance(transform.position, m_CurrentPoint.position) < 0.5f && m_CurrentPoint == m_PointB.transform)
        {
            m_CurrentPoint = m_PointB.transform;    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable Enemy = collision.GetComponentInParent<IDamageable>();
        if (Enemy == null) { return; }
        Enemy.ApplyDamage(m_Damage, this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(m_PointB.transform.position, 0.5f);
    }

}
