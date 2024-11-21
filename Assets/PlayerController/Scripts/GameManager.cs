using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController m_PlayerController;
    private PlayerController m_PlayerRef;
    [SerializeField] private TextMeshProUGUI m_ScoreUI;
    int dummyServicRef;

    private int m_CurrentScore = 0;

    public static Action<int> OnGainPoints;

    private void Start()
    {
        m_PlayerRef = Instantiate(m_PlayerController);

        m_PlayerRef.Init();
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

    //IEnumerable Start()
    //{
    //    yield return new WaitForSeconds(2f);
    //    m_PlayerController = Instantiate(m_PlayerController);
    //}
}
