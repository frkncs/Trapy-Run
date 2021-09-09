using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    PlayerController pc;
    Transform playerTrans;
    Vector3 playerPos;
    Vector3 offset;

	#endregion

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        pc = playerTrans.GetComponent<PlayerController>();
        playerPos = playerTrans.position;

        offset = transform.position - playerPos;
    }

    void LateUpdate()
    {
        if (!pc.gameOver)
        {
            playerPos = playerTrans.position;
            transform.position = playerPos + offset;
        }
    }
}
