using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    Animator animator;

    float firstFingerX, lastFingerX;

    bool gameOver = false;

    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveHorizontal();
        checkIsFloating();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameOver) return;

        if (collision.gameObject.CompareTag("GroundCube"))
            collision.gameObject.GetComponent<CubeScript>().fall();
    }

    public void die()
    {
        GetComponent<MoveScript>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        gameOver = true;
    }

    void MoveHorizontal()
    {
        if (Input.GetMouseButtonDown(0)) firstFingerX = getMousePos();
        else if (Input.GetMouseButton(0))
        {
            lastFingerX = getMousePos();

            float dif = lastFingerX - firstFingerX;

            transform.rotation = Quaternion.Euler(0, dif * 50, 0);
            transform.position += new Vector3(dif, 0, 0) * 0.6f;

            firstFingerX = lastFingerX;
        }
    }

    float getMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.y + 5;

        return Camera.main.ScreenToWorldPoint(mousePos).x;
    }

    void checkIsFloating()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, 5, LayerMask.GetMask("Ground")))
        {
            if (!animator.GetBool("isFloating")) animator.SetBool("isFloating", true);
        }
        else
        {
            if (animator.GetBool("isFloating")) animator.SetBool("isFloating", false);
        }
    }
}
