﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] bool isAI = false;

    PlayerController pc;
    Animator animator;
    NavMeshAgent navMesh;
    Vector3 playerPos;
    GameObject player;

    float minYPos = -15;

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

        if (isAI)
        {
            if (player != null)
            {
                if (navMesh != null && navMesh.enabled)
                {
                    playerPos = player.transform.position;
                    navMesh.SetDestination(playerPos);
                }
            }
            else
                letAIGo();

            if (pc.gameOver)
                letAIGo();
        }

        if (transform.position.y <= minYPos) Destroy(gameObject);

        checkIsFloating();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().die();
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

    void letAIGo()
    {
        if (navMesh.enabled)
        {
            GetComponent<MoveScript>().enabled = true;
            navMesh.enabled = false;
            transform.LookAt(new Vector3(0, 0, transform.position.z + 10));
        }
    }

    void checkIsFloating()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 2))
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
}
