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

    bool canChangeAngle = false;

	#endregion

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        pc = playerTrans.GetComponent<PlayerController>();
        playerPos = playerTrans.position;

        offset = transform.position - playerPos;

        pc.ChangeCameraAngle += changeAngle;
    }

    private void Update()
    {
        if (canChangeAngle)
        {
            transform.LookAt(playerTrans);
        }
    }

    void LateUpdate()
    {
        if (!pc.gameOver && !canChangeAngle)
        {
            playerPos = playerTrans.position;
            transform.position = playerPos + offset;
        }
    }

    void changeAngle()
    {
        canChangeAngle = true;
    }
}
