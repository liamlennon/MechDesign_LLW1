using System.Collections;
using System.Data.SqlTypes;
using System.IO.Pipes;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
	private PlayerControls m_ActionMap;
	private CharacterMovement m_Movement;

	[SerializeField] private Transform m_FirePoint;
	[SerializeField] private Bullet m_BulletPrefab;


	private HealthComponent m_HealthComponent;
	private DesignPatterns_ObjectPooler m_ObjectPooler;

	private bool m_InMoveActive = false;
	private Coroutine m_cMovement;

	//[SerializeField] private float m_JumpbufferTimer = 0.5f;
	//[SerializeField] private float m_JumpBufferCountdown;
	//private bool m_IsJumping;
	private Coroutine m_cJumpBuffer;

	

	
    private void Awake()
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

		m_ActionMap.Default.Dash.performed += Handlle_DashPerformed;

	    m_ActionMap.Default.Shoot.performed += Handle_ShootPerformed;

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


		m_ActionMap.Default.Dash.performed -= Handlle_DashPerformed;

		m_ActionMap.Default.Shoot.performed -= Handle_ShootPerformed;

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

	private void Handlle_DashPerformed(InputAction.CallbackContext context)
	{
		m_Movement.StartDash();	
	}

	private void Handle_ShootPerformed(InputAction.CallbackContext callbackContext)
	{
		//GameObject bullet = m_ObjectPooler.GetPooledObject("Bullet");
		//if(bullet == null) { return;}
		//bullet.SetActive(true);
		

		Bullet bulletObject = Instantiate(m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);

		// Ensure the bullet moves in the correct direction
		Rigidbody2D rb = bulletObject.GetComponent<Rigidbody2D>();
		if (rb != null)
		{
			float bulletSpeed = 10f; // Adjust speed as needed
			rb.linearVelocity = m_FirePoint.right * bulletSpeed;
			//bullet.transform.position = new Vector3(m_FirePoint.transform.position.x, transform.position.y + 2,0);
		}
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
		m_Movement.StartCrouch();
        Debug.Log("Crouch pressed---------------------");
    }

	private void Handle_CrouchCancelled(InputAction.CallbackContext context) 
	{
		m_Movement.StopCrouch();
        Debug.Log("Crouch pressed---------------------");
    }

	private void Handle_HealhDamage(float currentHealth, float maxHealth, float change) 
	{
		Debug.Log($"I was damaged, my current health is {currentHealth} out of {maxHealth}");
	}

	private void Handle_OnDead(MonoBehaviour causer) 
	{
		Debug.Log($"I am deaded, the thing that killed me is {causer.gameObject.name}");
	}

	public void Init(DesignPatterns_ObjectPooler poolerRef)
	{
		Debug.Log("initilized Player Controller");
        m_ObjectPooler = poolerRef;
    }
}
