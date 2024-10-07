using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerController;

    int dummyServicRef;

    IEnumerable Start()
    {
        yield return new WaitForSeconds(2f);
        m_PlayerController = Instantiate(m_PlayerController);


    }
}
