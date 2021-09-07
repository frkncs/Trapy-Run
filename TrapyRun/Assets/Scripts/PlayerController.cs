using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    float speed = 10;
	
	#endregion

    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
