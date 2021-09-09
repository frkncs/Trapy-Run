using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    PlayerController pc;
    Transform playerTrans, heliStopTrans;
    Vector3 playerPos;
    Vector3 offset;

    bool canChangeAngle = false;

	#endregion

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        pc = playerTrans.GetComponent<PlayerController>();
        playerPos = playerTrans.position;

        Transform helicopter = GameObject.FindGameObjectWithTag("Helicopter").transform;
        heliStopTrans = helicopter.Find("Heli_Stop").transform;

        offset = transform.position - playerPos;

        PlayerController.ChangeCameraAngle += changeAngle;
    }

    private void Update()
    {
        if (canChangeAngle)
        {
            transform.LookAt(heliStopTrans);
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
