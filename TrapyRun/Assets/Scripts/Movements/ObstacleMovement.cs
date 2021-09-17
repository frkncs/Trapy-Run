using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool useGlobalDirections = false;
    [SerializeField] private bool moveForward = false;
    [SerializeField] private bool canTurn = false;
    [SerializeField] private bool turnClockwise = false;
    [SerializeField] private Vector3 turnCoordinate;

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float turnSpeed = 0;

    private Vector3 rot = Vector3.zero;

    #endregion Variables

    private void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        if (moveForward)
        {
            MoveForward();
        }

        if (canTurn)
        {
            Turn();
        }
    }

    private void InitializeVariables()
    {
        InitializeRotValue();
    }

    private void InitializeRotValue()
    {
        rot = turnCoordinate * (turnClockwise ? 1 : -1) * 360;
    }

    private void MoveForward()
    {
        if (!useGlobalDirections)
        {
            transform.position += transform.forward * (Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.position += Vector3.forward * (Time.deltaTime * moveSpeed);
        }
    }

    private void Turn()
    {
        transform.Rotate(rot * (Time.deltaTime * turnSpeed));
    }
}
