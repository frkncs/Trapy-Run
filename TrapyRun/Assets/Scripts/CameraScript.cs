using UnityEngine;

public class CameraScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    private Transform heliStopTrans;

    private GameObject player;
    private Vector3 playerPos;
    private Vector3 offset;

    private bool canChangeAngle = false;

    #endregion Variables

    private void Start()
    {
        InitializeVariables();
    }

    private void OnEnable()
    {
        Actions.ChangeCameraAngle += ChangeAngle;
        Actions.StartConfettiEffect += StartConfettiEffect;
    }

    private void OnDisable()
    {
        Actions.ChangeCameraAngle -= ChangeAngle;
        Actions.StartConfettiEffect -= StartConfettiEffect;
    }

    private void Update()
    {
        if (canChangeAngle)
        {
            if (player != null)
            {
                transform.LookAt(player.transform);
            }
            else
            {
                transform.LookAt(heliStopTrans);
            }
        }
    }

    private void LateUpdate()
    {
        if (!PlayerController.gameOver && !canChangeAngle)
        {
            playerPos = player.transform.position;
            transform.position = playerPos + offset;
        }
    }

    private void InitializeVariables()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;

        Transform helicopter = GameObject.FindGameObjectWithTag("Helicopter").transform;
        heliStopTrans = helicopter.Find("Heli_Stop").transform;

        offset = transform.position - playerPos;
    }

    private void ChangeAngle()
    {
        canChangeAngle = true;
    }

    private void StartConfettiEffect()
    {
        transform.Find("Confetti_01").gameObject.SetActive(true);
        transform.Find("Confetti_02").gameObject.SetActive(true);
    }
}