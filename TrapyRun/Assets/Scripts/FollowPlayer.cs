using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    Transform playerTrans;
    Vector3 playerPos;
    Vector3 offset;

	#endregion

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = playerTrans.position;

        offset = transform.position - playerPos;
    }

    void LateUpdate()
    {
        playerPos = playerTrans.position;
        transform.position = playerPos + offset;
    }
}
