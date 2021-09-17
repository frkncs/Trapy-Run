using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [SerializeField] private float moveSpeed = 10;
    
    private float firstFingerX, lastFingerX;
    private float _swipeDiff;

    #endregion Variables
    
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
        else if (Input.GetMouseButtonUp(0))
        {
            RotateForward();
        }
    }

    public void MoveAndRotate()
    {
        CalcInputValues();
        Rotate();
        Move();
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(0, _swipeDiff * 50, 0);
    }

    private void RotateForward()
    {
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z + 5));
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
