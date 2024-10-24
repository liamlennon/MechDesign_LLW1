using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public event Action<float, float, float> OnDamage;
    public event Action<MonoBehaviour> OnDeath;

    public GameObject Player;

    [SerializeField] private Image m_healthBar;
    [SerializeField] private bool m_HasHealthBar;
    [SerializeField] private float m_MaxHealth;
    [SerializeField] private float m_CurrentHealth;

    private void Awake()
    {
        m_CurrentHealth = m_MaxHealth;
    }
    private void Update()
    {
        if(m_HasHealthBar)
        {
            m_healthBar.fillAmount = m_CurrentHealth / 100;
        }
    }

    public void ApplyDamage(float damage, MonoBehaviour causer)
    {
        float change = Mathf.Min(m_CurrentHealth, damage);
        m_CurrentHealth -= change;

        OnDamage?.Invoke(m_CurrentHealth, m_MaxHealth, change);
        if(m_CurrentHealth <= 0.0f) { OnDeath?.Invoke(causer);  }

       
    }
}
