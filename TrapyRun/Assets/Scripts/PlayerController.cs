using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    float firstFingerX, lastFingerX;

    float speed = 10;
	
	#endregion

    void Update()
    {
        MoveForward();
        MoveHorizontal();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void MoveHorizontal()
    {
        float mousePosX = getMousePos();

        if (Input.GetMouseButtonDown(0)) firstFingerX = mousePosX;
        else if (Input.GetMouseButton(0))
        {
            lastFingerX = mousePosX;

            float dif = lastFingerX - firstFingerX;

            transform.position += new Vector3(dif, 0, 0);

            firstFingerX = lastFingerX;
        }
    }

    float getMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePos).x;
    }
}
