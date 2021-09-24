using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] private float moveSpeed = 10;

    private float _firstFingerX, _lastFingerX;
    private float _swipeDiff;

    #endregion Variables

    public void MoveAndRotate()
    {
        CalcInputValues();
        Rotate();
        Move();
    }

    public void ResetLookRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void CalcInputValues()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstFingerX = GetMousePos();
        }
        else if (Input.GetMouseButton(0))
        {   
            _lastFingerX = GetMousePos();

            _swipeDiff = _lastFingerX - _firstFingerX;

            _firstFingerX = _lastFingerX;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _swipeDiff = 0;
            ResetLookRotation();
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