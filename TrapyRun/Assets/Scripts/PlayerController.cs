using Assets.Scripts;
using States.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    
    #region Variables
    [SerializeField] private LayerMask groundLayerMask;
    
    [HideInInspector] public PlayerBaseState currentState;
    // Public Variables

    // Private Variables
    private Animator animator;
    private MoveScript _moveScript;

    private float firstFingerX, lastFingerX;
    private float minYPos = -15;

    #endregion Variables

    private void Start()
    {
        Stop();

        SignUpEvents();
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        animator = GetComponent<Animator>();
        _moveScript = GetComponent<MoveScript>();
    }

    private void SignUpEvents()
    {
        Actions.TutorialFinishEvent += Run;
    }

    public void Stop()
    {
        currentState = new PlayerIdleState(this);
        _moveScript.enabled = false;
    }
    
    private void Run()
    {
        currentState = new PlayerRunState(this);
        _moveScript.enabled = true;
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
            MoveScript ms = GetComponent<MoveScript>();

            if (ms != null)
            {
                ms.enabled = false;
                animator.SetBool("isRunning", false);

                Actions.HeliEvent();

                Destroy(gameObject, 1.5f);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (GameManager.currentState == GameManager.GameStates.GameOver)
        {
            return;
        }

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
        _moveScript.enabled = false;
        enabled = false;

        Actions.LoseEvent?.Invoke();
    }

    private float GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.y + 5;

        return Camera.main.ScreenToWorldPoint(mousePos).x;
    }

    public void MoveHorizontal()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstFingerX = GetMousePos();
        }
        else if (Input.GetMouseButton(0))
        {
            lastFingerX = GetMousePos();

            float dif = lastFingerX - firstFingerX;

            transform.rotation = Quaternion.Euler(0, dif * 50, 0);
            transform.position += new Vector3(dif, 0, 0) * 0.9f;

            firstFingerX = lastFingerX;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z + 5));
        }
    }

    public bool CheckIsFloating()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1, groundLayerMask);
    }

    public bool CheckIfFell()
    {
        return (transform.position.y <= minYPos);
    }

    public void PlaIdleAnim()
    {
        //TODO: Play Idle Animation
    }
    
    public void PlaRunAnim()
    {
        //TODO: Play Run Animation
    }
    
    public void PlaFallAnim()
    {
        //TODO: Play Fall Animation
    }

    public void PlayDeathAnim()
    {
        //TODO: Play Death Animation
    }
}