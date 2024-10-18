using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int m_ScorePoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.OnGainPoints?.Invoke(m_ScorePoint);
        Destroy(gameObject);
    }
}
