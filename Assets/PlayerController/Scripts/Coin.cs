using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] private int m_ScorePoint;
    //[SerializeField] private TextMeshProUGUI m_CollectableText;
    //private AudioSource m_Collected;
//    [SerializeField] GameObject m_CoinParticle;

   /* private void Start()
    {
        m_Collected = GetComponent<AudioSource>();
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.OnGainPoints?.Invoke(m_ScorePoint);
       // m_CollectableText.text = m_ScorePoint.ToString();
        //Instantiate(m_CoinParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
       // OnDestroy();
    }
    private void OnDestroy()
    {
        //m_Collected.Play();
    }
}
