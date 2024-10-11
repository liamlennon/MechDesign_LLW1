using System.Runtime.CompilerServices;
using UnityEngine;

public class Spikeyboy : MonoBehaviour
{
   [SerializeField] private float m_DamageAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable suqishything = collision.GetComponentInParent<IDamageable>();
        if (suqishything == null ) { return; }
        suqishything.ApplyDamage(m_DamageAmount, this);
    }
}
