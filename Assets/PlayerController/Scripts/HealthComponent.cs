using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public event Action<float, float, float> OnDamage;
    public event Action<MonoBehaviour> OnDeath;

    public GameObject Player;

    [SerializeField] private Image m_HealthBar;
    [SerializeField] private Image m_LowHealthEffect;
    [SerializeField] private bool m_HasHealthBar;
    [SerializeField] private float m_MaxHealth;
    [SerializeField] private float m_CurrentHealth;
    [SerializeField] private float damageAlpha = 0.3f, damageFadeSpeed = 3f;


    private void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public void Awake()
    {
        
    }


    private void Update()
    {
        if (m_HasHealthBar)
        {
            m_HealthBar.fillAmount = m_CurrentHealth / 100;
        }

        if (m_LowHealthEffect.color.a != 0)
        {
            m_LowHealthEffect.color = new Color(m_LowHealthEffect.color.r, m_LowHealthEffect.color.g, m_LowHealthEffect.color.b, Mathf.MoveTowards(m_LowHealthEffect.color.a, 0f,
            damageFadeSpeed * Time.deltaTime));
        }
    }

    public void ApplyDamage(float damage, MonoBehaviour causer)
    {

        float change = Mathf.Min(m_CurrentHealth, damage);
        m_CurrentHealth -= change;

        OnDamage?.Invoke(m_CurrentHealth, m_MaxHealth, change);
        if (m_CurrentHealth <= 0.0f) { OnDeath?.Invoke(causer); }

       

    }
    public void ShowDamage()
    {
        m_LowHealthEffect.color = new Color(m_LowHealthEffect.color.r, m_LowHealthEffect.color.g, m_LowHealthEffect.color.b, 0.3f);
    }
}
