using UnityEngine;

public class CapsuleColliderFM : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D m_CapsuleCollider;
    [SerializeField] Rigidbody2D m_Rigidbody;
    private Vector2 m_DefaultSize;
    [SerializeField] private Vector2 m_ShrinkSize;

    private void Awake()
    {

    }
    private void Start()
    {
        m_DefaultSize = m_CapsuleCollider.size;
    }
    private void FixedUpdate()
    {
        if (m_Rigidbody.linearVelocityY > 0)
        {
            m_CapsuleCollider.size = new Vector2(0.3f, 2);
        }
        else
        {
            m_CapsuleCollider.size = new Vector2(1, 2);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //m_CapsuleCollider.size = new Vector2(1, 2);
        //m_CapsuleCollider.size = new Vector2(1, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("IN wall");
        m_CapsuleCollider.size = new Vector2(1, 2);
        //m_CapsuleCollider.size = new Vector2(defaultSize.x, defaultSize.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
    }

}
