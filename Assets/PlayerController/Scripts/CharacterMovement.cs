using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
	private Rigidbody2D m_RB;

	[SerializeField] private StatefulRaycastSensor2D m_GroundSensor;
	[SerializeField] private float m_MoveSpeed;
	[SerializeField] private float m_JumpStrength;

	private float m_InMove;

	private void Awake()
	{
		m_RB = GetComponent<Rigidbody2D>();
		Debug.Assert(m_GroundSensor != null);
	}

	public void SetInMove(float newMove) => m_InMove = newMove;
	public void StartJump()
	{
		if (m_GroundSensor.HasDetectedHit())
		{
			m_RB.AddForce(Vector2.up * m_JumpStrength, ForceMode2D.Impulse);
		}
	}
	public void StopJump() { }

	private void FixedUpdate()
	{
		m_RB.linearVelocityX = m_MoveSpeed * m_InMove;
	}
}
