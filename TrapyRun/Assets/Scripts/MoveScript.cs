using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] bool useGlobalDirections = false;
    [SerializeField] bool moveForward = false;
    [SerializeField] bool moveHorizontal = false;
    [SerializeField] bool turnClockwise = false;
    [SerializeField] bool turnReverse = false;
    [SerializeField] bool turnXCoordinate = false;
    [SerializeField] bool turnYCoordinate = true;
    [SerializeField] bool turnZCoordinate = false;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float turnSpeed = 0;

    Vector3 dir = Vector3.right;
    Vector3 rot = Vector3.zero;

    #endregion

    private void Start()
    {
        if (turnXCoordinate)
            rot = new Vector3((turnClockwise ? 1 : -1) * 360, 0, 0);
        if (turnYCoordinate)
            rot = new Vector3(0, (turnClockwise ? 1 : -1) * 360, 0);
        else if (turnZCoordinate)
            rot = new Vector3(0, 0, (turnClockwise ? 1 : -1) * 360);
    }

    void Update()
    {
        if (moveForward)
            MoveForward();
        else if (moveHorizontal)
            MoveHorizontal();

        if (turnClockwise || turnReverse)
            Turn();
    }

    void MoveForward()
    {
        if (!useGlobalDirections)
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        else
            transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }

    void MoveHorizontal()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, 5, LayerMask.GetMask("Ground")))
            dir *= -1;

        transform.Translate(dir * Time.deltaTime * moveSpeed);
    }

    void Turn()
    {
        transform.Rotate(rot * Time.deltaTime * turnSpeed);
    }
}
