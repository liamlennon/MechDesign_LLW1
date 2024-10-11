using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerController;

    int dummyServicRef;

    private int m_CurrentScore = 0;

    public static Action<int> OnGainPoints;

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
        Debug.Log($"The current score is: {m_CurrentScore}");
    }

    //IEnumerable Start()
    //{
    //    yield return new WaitForSeconds(2f);
    //    m_PlayerController = Instantiate(m_PlayerController);
    //}
}
