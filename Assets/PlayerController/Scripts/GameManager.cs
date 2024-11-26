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
    [SerializeField] private GameObject m_EndGame;
    [SerializeField] private DesignPatterns_ObjectPooler m_ObjectPooler;
`
    int dummyServicRef;

    private int m_CurrentScore = 0;

    public static Action<int> OnGainPoints;

    private void Start()
    {
        m_PlayerRef = Instantiate(m_PlayerController);
        m_PlayerRef.Init(m_ObjectPooler);
    }

    private void OnEnable()
    {
        OnGainPoints += Handle_RecievePoints;
    }

    private void OnDisable()
    {
        OnGainPoints -= Handle_RecievePoints;
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

    //IEnumerable Start()
    //{
    //    yield return new WaitForSeconds(2f);
    //    m_PlayerController = Instantiate(m_PlayerController);
    //}
}
