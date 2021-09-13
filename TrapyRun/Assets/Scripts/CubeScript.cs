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

    private float fallSpeedTimer = 0;
    private float fallSpeed = 0;
    private bool canFall = false;

    private const float maxFallSpeed = 15;
    private const float maxYPos = -20;

    #endregion Variables

    private void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        if (canFall)
        {
            if (fallSpeed < maxFallSpeed)
            {
                fallSpeedTimer += Time.deltaTime;

                if (fallSpeedTimer >= 0.04f)
                {
                    fallSpeed++;
                    fallSpeedTimer = 0;
                }
            }

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