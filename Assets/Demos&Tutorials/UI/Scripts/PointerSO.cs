using UnityEngine;

[CreateAssetMenu(fileName = "PointerSO", menuName = "DataObjects/PointerSO", order = 0)]
[System.Serializable]
public class PointerSO : ScriptableObject
{
	public PointerType Type;
	public Texture2D Texture;
	public Vector2 Hotspot;
}
