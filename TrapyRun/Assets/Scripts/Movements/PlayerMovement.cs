using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] private float moveSpeed = 10;

    private float firstFingerX, lastFingerX;
    private float _swipeDiff;

    #endregion Variables

    public void MoveAndRotate()
    {
        CalcInputValues();
        Rotate();
        Move();

        if (Input.GetMouseButtonUp(0))
        {
            RotateForward();
        }
    }

    public void ResetLookRotation()
    {
        _swipeDiff = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void CalcInputValues()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstFingerX = GetMousePos();
        }
        else if (Input.GetMouseButton(0))
        {
            lastFingerX = GetMousePos();

            _swipeDiff = lastFingerX - firstFingerX;

            firstFingerX = lastFingerX;
        }
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(0, _swipeDiff * 50, 0);
    }

    private void Move()
    {
        MoveForward();
        MoveHorizontal();
    }

    private void RotateForward()
    {
        Vector3 _lookRot = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
        transform.LookAt(_lookRot);
    }

    private void MoveHorizontal()
    {
        transform.position += new Vector3(_swipeDiff, 0, 0) * 0.9f;
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * (Time.deltaTime * moveSpeed);
    }

    private float GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.y + 5;

        return Camera.main.ScreenToWorldPoint(mousePos).x;
    }
}