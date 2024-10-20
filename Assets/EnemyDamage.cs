using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float m_DamageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable Enemy = collision.GetComponent<IDamageable>();
        if (Enemy == null) { return; }
        Enemy.ApplyDamage(m_DamageAmount, this);
    }

}
