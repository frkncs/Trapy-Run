using UnityEngine;

public class CameraScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    PlayerController pc;
    Transform heliStopTrans;
    GameObject player;
    Vector3 playerPos;
    Vector3 offset;

    bool canChangeAngle = false;

	#endregion

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        playerPos = player.transform.position;

        Transform helicopter = GameObject.FindGameObjectWithTag("Helicopter").transform;
        heliStopTrans = helicopter.Find("Heli_Stop").transform;

        offset = transform.position - playerPos;

        PlayerController.ChangeCameraAngle += changeAngle;
    }

    private void Update()
    {
        if (canChangeAngle)
        {
            if (player != null)
                transform.LookAt(player.transform);
            else
                transform.LookAt(heliStopTrans);
        }
    }

    void LateUpdate()
    {
        if (!pc.gameOver && !canChangeAngle)
        {
            playerPos = player.transform.position;
            transform.position = playerPos + offset;
        }
    }

    void changeAngle()
    {
        canChangeAngle = true;
    }
}
