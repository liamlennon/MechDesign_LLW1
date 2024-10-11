using System.Collections;
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
		m_Movement.StartJump();
	}
	private void Handle_JumpCancelled(InputAction.CallbackContext context)
	{
		m_Movement.StopJump();
	}

	private void Handle_CrouchPeformed(InputAction.CallbackContext context)
	{
		
	}

	private void Handle_CrouchCancelled(InputAction.CallbackContext context) {

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
