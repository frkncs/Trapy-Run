using Assets.Scripts;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Public Variables
    [HideInInspector] public static bool gameStart = false;
    [HideInInspector] public static bool gameOver = false;

    // Private Variables
    private Animator animator;

    private float firstFingerX, lastFingerX;
    private float minYPos = -15;

    #endregion Variables

    private void Start()
    {
        Actions.OpenScreen(Strings.startScreen);
        
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        gameStart = false;
        gameOver = false;

        animator = GetComponent<Animator>();

        Collider heliCheckerCol = GameObject.Find("Heli_Entry_Checker").GetComponent<Collider>();
        Physics.IgnoreCollision(heliCheckerCol, GetComponent<Collider>());
    }

    private void Update()
    {
        if (!gameStart) return;

        if (!animator.GetBool("isRunning"))
        {
            animator.SetBool("isRunning", true);
        }

        MoveHorizontal();
        CheckIsFloating();
        CheckIsFall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "FinishLine")
        {
            // YouWin
            Actions.StartConfettiEffect();
            Actions.OpenScreen(Strings.youWinScreen);
            Actions.ChangeCameraAngle();
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

                Actions.MoveHelicopter();

                Destroy(gameObject, 1.5f);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameOver) return;

        if (collision.gameObject.CompareTag("GroundCube"))
        {
            CubeScript cubeScript = collision.gameObject.GetComponent<CubeScript>();

            if (cubeScript != null)
            {
                cubeScript.Fall();
            }
        }
    }

    public void Die(bool dieWithFalling)
    {
        GetComponent<MoveScript>().enabled = false;
        GetComponent<PlayerController>().enabled = false;

        if (!dieWithFalling)
        {
            animator.SetTrigger("die");
        }
        else
        {
            animator.SetBool("isFloating", true);
        }

        // GameOver

        gameOver = true;

        Actions.OpenScreen(Strings.gameOverScreen);
    }

    private float GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.y + 5;

        return Camera.main.ScreenToWorldPoint(mousePos).x;
    }

    private void MoveHorizontal()
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

    private void CheckIsFloating()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1))
        {
            if (hitInfo.collider == null)
            {
                if (!animator.GetBool("isFloating"))
                {
                    animator.SetBool("isFloating", true);
                }
            }
        }
        else
        {
            if (animator.GetBool("isFloating"))
            {
                animator.SetBool("isFloating", false);
            }
        }
    }

    private void CheckIsFall()
    {
        if (transform.position.y <= minYPos)
        {
            Die(dieWithFalling: true);
        }
    }
}