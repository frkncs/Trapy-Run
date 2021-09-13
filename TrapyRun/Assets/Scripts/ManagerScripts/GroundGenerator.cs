using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    #region Variables

    // Public Variables
    public bool isLevelIncludeNavMesh = false;

    // Private Variables
    [SerializeField] private int row, column;

    #endregion Variables

    public void GenerateGround(GameObject cube, GameObject barrier)
    {
        // Row count for just one side
        // eg. [row] left + [row] right + 1 center

        cube.tag = "GroundCube";

        float distanceBetween2Cubes = cube.transform.localScale.x;

        float defaultCloneXPos = -(row * distanceBetween2Cubes);

        float cloneXPos = -(row * distanceBetween2Cubes);
        float cloneZPos = 0;

        GameObject cubeObject = new GameObject("Cube Object");

        for (float z = 0; z < column; z++)
        {
            for (float x = 0; x < row * 2 + 1; x++)
            {
                GameObject go = Instantiate(cube, new Vector3(cloneXPos, -(1.5f * distanceBetween2Cubes), cloneZPos), Quaternion.identity);
                go.transform.SetParent(cubeObject.transform);
                cloneXPos += distanceBetween2Cubes;

                if (x == 0)
                {
                    AddBarrier(go, barrier, false);
                }
                else if (x == row * 2 + 1 - 1)
                {
                    AddBarrier(go, barrier, true);
                }
            }

            cloneZPos += distanceBetween2Cubes;
            cloneXPos = defaultCloneXPos;
        }
    }

    public void DeleteGroundItems()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("GroundCube");

        foreach (GameObject cube in cubes)
        {
            DestroyImmediate(cube);
        }
    }

    private void AddBarrier(GameObject objectToParent, GameObject barrier, bool isRight)
    {
        GameObject barrierObj = Instantiate(barrier, Vector3.zero, Quaternion.identity);
        barrierObj.transform.SetParent(objectToParent.transform);
        barrierObj.transform.localPosition = new Vector3((isRight ? 1 : -1) * .35f, .55f, 0);
        barrierObj.transform.localScale = new Vector3(.3f, .1f, 1);
    }
}