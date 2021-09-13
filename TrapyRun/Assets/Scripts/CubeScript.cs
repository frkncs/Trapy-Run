using UnityEngine;
using UnityEngine.AI;

public class CubeScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    private static GroundGenerator gg;

    private Transform playerTrans;
    private MeshRenderer mr;
    private Collider col;

    private readonly float fallSpeed = 15;
    private readonly float maxYPos = -20;

    private bool canFall = false;

    #endregion Variables

    private void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        if (canFall)
        {
            transform.Translate(Vector3.down * (Time.deltaTime * fallSpeed));
        }

        if (transform.position.y <= maxYPos)
        {
            Destroy(gameObject);
        }
    }

    public void Fall()
    {
        if (gg.isLevelIncludeNavMesh) CreateNavMeshObstacle();

        if (ColorUtility.TryParseHtmlString("#F65149", out Color c))
        {
            mr.material.color = c;
        }

        canFall = true;
        col.enabled = false;
    }

    private void InitializeVariables()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<BoxCollider>();

        if (gg == null)
        {
            gg = FindObjectOfType<GroundGenerator>().GetComponent<GroundGenerator>();
        }
    }

    private void CreateNavMeshObstacle()
    {
        GameObject go = new GameObject("NavMeshObstacle");
        go.AddComponent<NavMeshObstacle>();
        go.GetComponent<NavMeshObstacle>().carving = true;
        go.GetComponent<NavMeshObstacle>().carveOnlyStationary = false;

        Instantiate(go, playerTrans.position, Quaternion.identity);
    }
}