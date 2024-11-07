using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController m_PlayerController;
    private PlayerController m_PlayerRef;

    int dummyServicRef;

    private int m_CurrentScore = 1;

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
        points += 1;
        Debug.Log($"The current score is: {m_CurrentScore}");
    }

    //IEnumerable Start()
    //{
    //    yield return new WaitForSeconds(2f);
    //    m_PlayerController = Instantiate(m_PlayerController);
    //}
}
