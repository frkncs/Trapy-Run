using Assets.Scripts;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] private bool useGlobalDirections = false;
    [SerializeField] private bool moveForward = false;
    [SerializeField] private bool moveHorizontal = false;
    [SerializeField] private bool canTurn = false;
    [SerializeField] private bool turnClockwise = false;
    [SerializeField] private Vector3 turnCoordinate;

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float turnSpeed = 0;

    private Vector3 dir = Vector3.right;
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
        else if (moveHorizontal)
        {
            MoveHorizontal();
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

    private void MoveHorizontal()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, 5, LayerMask.GetMask("Ground")))
        {
            dir *= -1;
        }

        transform.Translate(dir * (Time.deltaTime * moveSpeed));
    }

    private void Turn()
    {
        transform.Rotate(rot * (Time.deltaTime * turnSpeed));
    }
}