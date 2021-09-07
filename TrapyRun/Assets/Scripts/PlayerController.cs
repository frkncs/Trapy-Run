using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    float firstFingerX, lastFingerX;
	
	#endregion

    void Update()
    {
        MoveHorizontal();
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
        mousePos.z = transform.position.z + 5;

        return Camera.main.ScreenToWorldPoint(mousePos).x;
    }
}
