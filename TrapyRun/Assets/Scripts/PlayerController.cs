using System;
using Assets.Scripts;
using States.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private LayerMask groundLayerMask;
    
    [HideInInspector] public PlayerBaseState currentState;
    [HideInInspector] public PlayerMovement playerMovement;

    private float minYPos = -15;
    private Animator animator;
    private Rigidbody _rigidbody;
    
    #endregion Variables

    private void Start()
    {
        SignUpEvents();
        InitializeVariables();
        Stop();
    }

    private void InitializeVariables()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void SignUpEvents()
    {
        Actions.TutorialFinishEvent += Run;
    }
    
    private void SignOutEvents()
    {
        Actions.TutorialFinishEvent -= Run;
    }

    public void Stop()
    {
        currentState = new PlayerIdleState(this);
        playerMovement.enabled = false;
    }
    
    private void Run()
    {
        currentState = new PlayerRunState(this);
        playerMovement.enabled = true;
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }

    private void Update()
    {
        currentState.Update(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "FinishLine")
        {
            Actions.WinEvent?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "HelicopterStop")
        {
            Stop();

            Actions.HeliEvent();
            Destroy(gameObject, 1.5f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("GroundCube"))
        {
            CubeScript cubeScript = collision.gameObject.GetComponent<CubeScript>();

            if (cubeScript != null)
            {
                cubeScript.Fall();
            }
        }
    }

    public void Catch()
    {
        PlayDeathAnim();
        Die();
    }

    public void Die()
    {
        playerMovement.enabled = false;
        enabled = false;

        Actions.LoseEvent?.Invoke();
    }

    public bool CheckIsFloating()
    {
        /*return transform.position.y <= -1.3f;*/
        return _rigidbody.velocity.y < -0.01f;
    }

    public bool CheckIfFell()
    {
        return (transform.position.y <= minYPos);
    }

    public void PlayIdleAnim()
    {
        animator.Play("Idle");
    }
    
    public void PlayRunAnim()
    {
        animator.Play("Running");
    }
    
    public void PlayFallAnim()
    {
        animator.Play("Falling");
    }

    public void PlayDeathAnim()
    {
        animator.Play("Death");
    }
    
    private void OnDisable()
    {
        SignOutEvents();
    }
}