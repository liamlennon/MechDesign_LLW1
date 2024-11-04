using System.Runtime.CompilerServices;
using UnityEngine;

public class Spikeyboy : MonoBehaviour
{
   [SerializeField] private float m_DamageAmount;
    private CharacterMovement m_CharacterMovement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable SpikeTrap = collision.GetComponentInParent<IDamageable>();
        if (SpikeTrap == null ) { return; }
        SpikeTrap.ApplyDamage(m_DamageAmount, this);

        //(gameObject.tag = "Player")
        Rigidbody2D m_SB = m_CharacterMovement.GetComponent<Rigidbody2D>();
        m_SB.AddForce(Vector2.up * 2, ForceMode2D.Impulse); 
    }
}
