using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    Rigidbody rb;

    float maxYPos = -15;

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!rb.isKinematic)
        {
            if (transform.position.y <= maxYPos) Destroy(gameObject);
        }
    }

    public void fall()
    {
        if (rb != null && rb.isKinematic) rb.isKinematic = false;
    }
}
