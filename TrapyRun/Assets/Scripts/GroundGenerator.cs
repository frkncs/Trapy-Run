using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundGenerator : MonoBehaviour
{
    #region Variables

    // Public Variables
    public static bool isLevelIncludeNavMesh = false;

    // Private Variables
    [SerializeField] int row, column;

    float groundStartZPos = -10;

    #endregion

    public void generateGround(GameObject cube)
    {
        // Row count for just one side
        // eg. [row] left + [row] right + 1 center

        if (isLevelIncludeNavMesh)
        {
            cube.AddComponent<NavMeshObstacle>();
            cube.isStatic = true;
        }

        float distanceBetween2Cubes = cube.transform.localScale.x;

        float defaultCloneXPos = -(row * distanceBetween2Cubes);

        float cloneXPos = -(row * distanceBetween2Cubes);
        float cloneZPos = groundStartZPos;

        GameObject cubeObject = new GameObject("Cube Object");

        for (float z = 0; z < column; z++)
        {
            for (float x = 0; x < row * 2 + 1; x++)
            {
                GameObject go = Instantiate(cube, new Vector3(cloneXPos, -(1.5f * distanceBetween2Cubes), cloneZPos), Quaternion.identity);
                go.transform.SetParent(cubeObject.transform);
                cloneXPos += distanceBetween2Cubes;
            }

            cloneZPos += distanceBetween2Cubes;
            cloneXPos = defaultCloneXPos;
        }
    }

    public void deleteGroundItems()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("GroundCube");

        foreach (GameObject cube in cubes)
        {
            DestroyImmediate(cube);
        }
    }
}

