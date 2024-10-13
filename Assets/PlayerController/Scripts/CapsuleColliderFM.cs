using UnityEngine;

public class CapsuleColliderFM : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D m_CapsuleCollider;

    private void Awake()
    {
        m_CapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_CapsuleCollider.size = new Vector2(0.5f, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_CapsuleCollider.size = new Vector2(1, 1);
    }

}
