using UnityEngine;

public class HelicopterScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    private bool canMove = false;

    private const float upSpeed = 3.5f;
    private const float turnSpeed = 13;
    private const float moveSpeed = 12;
    private const float rotX = 340;

    #endregion Variables

    private void OnEnable()
    {
        Actions.HeliEvent += MoveHelicopter;
    }

    private void OnDisable()
    {
        Actions.HeliEvent -= MoveHelicopter;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            transform.Translate(Vector3.up * (Time.deltaTime * upSpeed));

            if (transform.localRotation.eulerAngles.x > rotX)
            {
                transform.Rotate(Time.deltaTime * turnSpeed * -1 * Vector3.right);
            }

            transform.Translate(Vector3.back * (Time.deltaTime * moveSpeed));
        }
    }

    private void MoveHelicopter()
    {
        canMove = true;
    }
}