using UnityEngine;

public class TextPopup : MonoBehaviour
{
    public GameObject popUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            popUI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            popUI.SetActive(false);
        }
    }
}
