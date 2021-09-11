using UnityEngine;

public class CameraScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    Transform heliStopTrans;
    GameObject player;
    Vector3 playerPos;
    Vector3 offset;

    bool canChangeAngle = false;

    #endregion

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;

        Transform helicopter = GameObject.FindGameObjectWithTag("Helicopter").transform;
        heliStopTrans = helicopter.Find("Heli_Stop").transform;

        offset = transform.position - playerPos;
    }

    private void OnEnable()
    {
        Actions.ChangeCameraAngle += changeAngle;
    }
    private void OnDisable()
    {
        Actions.ChangeCameraAngle -= changeAngle;
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
        if (!PlayerController.gameOver && !canChangeAngle)
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
