using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    PlayerController pc;
    Animator animator;
    NavMeshAgent navMesh;
    Vector3 playerPos;
    GameObject player;

    float minYPos = -5;
	float fallSpeed = 10;

    bool isFindPath = false;

    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!PlayerController.isGameStart) return;
        else
            if (!animator.GetBool("isRunning")) animator.SetBool("isRunning", true);

        if (navMesh != null && navMesh.enabled) // enemy has ai
        {
            if (player != null || !pc.gameOver)
            {
                playerPos = player.transform.position;
                navMesh.SetDestination(playerPos);
            }
            else
                letAIGo(true);

            if (pc.gameOver)
                letAIGo(true);
        }

        if (transform.position.y <= minYPos) Destroy(gameObject);

        checkIsFloating();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().die(dieWithFalling: false);
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
                    StartCoroutine(resetLookRotation());
                }
            }
        }
    }

    IEnumerator resetLookRotation()
    {
        yield return new WaitForSeconds(0.2f);

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void letAIGo(bool let)
    {
        if (let)
        {
            if (navMesh.enabled)
            {
                navMesh.enabled = false;
                GetComponent<MoveScript>().enabled = true;
                transform.LookAt(new Vector3(0, 0, transform.position.z + 10));
            }
        }
        else
        {
            if (!navMesh.enabled)
            {
                navMesh.enabled = true;
                GetComponent<MoveScript>().enabled = false;
                transform.LookAt(new Vector3(0, 0, transform.position.z + 10));
            }
        }
    }

    void checkIsFloating()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 2))
        {
            if (hitInfo.collider == null)
            {
                if (!animator.GetBool("isFloating")) animator.SetBool("isFloating", true);

                if (navMesh != null) letAIGo(true);

				transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
            }
        }
        else
        {
            if (animator.GetBool("isFloating")) animator.SetBool("isFloating", false);
            if (navMesh != null) letAIGo(false);
        }
    }
}
