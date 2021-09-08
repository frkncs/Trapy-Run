using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] bool moveForward = false;
    [SerializeField] bool turnClockwise = false;
    [SerializeField] bool turnReverse = false;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float turnSpeed = 15;

    #endregion

    void Update()
    {
        if (moveForward)
            MoveForward();
        if (turnClockwise || turnReverse)
            Turn();
    }

    void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    void Turn()
    {
        transform.Rotate(new Vector3(0, (turnClockwise ? 1 : -1) * 360, 0) * Time.deltaTime * turnSpeed);
    }
}
