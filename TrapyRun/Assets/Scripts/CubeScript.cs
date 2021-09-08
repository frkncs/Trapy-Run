using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (!rb.isKinematic && transform.position.y <= maxYPos)
            Destroy(gameObject);
    }

    public void fall()
    {
        if (rb != null && rb.isKinematic)
        {
            if (GroundGenerator.isLevelIncludeNavMesh) createNavMeshObstacle();

            rb.isKinematic = false;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    void createNavMeshObstacle()
    {
        GameObject go = new GameObject("NavMeshObstacle");
        go.AddComponent<NavMeshObstacle>();
        go.GetComponent<NavMeshObstacle>().carving = true;
        go.GetComponent<NavMeshObstacle>().carveOnlyStationary = false;

        Instantiate(go, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
    }
}
