using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    bool canMove = false;

    const float turnSpeed = 13;
    const float moveSpeed = 10;
    const float rotX = 340;

    #endregion

    void Start()
    {
        PlayerController.MoveHelicopter += moveHelicopter;
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector3.up * Time.deltaTime * turnSpeed * .4f);

            if (transform.localRotation.eulerAngles.x > rotX) transform.Rotate(Vector3.right * -1 * Time.deltaTime * turnSpeed);

            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }
    }

    void moveHelicopter()
    {
        canMove = true;
    }
}
