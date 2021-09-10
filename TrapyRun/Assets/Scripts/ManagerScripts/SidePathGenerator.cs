using UnityEngine;
using UnityEngine.AI;

public class SidePathGenerator : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] int row, column;

    GroundGenerator gg;

    bool isLevelIncludeNavMesh = false;

    #endregion

    
    public void generateSidePath(GameObject cube)
    {
        // Row count for just one side
        // eg. [row] left + [row] right + 1 center

        cube.tag = "SideCube";

        gg = GameObject.Find("GroundGenerator").GetComponent<GroundGenerator>();

        isLevelIncludeNavMesh = gg.isLevelIncludeNavMesh;

        if (isLevelIncludeNavMesh)
        {
            cube.AddComponent<NavMeshObstacle>();
            cube.isStatic = true;
        }

        float distanceBetween2Cubes = cube.transform.localScale.x;

        float defaultCloneXPos = -(row * distanceBetween2Cubes);

        float cloneXPos = -(row * distanceBetween2Cubes);
        float cloneZPos = 0;

        GameObject cubeObject = new GameObject("Side Path Object");

        for (float z = 0; z < column; z++)
        {
            for (float x = 0; x < row * 2 + 1; x++)
            {
                GameObject go = Instantiate(cube, new Vector3(cloneXPos, -(1.5f * distanceBetween2Cubes), cloneZPos), Quaternion.identity);
                go.transform.SetParent(cubeObject.transform);
                cloneXPos += distanceBetween2Cubes;
            }

            cloneZPos += distanceBetween2Cubes;
            defaultCloneXPos++;
            cloneXPos = defaultCloneXPos;
        }
    }
}
