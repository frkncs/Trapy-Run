using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] bool isAI = false;

    Animator animator;
    Vector3 playerPos;

    float minYPos = -15;

    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAI)
        {
            if(GetComponent<NavMeshAgent>() != null)
            {
                playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                GetComponent<NavMeshAgent>().SetDestination(playerPos);
            }
        }

        if (transform.position.y <= minYPos) Destroy(gameObject);

        checkIsFloating();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().die();
            if (GetComponent<MoveScript>() != null)
                GetComponent<MoveScript>().enabled = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isAI)
        {
            if (collision.gameObject.CompareTag("SideCube"))
            {
                transform.LookAt(collision.gameObject.transform.parent.Find("EnemyCoord").transform);
            }
            else if (collision.gameObject.CompareTag("GroundCube"))
            {
                StartCoroutine(resetLookPoint());
            }
        }
    }

    IEnumerator resetLookPoint()
    {
        float second = Random.Range(0.8f, 1.1f);
        yield return new WaitForSeconds(second);

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void checkIsFloating()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, 5, LayerMask.GetMask("Ground")))
        {
            if (!animator.GetBool("isFloating")) animator.SetBool("isFloating", true);
        }
        else
        {
            if (animator.GetBool("isFloating")) animator.SetBool("isFloating", false);
        }
    }
}
