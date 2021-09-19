using System.Collections;
using Assets.Scripts;
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

        FollowPlayer();
    }

    private void OnEnable()
    {
        Actions.WinEvent += OnWinEvent;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Actions.WinEvent -= OnWinEvent;
    }

    private void OnWinEvent()
    {
        ActivateAngle();
        StartConfettiEffect();
        StopAllCoroutines();
        StartRotation();
    }

    private void FollowPlayer()
    {
        StartCoroutine(MovementLoop());
    }

    private IEnumerator MovementLoop()
    {
        while (true)
        {
            Move();
            yield return null;
        }
    }

    private IEnumerator RotationLoop()
    {
        while (true)
        {
            if (player != null)
            {
                transform.LookAt(player.transform);
            }
            else
            {
                transform.LookAt(heliStopTrans);
            }
            
            yield return null;
        }
    }

    private void StartRotation()
    {
        StartCoroutine(RotationLoop());
    }

    private void Move()
    {
        playerPos = player.transform.position;
        transform.position = playerPos + offset;
    }

    private void InitializeVariables()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;

        Transform helicopter = GameObject.FindGameObjectWithTag("Helicopter").transform;
        heliStopTrans = helicopter.Find("Heli_Stop").transform;

        offset = transform.position - playerPos;
    }

    private void ActivateAngle()
    {
        canChangeAngle = true;
    }

    private void StartConfettiEffect()
    {
        transform.Find("Confetti_01").gameObject.SetActive(true);
        transform.Find("Confetti_02").gameObject.SetActive(true);
    }
}