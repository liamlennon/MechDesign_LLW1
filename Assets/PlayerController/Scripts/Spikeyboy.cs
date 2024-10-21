using System.Runtime.CompilerServices;
using UnityEngine;

public class Spikeyboy : MonoBehaviour
{
   [SerializeField] private float m_DamageAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable SpikeTrap = collision.GetComponentInParent<IDamageable>();
        if (SpikeTrap == null ) { return; }
        SpikeTrap.ApplyDamage(m_DamageAmount, this);
    }
}