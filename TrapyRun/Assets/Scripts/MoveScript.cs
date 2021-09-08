using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] bool moveForward = false;
    [SerializeField] bool moveHorizontal = false;
    [SerializeField] bool turnClockwise = false;
    [SerializeField] bool turnReverse = false;

    [SerializeField] float moveSpeed = 10;
    [Range(0,1)]
    [SerializeField] float turnSpeed = 0;

    Vector3 dir = Vector3.right;

    #endregion

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
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    void MoveHorizontal()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, 5, LayerMask.GetMask("Ground")))
            dir *= -1;

        transform.Translate(dir * Time.deltaTime * moveSpeed);
    }

    void Turn()
    {
        transform.Rotate(new Vector3(0, (turnClockwise ? 1 : -1) * 360, 0) * Time.deltaTime * turnSpeed);
    }
}
