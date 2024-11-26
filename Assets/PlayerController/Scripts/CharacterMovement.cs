using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

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
	[SerializeField] BoxCollider2D m_BoxCollider2D;
    [SerializeField] private float m_BounceForce;
    [SerializeField] private StatefulRaycastSensor2D m_GroundSensor;
	[SerializeField] private float m_MoveSpeed;
	[SerializeField] private float m_JumpStrength;

	private PlayerController m_PlayerController;

	[SerializeField] private TrailRenderer tr;
	private bool canDash = true;
	private bool isDashing;
	[SerializeField] float dashingPower;
	private float dashingTime = 0.2f;
	private float dashingCooldown = 1f;

	private bool m_isJumping;
	private bool m_CanCoyote;
	//[SerializeField] private float m_JumpBufferTimer = 0.5f;
	//[SerializeField] private float m_JumpBufferCountdown;

	private bool m_IsMoving;
	private Coroutine m_CMove;
	private Coroutine m_CDash;
	private Coroutine m_CJumpbuff;
	private Coroutine m_CoyoteTimeCoroutine;

	private bool isFacingRight = true;

	private float m_InMove;
	[SerializeField] private float m_CoyoteTimer;
	[SerializeField] private float m_CoyoteThresHold;

	[SerializeField] private float m_MinSpeed;
	[SerializeField] private float m_CurrentSpeed;
	[SerializeField] private float m_MaxSpeed;

	[SerializeField] private float m_FallSpeed;
	[SerializeField] private float m_MaxFallSpeed;

	[SerializeField] private float m_GroundMoveSpeed;
	private float m_ApexPoint;
	private float m_ApexSpeed;
	private float m_JumpTimeCounter;

    [SerializeField] private float m_JumpbufferTimer = 0.5f;
	[SerializeField] private float m_JumpBufferThreshold;
	[SerializeField] private float m_ApexMoveSpeed;


    enum JumpStates
	{
		Rising,
		Apex,
		Falling
	}
	JumpStates jumpStates = JumpStates.Rising, Apex, Falling;


	private void Awake()
	{
		m_RB = GetComponent<Rigidbody2D>();
		m_BoxCollider2D = GetComponent<BoxCollider2D>();
		//Debug.Assert(m_GroundSensor != null);
		m_PlayerController = GetComponent<PlayerController>();
	}
	private void Update()
	{
		if (isDashing) { return; }
	}

	private void FixedUpdate()
	{		
		if (isDashing) { return; }
	}

	private void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	IEnumerator JumpApex()
	{
		while (!m_GroundSensor.HasDetectedHit())
		{
			switch (jumpStates)
			{
				case JumpStates.Rising:
					//control speed of rise
					m_RB.gravityScale = 1;
					Debug.Log("Rising");
					if (m_RB.linearVelocityY < 0.5)
					{
						jumpStates = JumpStates.Apex;
					}
					break;
				case JumpStates.Apex:
					// boost in speed when player hits jump apex, temporarily turn off gravity

					m_MaxSpeed = 4;					
					m_RB.gravityScale = 0.5f;
					if (m_RB.linearVelocityY < -2)
					{
						Debug.Log("Jump Apex");
						jumpStates = JumpStates.Falling;
					}
					break;
					case JumpStates.Falling:
					//control fall speed, camera zoom in
						m_RB.gravityScale = 2f;
						Camera.main.fieldOfView = 30;
					
					while(!m_GroundSensor)
					{
						m_MoveSpeed = m_FallSpeed;
					}	
					//Falling = (JumpStates)Mathf.Lerp(m_FallSpeed, m_MaxFallSpeed, m_ApexPoint);
					Debug.Log("Jump Falling");
					break;
			}
			yield return new WaitForFixedUpdate();

		}

	}

	public void SetInMove(float newMove)
	{
		m_InMove = newMove;
		Flip();
		if (m_MoveSpeed == m_MaxSpeed)
		{
			m_RB.gameObject.SetActive(false);
		}
		if (m_InMove == 0)
		{
			//we are stopped;
			if (!m_IsMoving) { return; }
			Flip();
			m_IsMoving = false;
		}
		else
		{
			// we are moving
			if (m_IsMoving) { return; }
			m_IsMoving = true;
			m_CMove = StartCoroutine(C_MoveUpdate());
		}
	}
	private IEnumerator C_MoveUpdate()
	{
		while (m_IsMoving)
		{
			if (!isDashing)
			{
				m_RB.linearVelocityX = m_MoveSpeed * m_InMove;
			}
			if (m_CurrentSpeed == m_MaxSpeed)
			{

				m_RB.simulated = false;
			}
			yield return new WaitForFixedUpdate();
		}
		m_RB.linearVelocityX = m_MoveSpeed * m_InMove;
	}
	public void StartCrouch()
	{
		//Debug.Log("Crouch pressed---------------------");
	}
	public void StopCrouch()
	{
		// Debug.Log("Crouch not pressed---------------------");
	}
	public void StartDash()
	{
		 Debug.Log("IsDashing");
		if (isDashing == false)
		{
			m_CDash = StartCoroutine(Dash());
		}
	}
	private IEnumerator Dash()
	{
		isDashing = true;
		float originalGravity = m_RB.gravityScale;
		m_RB.gravityScale = 0f;
		//m_RB.linearVelocity = new Vector2(transform.localScale.y * dashingPower, 0f);
		m_RB.AddForce(new Vector2(m_InMove * dashingPower, 0), ForceMode2D.Impulse);
		tr.emitting = true;
		yield return new WaitForSeconds(1);
		tr.emitting = false;
		m_RB.gravityScale = originalGravity;
		isDashing = false;
		yield return new WaitForSeconds(1);

	}
	public void StartJump()
	{
        if (m_GroundSensor.HasDetectedHit() || m_CoyoteTimer > 0)
		{
			m_RB.AddForce(Vector2.up * m_JumpStrength, ForceMode2D.Impulse);
			m_JumpTimeCounter -= Time.deltaTime;
			StartCoroutine(JumpApex());
		}
		else
		{
			Debug.Log("jump buffer hit");
			m_JumpbufferTimer = m_JumpBufferThreshold; 
			m_CJumpbuff = StartCoroutine(C_JumpBuffer());
		}
	}
	IEnumerator C_JumpBuffer()
	{
		while (m_JumpbufferTimer >= 0)
		{
			m_JumpbufferTimer -= Time.deltaTime;
			yield return null;
        }
    }
	public void StopJump()
	{
		jumpStates = Falling;
	}
	private IEnumerator CoyoteCouritne()
	{	
		//m_CanCoyote = true;
		Debug.Log("CoyoteCouritne");
		while (m_CoyoteTimer >= 0)
		{
			m_CoyoteTimer -= Time.deltaTime;
			yield return null;
		}   
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Spike"))
        {
			Vector2 bounceDirct = -collision.contacts[0].normal;
            m_RB.linearVelocity = Vector2.zero;
			m_RB.AddForce(bounceDirct * m_BounceForce, ForceMode2D.Impulse);
        }
		if (m_JumpbufferTimer > 0)
		{
            m_RB.AddForce(Vector2.up * m_JumpStrength, ForceMode2D.Impulse);
			m_JumpbufferTimer = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (m_RB.linearVelocityY <= 0)
        {
            m_CoyoteTimer = m_CoyoteThresHold;
            m_CoyoteTimeCoroutine = StartCoroutine(CoyoteCouritne());
            Debug.Log("CoyoteTimeWorking");
        }
    }

}