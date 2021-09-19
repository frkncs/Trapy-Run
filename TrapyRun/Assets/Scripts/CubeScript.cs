using UnityEngine;
using UnityEngine.AI;

public class CubeScript : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    private static GroundGenerator gg;

    private bool canFall = false;
    private Collider col;
    private float colorTimer = 0;
    private float fallSpeed = 0;
    private float fallSpeedTimer = 0;
    private float g = 140, b = 140;
    private Material material;
    private Transform playerTrans;

    private const float maxFallSpeed = 15;
    private const float maxYPos = -20;
    private const float r = 255;

    #endregion Variables

    private void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        if (canFall)
        {
            FallCube();
            ChangeCubeColor();
        }

        if (transform.position.y <= maxYPos)
        {
            Destroy(gameObject);
        }
    }

    public void Fall()
    {
        if (gg.isLevelIncludeNavMesh) CreateNavMeshObstacle();

        material.color = new Color(r / 255, g / 255, b / 255);
        canFall = true;
        col.enabled = false;
    }

    private void ChangeCubeColor()
    {
        colorTimer += Time.deltaTime;

        if (colorTimer >= .1f && g > 70 && b > 70)
        {
            g -= 10;
            b -= 10;

            colorTimer = 0;

            material.color = new Color(r, g / 255, b / 255);
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

    private void FallCube()
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

    private void InitializeVariables()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        material = GetComponent<MeshRenderer>().material;
        col = GetComponent<BoxCollider>();

        if (gg == null)
        {
            gg = FindObjectOfType<GroundGenerator>().GetComponent<GroundGenerator>();
        }
    }
}