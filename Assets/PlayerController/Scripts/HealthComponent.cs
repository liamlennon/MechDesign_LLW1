using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public event Action<float, float, float> OnDamage;
    public event Action<MonoBehaviour> OnDeath;

    [SerializeField] private float m_MaxHealth;
    private float m_CurrentHealth;

    private void Awake()
    {
        m_CurrentHealth = m_MaxHealth;
    }
    public void ApplyDamage(float damage, MonoBehaviour causer)
    {
        float change = Mathf.Min(m_CurrentHealth, damage);
        m_CurrentHealth -= change;

        OnDamage?.Invoke(m_CurrentHealth, m_MaxHealth, change);
        if(m_CurrentHealth <= 0.0f) { OnDeath?.Invoke(causer);  }
    }
}
