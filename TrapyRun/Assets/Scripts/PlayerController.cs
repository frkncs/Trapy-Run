using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    float firstFingerX, lastFingerX;

    bool gameOver = false;

    #endregion

    void Update()
    {
        MoveHorizontal();
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

            transform.position += new Vector3(dif, 0, 0) * 0.8f;

            firstFingerX = lastFingerX;
        }
    }

    float getMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.y + 5;

        return Camera.main.ScreenToWorldPoint(mousePos).x;
    }
}
