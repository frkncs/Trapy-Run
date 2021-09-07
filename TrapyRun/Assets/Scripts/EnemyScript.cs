using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	#region Variables
	
	// Public Variables
	
	// Private Variables
	
	#endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().die();
            GetComponent<MoveForwardScript>().enabled = false;
        }
    }
}
