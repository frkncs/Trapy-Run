using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    static GroundGenerator gg;

    float fallSpeed = 15;
    float maxYPos = -20;

    bool canFall = false;

    #endregion

    private void Start()
    {
        if (gg == null) gg = GameObject.FindObjectOfType<GroundGenerator>().GetComponent<GroundGenerator>();
    }

    private void Update()
    {
        if (canFall)
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);

        if (transform.position.y <= maxYPos)
            Destroy(gameObject);
    }

    public void fall()
    {
        if (gg.isLevelIncludeNavMesh) createNavMeshObstacle();
        GetComponent<MeshRenderer>().material.color = Color.red;
        canFall = true;
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
