using States.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Public Variables
    [HideInInspector] public PlayerBaseState currentState;
    [HideInInspector] public PlayerMovement playerMovement;

    // Private Variables
    [SerializeField] private LayerMask groundLayerMask;

    private Animator animator;

    private const float minYPos = -15;

    #endregion Variables

    private void Start()
    {
        InitializeVariables();
        Stop();
    }

    private void OnEnable()
    {
        SignUpEvents();
    }

    private void OnDisable()
    {
        SignOutEvents();
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
            Destroy(gameObject, 1f);
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

    public void Stop()
    {
        currentState = new PlayerIdleState(this);
        playerMovement.enabled = false;
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
        Vector3 _rayPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

        return !Physics.Raycast(_rayPosition, Vector3.down, 5, groundLayerMask);
    }

    public bool CheckIfFell()
    {
        return transform.position.y <= minYPos;
    }

    public void PlayIdleAnim()
    {
        animator.Play("Idle");
    }

    public void PlayRunAnim()
    {
        animator.SetBool("running", true);
        animator.SetBool("floating", false);
    }

    public void PlayFallAnim()
    {
        animator.SetBool("running", false);
        animator.SetBool("floating", true);
    }

    public void PlayDeathAnim()
    {
        animator.SetTrigger("die");
    }

    private void Run()
    {
        currentState = new PlayerRunState(this);
        playerMovement.enabled = true;
    }

    private void InitializeVariables()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        EnemyController.canRun = false;
    }

    private void SignUpEvents()
    {
        Actions.TutorialFinishEvent += Run;
    }

    private void SignOutEvents()
    {
        Actions.TutorialFinishEvent -= Run;
    }
}