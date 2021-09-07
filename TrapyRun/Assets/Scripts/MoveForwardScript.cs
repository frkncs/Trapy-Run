using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] float speed = 10;

    #endregion

    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * speed;
    }
}
