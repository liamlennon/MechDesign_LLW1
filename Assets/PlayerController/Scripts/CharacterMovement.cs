using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
	/*
	Jump Buffering
	Coyote Time
	Anti-Gravity Apex
	Speed Apex
	Sticky Feet on Land
	Bumped Head Correction
	Corner Clip on Jumps
	Hold Crouch to Stay on Ledge
	Relaxed Semi-Solids
	Variable Jump Strength
	 */
	
	private Rigidbody2D m_RB;
	[SerializeField] CapsuleCollider2D m_CapsuleCollider;

	[SerializeField] private StatefulRaycastSensor2D m_GroundSensor;
	[SerializeField] private float m_MoveSpeed;
	[SerializeField] private float m_JumpStrength;

	private bool m_isJumping;
	//[SerializeField] private float m_JumpBufferTimer = 0.5f;
	//[SerializeField] private float m_JumpBufferCountdown;

	private bool m_IsMoving;
	private Coroutine m_CMoveUpdate;

	private float m_InMove;
	[SerializeField] private float m_CoyoteTimer;
	[SerializeField] private float m_CoyoteThresHold;

	[SerializeField] private float m_MinSpeed;
	[SerializeField] private float m_CurrentSpeed;
	[SerializeField] private float m_MaxSpeed;

	private bool m_IsJumpBuffering;

	private void Awake()
	{
		m_RB = GetComponent<Rigidbody2D>();
        m_CapsuleCollider = GetComponent<CapsuleCollider2D>();
		Debug.Assert(m_GroundSensor != null);
	}
	public void SetInMove(float newMove)
	{
		m_InMove = newMove;
		if(m_MoveSpeed == m_MaxSpeed)
		{
			m_RB.gameObject.SetActive(false);
		}
		if (m_InMove == 0)
		{
			//we are stopped;
			if (!m_IsMoving) { return; }

			m_IsMoving = false;
		}
		else
		{
			// we are moving
			if (m_IsMoving) { return; }

			m_IsMoving = true;
			m_CMoveUpdate = StartCoroutine(C_MoveUpdate());
		}
	}
	private IEnumerator C_MoveUpdate()
	{
		while(m_IsMoving)
		{
            //use lerp for min and max speed
            //temporarily disable rigibody when max speed is met
            //lerp may be no good because it's for two vectors and float not 3 floats
            //Maybe use coroutine
            m_RB.linearVelocityX = m_MoveSpeed * m_InMove;
			if(m_CurrentSpeed ==  m_MaxSpeed)
			{
				//turn off game objects rigibody
				//Does rigibody need to be turned off while at max speed or only a set amount of time
				m_RB.simulated = false;
			}
            yield return new WaitForFixedUpdate();
		}
        m_RB.linearVelocityX = m_MoveSpeed * m_InMove;
    }
	public void StartCrouch()
	{
		Debug.Log("Crouch pressed---------------------");
	}
	public void StopCrouch() 
	{
        Debug.Log("Crouch pressed---------------------");
    }
	public void StartJump()
	{
		//m_JumpBufferCountdown = m_JumpBufferTimer; 
		//determine how far off ground player is to allow early or late input then jump
		// Calculate how high rigibody is off the ground
		if (m_GroundSensor.HasDetectedHit() || m_CoyoteTimer > 0 /* || m_JumpBufferCountdown > 0*/)
		{
				//m_JumpBufferCountdown -= Time.fixedDeltaTime;
				m_RB.AddForce(Vector2.up * m_JumpStrength, ForceMode2D.Impulse);	
			/*m_isJumping = true;
			if(m_JumpBufferCountdown ==   m_JumpBufferTimer)
			{
				
			}*/
		}
	}
	public void StopJump() 
	{    
		/*if(!m_GroundSensor.HasDetectedHit() || m_CoyoteTimer < 0)
		{
				m_isJumping = false;
			   m_JumpBufferCountdown = 0;
		}
		*/
	}
	private void FixedUpdate()
	{
		m_RB.linearVelocityX = m_MoveSpeed * m_InMove;

		m_CoyoteTimer -= Time.fixedDeltaTime;
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(m_RB.linearVelocityY <= 0)
		{
			m_CoyoteTimer = m_CoyoteThresHold;
		}

		//const float ySize = 100.0f;
		
        //m_CapsuleCollider.size = new Vector2(0.5f, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		//m_CapsuleCollider.size = new Vector2(1, 1);
    }
}

