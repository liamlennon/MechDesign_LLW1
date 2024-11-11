using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button_ClickOnlyDemo : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent m_event;

    public void OnPointerClick(PointerEventData eventData)
    {
        m_event?.Invoke();
    }
}
