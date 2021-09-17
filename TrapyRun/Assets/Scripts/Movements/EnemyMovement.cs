using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private float autoSpeedControlDistance = 11;

    [SerializeField] private float moveSpeed = 10;

    private GameObject player;
    private Transform playerTrans;
    private bool isFindPath = false;
    private NavMeshAgent navMesh;
    private float fallSpeed = 10;
    #endregion Variables

    private void Start()
    {
        InitializeVariables();
        DeactivateNavMesh();
    }

    private void Update()
    {
        MoveForward();
    }
    
    private IEnumerator ResetLookRotation()
    {
        yield return new WaitForSeconds(0.2f);

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    public void NavMeshMovement()
    {
        if (IsNavMeshActivated()) // enemy has ai
        {
            if ((object)player != null)
            {
                navMesh.SetDestination(playerTrans.position);
            }
            else
            {
                DeactivateNavMesh();
            }
        }
    }

    private void OnDisable()
    {
        DeactivateNavMesh();
    }

    public void Fall()
    {
        if (navMesh != null)
        {
            DeactivateNavMesh();
        }

        ForceFall();
    }
    
    public void ForceFall()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * fallSpeed));
    }
    
    public bool IsPlayerReachable()
    {
        return (object) player != null;
    }
    
    public void Respawn()
    {
        if (!IsPlayerReachable())
        {
            Destroy(gameObject);
            return;
        }
        
        float randomXValue = Random.Range(-5, 5);
        float randomZValue = Random.Range(30, 40);

        Vector3 newPos = new Vector3(playerTrans.position.x + randomXValue, playerTrans.position.y, playerTrans.position.z - randomZValue);

        transform.position = newPos;
    }
    
    public bool IsNavMeshActivated()
    {
        return navMesh != null && navMesh.enabled;
    }
    
    public void ActivateNavMesh()
    {
        if (navMesh == null || navMesh.enabled == true) { return; }
        
        navMesh.enabled = true;
        transform.LookAt(new Vector3(0, 0, transform.position.z + 10));
    }

    public void DeactivateNavMesh()
    {
        if (navMesh == null || navMesh.enabled == false) { return; }
        
        navMesh.enabled = false;
        transform.LookAt(new Vector3(0, 0, transform.position.z + 10));
    }
    
    public void FindPath(Collision collision)
    {
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
        
        navMesh = GetComponent<NavMeshAgent>();
    }

    public void MoveForward()
    {
        if ((object)player != null)
        {
            Vector3 playerPos = player.transform.position;

            float dist = playerPos.z - transform.position.z;

            if (dist >= autoSpeedControlDistance)
            {
                moveSpeed = dist;
            }
        }
        
        transform.position += Vector3.forward * (Time.deltaTime * moveSpeed);
    }
}
