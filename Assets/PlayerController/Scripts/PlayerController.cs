using System.Collections;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
	private PlayerControls m_ActionMap;
	private CharacterMovement m_Movement;

	private HealthComponent m_HealthComponent;

	private bool m_InMoveActive = false;
	private Coroutine m_cMovement;

	[SerializeField] private DesignPatterns_ObjectPooler m_ObjectPooler;

	[SerializeField] private float m_JumpbufferTimer = 0.5f;
	[SerializeField] private float m_JumpBufferCountdown;
	private bool m_IsJumping;
	private Coroutine m_cJumpBuffer;

	/*public void Init(int dummyServicRef)
	{
		m_ActionMap = new PlayerControls();	
		m_Movement = GetComponent<CharacterMovement>();
		//m_Movement.Init();
	}
   */ private void Awake()
	{
		m_ActionMap = new PlayerControls();
		m_Movement = GetComponent<CharacterMovement>();
		m_HealthComponent = GetComponent<HealthComponent>();	
	} 

	private void OnEnable()
	{
		m_ActionMap.Enable();

		m_ActionMap.Default.MoveHoriz.performed += Handle_MovePerformed;
		m_ActionMap.Default.MoveHoriz.canceled += Handle_MoveCancelled;

		m_ActionMap.Default.Jump.performed += Handle_JumpPerformed;
		m_ActionMap.Default.Jump.canceled += Handle_JumpCancelled;

		m_ActionMap.Default.Crouch.performed += Handle_CrouchPeformed;
		m_ActionMap.Default.Crouch.canceled += Handle_CrouchCancelled;

		m_ActionMap.Default.Shoot.performed += Handle_ShootPerformed;

		m_ActionMap.Default.Dash.performed += Handle_DashPreformed;

		m_HealthComponent.OnDamage += Handle_HealhDamage;
		m_HealthComponent.OnDeath += Handle_OnDead;
	}

	private void OnDisable()
	{
		m_ActionMap.Disable();

		m_ActionMap.Default.MoveHoriz.performed -= Handle_MovePerformed;
		m_ActionMap.Default.MoveHoriz.canceled -= Handle_MoveCancelled;
		m_ActionMap.Default.Jump.performed -= Handle_JumpPerformed;
		m_ActionMap.Default.Jump.canceled -= Handle_JumpCancelled;

		m_ActionMap.Default.Crouch.performed -= Handle_CrouchPeformed;
		m_ActionMap.Default.Crouch.canceled -= Handle_CrouchCancelled;

		m_ActionMap.Default.Shoot.performed -= Handle_ShootPerformed;

		m_ActionMap.Default.Dash.performed -= Handle_DashPreformed;

		m_HealthComponent.OnDamage -= Handle_HealhDamage;
		m_HealthComponent.OnDeath -= Handle_OnDead;	
    }


	private IEnumerator C_MovedUpdate()
	{
		while(m_InMoveActive)
		{
			yield return new WaitForSeconds(5f);
		}
	}

	private void Handle_MovePerformed(InputAction.CallbackContext context)
	{
		m_Movement.SetInMove(context.ReadValue<float>());
	}
	private void Handle_MoveCancelled(InputAction.CallbackContext context)
	{
		m_Movement.SetInMove(0f);
	}

	private void Handle_JumpPerformed(InputAction.CallbackContext context)
	{
		m_JumpBufferCountdown = m_JumpbufferTimer;

		if(m_JumpBufferCountdown > 0) 
		{
			C_JumpBuffer();
		}
	
		m_Movement.StartJump();
		//maybe use while loop. While jump buffering is greater than 0 execute jump function
	}

	private IEnumerator C_JumpBuffer()
	{
		m_IsJumping = true;
		yield return new WaitForSeconds(m_JumpBufferCountdown);
		m_Movement.StartJump();
		m_IsJumping = false;
	}

	private void Handle_JumpCancelled(InputAction.CallbackContext context)
	{
		m_Movement.StopJump();
	}

	private void Handle_CrouchPeformed(InputAction.CallbackContext context)
	{
		m_Movement.StartCrouch();
    }

	private void Handle_CrouchCancelled(InputAction.CallbackContext context) 
	{
		m_Movement.StopCrouch();
    }

	private void Handle_ShootPerformed(InputAction.CallbackContext context)
	{
		GameObject bullet = m_ObjectPooler.GetPooledObject("bullet");
		if (bullet == null)  { return; }

		bullet.SetActive(true);

		bullet.transform.position = new Vector3(transform.position.z, transform.position.y + 2, 0);
	}

	private void Handle_DashPreformed(InputAction.CallbackContext context)
	{

	}

	private void Handle_HealhDamage(float currentHealth, float maxHealth, float change) 
	{
		Debug.Log($"I was damaged, my current health is {currentHealth} out of {maxHealth}");
	}

	private void Handle_OnDead(MonoBehaviour causer) 
	{
		Debug.Log($"I am deaded, the thing that killed me is {causer.gameObject.name}");
	}
}
