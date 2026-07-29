using System;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController m_PlayerController;
    private PlayerController m_PlayerRef;
    [SerializeField] private TextMeshProUGUI m_ScoreUI;
    [SerializeField] private TextMeshProUGUI m_DamageUI;
    
   
    [SerializeField] private GameObject m_EndGame;
    [SerializeField] private DesignPatterns_ObjectPooler m_ObjectPooler;
    [SerializeField] HealthComponent m_HealthComponent;


    int dummyServicRef;

    private int m_CurrentScore = 0;
    private int m_CurrentHealth = 100;

    public static Action<int> OnGainPoints;
    public static Action<int> OnTalkDamage;

    private void Start()
    {
        m_PlayerRef = Instantiate(m_PlayerController);
        m_PlayerRef.Init(m_ObjectPooler);
        m_PlayerRef.InitHealth(m_HealthComponent);
    }

    private void OnEnable()
    {
        OnGainPoints += Handle_RecievePoints;

    }

    private void OnDisable()
    {
        OnGainPoints -= Handle_RecievePoints;
    }

    private void Handle_Damage(int damage)
    {

        m_CurrentHealth -= damage;

       

        m_DamageUI.text = $"{m_CurrentScore}";       
    }




    private void Handle_RecievePoints(int points) 
    {
        m_CurrentScore += points;

       m_ScoreUI.text =$"The current score is: {m_CurrentScore}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
