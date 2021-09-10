using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Public Variables
    [HideInInspector] public static Action ChangeCameraAngle;
    [HideInInspector] public static Action MoveHelicopter;
    [HideInInspector] public static bool isGameStart = false;

    [HideInInspector] public bool gameOver = false;

    // Private Variables
    Animator animator;

    UIController uic;

    string startScreen, gameOverScreen, youWinScreen;

    float firstFingerX, lastFingerX;
    float minYPos = -15;

    #endregion

    private void Start()
    {
        startScreen = Strings.ScreenNames.StartScreen.ToString();
        gameOverScreen = Strings.ScreenNames.GameOverScreen.ToString();
        youWinScreen = Strings.ScreenNames.YouWinScreen.ToString();

        uic = FindObjectOfType<UIController>().GetComponent<UIController>();

        isGameStart = false;

        uic.openScreen(startScreen);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isGameStart) return;
        else
            if (!animator.GetBool("isRunning")) animator.SetBool("isRunning", true);

        MoveHorizontal();
        checkIsFloating();
        checkIsFall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "FinishLine")
        {
            Camera.main.transform.Find("Confetti_01").gameObject.SetActive(true);
            Camera.main.transform.Find("Confetti_02").gameObject.SetActive(true);
            uic.openScreen(youWinScreen);

            ChangeCameraAngle();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "HelicopterStop")
        {
            MoveScript ms = GetComponent<MoveScript>();
            PlayerController pc = GetComponent<PlayerController>();

            if (ms != null)
            {
                ms.enabled = false;
                animator.SetBool("isRunning", false);

                MoveHelicopter();

                StartCoroutine(DestroyPlayer(1.5f));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameOver) return;

        if (collision.gameObject.CompareTag("GroundCube"))
        {
            if (collision.gameObject.GetComponent<CubeScript>() != null)
                collision.gameObject.GetComponent<CubeScript>().fall();
        }
    }

    public void die()
    {
        GetComponent<MoveScript>().enabled = false;
        GetComponent<PlayerController>().enabled = false;

        animator.SetTrigger("die");

        gameOver = true;

        uic.openScreen(gameOverScreen);
    }

    float getMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.y + 5;

        return Camera.main.ScreenToWorldPoint(mousePos).x;
    }

    IEnumerator DestroyPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void MoveHorizontal()
    {
        if (Input.GetMouseButtonDown(0)) firstFingerX = getMousePos();
        else if (Input.GetMouseButton(0))
        {
            lastFingerX = getMousePos();

            float dif = lastFingerX - firstFingerX;

            transform.rotation = Quaternion.Euler(0, dif * 50, 0);
            transform.position += new Vector3(dif, 0, 0) * 0.8f;

            firstFingerX = lastFingerX;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z + 5));
        }
    }

    void checkIsFloating()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1))
        {
            if (hitInfo.collider == null)
            {
                if (!animator.GetBool("isFloating")) animator.SetBool("isFloating", true);
            }
        }
        else
        {
            if (animator.GetBool("isFloating")) animator.SetBool("isFloating", false);
        }
    }

    void checkIsFall()
    {
        if (transform.position.y <= minYPos)
            die();
    }
}
