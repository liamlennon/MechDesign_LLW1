using UnityEngine;
using UnityEngine.EventSystems;

public class PointerChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public PointerType m_Type;

	public void OnPointerEnter(PointerEventData eventData)
	{
		Pointer.TryChangeIcon(m_Type);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Pointer.TryResetIcon(m_Type);
	}
}