using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    private Animator animator;

    private NavMeshAgent navMesh;
    private Transform playerTrans;
    private GameObject player;
    private MoveScript moveScript;

    private float minYPos = -5;
    private float fallSpeed = 10;

    private bool isFindPath = false;

    #endregion Variables

    private void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        if (!PlayerController.gameStart) return;

        if (!animator.GetBool("isRunning"))
        {
            animator.SetBool("isRunning", true);
        }

        if (navMesh != null && navMesh.enabled) // enemy has ai
        {
            if (player != null || !PlayerController.gameOver)
            {
                navMesh.SetDestination(playerTrans.position);
            }
            else
            {
                LetAIGo(true);
            }

            if (PlayerController.gameOver)
            {
                LetAIGo(true);
            }
        }

        if (transform.position.y <= minYPos)
        {
            float randomXValue = Random.Range(-5, 5);
            float randomZValue = Random.Range(30, 40);

            Vector3 newPos = new Vector3(playerTrans.position.x + randomXValue, playerTrans.position.y, playerTrans.position.z - randomZValue);

            transform.position = newPos;
        }

        CheckIsFloating();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Die(dieWithFalling: false);
        }

        if (!isFindPath)
        {
            if (navMesh == null)
            {
                if (collision.gameObject.CompareTag("SideCube"))
                {
                    transform.LookAt(collision.gameObject.transform.parent.Find("EnemyCoord").transform);
                }
                else if (collision.gameObject.CompareTag("GroundCube"))
                {
                    isFindPath = true;
                    StartCoroutine(ResetLookRotation());
                }
            }
        }
    }

    private void InitializeVariables()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
        animator = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        moveScript = GetComponent<MoveScript>();
    }

    private IEnumerator ResetLookRotation()
    {
        yield return new WaitForSeconds(0.2f);

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void LetAIGo(bool let)
    {
        if (let)
        {
            ChangeNavMeshState(false);
        }
        else
        {
            ChangeNavMeshState(true);
        }
    }

    private void ChangeNavMeshState(bool newNavMeshState)
    {
        if (navMesh.enabled == !newNavMeshState)
        {
            navMesh.enabled = newNavMeshState;
            moveScript.enabled = !newNavMeshState;
            transform.LookAt(new Vector3(0, 0, transform.position.z + 10));
        }
    }

    private void CheckIsFloating()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 2))
        {
            if (hitInfo.collider == null)
            {
                if (!animator.GetBool("isFloating"))
                {
                    animator.SetBool("isFloating", true);
                }

                if (navMesh != null)
                {
                    LetAIGo(true);
                }

                transform.Translate(Vector3.down * (Time.deltaTime * fallSpeed));
            }
        }
        else
        {
            if (animator.GetBool("isFloating"))
            {
                animator.SetBool("isFloating", false);
            }

            if (navMesh != null) LetAIGo(false);
        }
    }
}