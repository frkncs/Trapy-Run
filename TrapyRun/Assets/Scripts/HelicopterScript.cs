using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    bool canMove = false;

    const float upSpeed = 3.5f;
    const float turnSpeed = 13;
    const float moveSpeed = 12;
    const float rotX = 340;

    #endregion

    private void OnEnable()
    {
        Actions.MoveHelicopter += moveHelicopter;
    }
    private void OnDisable()
    {
        Actions.MoveHelicopter -= moveHelicopter;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            transform.Translate(Vector3.up * Time.deltaTime * upSpeed);

            if (transform.localRotation.eulerAngles.x > rotX) transform.Rotate(Vector3.right * -1 * Time.deltaTime * turnSpeed);

            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }
    }

    void moveHelicopter()
    {
        canMove = true;
    }
}
