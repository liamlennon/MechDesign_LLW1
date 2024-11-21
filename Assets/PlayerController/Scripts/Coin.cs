using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] private int m_ScorePoint;
    //[SerializeField] private TextMeshProUGUI m_CollectableText;
    [SerializeField] private AudioSource m_Collected;
    [SerializeField] private SpriteRenderer m_Sprite;
    [SerializeField] private ParticleSystem m_CoinParticles;
    private ParticleSystem m_CoinPatriclesInstance;
    private float m_ParticleLifeTime;
    //[SerializeField] GameObject m_CoinParticle;

    /* private void Start()
     {
         m_Collected = GetComponent<AudioSource>();
     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.OnGainPoints?.Invoke(m_ScorePoint);
            // m_CollectableText.text = m_ScorePoint.ToString();
            //Instantiate(m_CoinParticle, transform.position, Quaternion.identity);
            SpawnCoinParticles();

            //turn of the collider and Sprite
            m_Collected.Play();

            m_Sprite.enabled = false;

            Destroy(this.gameObject, 2f);
           // OnDestroy();
        }
    }
    private void SpawnCoinParticles()
    {
       //instantiate (coin particles, spawn at the current transform.position, spawn in with no rotation)
        m_CoinPatriclesInstance = Instantiate(m_CoinParticles, transform.position, Quaternion.identity);
    }
}
