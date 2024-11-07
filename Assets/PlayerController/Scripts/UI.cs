using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image m_HealthEffect;
    [SerializeField] private float damageAlpha = 0.3f, damageFadeSpeed = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_HealthEffect.color.a != 0)
        {
            m_HealthEffect.color = new Color(m_HealthEffect.color.r, m_HealthEffect.color.g, m_HealthEffect.color.b, Mathf.MoveTowards(m_HealthEffect.color.a, 0f,
            damageFadeSpeed * Time.deltaTime));
        }
    }

    public void ShowDamage()
    {
        m_HealthEffect.color = new Color(m_HealthEffect.color.r, m_HealthEffect.color.g, m_HealthEffect.color.b, 0.3f);
    }

}
