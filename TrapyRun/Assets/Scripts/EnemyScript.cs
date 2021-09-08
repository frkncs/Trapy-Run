using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] bool canFollow = false;

    Vector3 playerPos;

    #endregion

    private void Update()
    {
        if (canFollow)
        {
            if(GetComponent<NavMeshAgent>() != null)
            {
                playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                GetComponent<NavMeshAgent>().SetDestination(playerPos);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().die();
            GetComponent<MoveScript>().enabled = false;
        }
    }
}
