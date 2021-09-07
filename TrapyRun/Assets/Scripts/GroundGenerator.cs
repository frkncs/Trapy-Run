using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    GameObject cube;

    float groundStartZPos = -10;
	
	#endregion

    void Start()
    {
        cube = Resources.Load<GameObject>("Prefabs/Cube");

        generateGround(5, 150);
    }

    void generateGround(int row, int column)
    {
        // Row count for just one side
        // eg. [row] left + [row] right + 1 center

        float distanceBetween2Cubes = cube.transform.localScale.x;

        float defaultCloneXPos = -(row * distanceBetween2Cubes);

        float cloneXPos = -(row * distanceBetween2Cubes);
        float cloneZPos = groundStartZPos;

        for (float z = 0; z < column; z++)
        {
            for (float x = 0; x < row * 2 + 1; x++)
            {
                Instantiate(cube, new Vector3(cloneXPos, -1.5f, cloneZPos), Quaternion.identity);
                cloneXPos += distanceBetween2Cubes;
            }

            cloneZPos += distanceBetween2Cubes;
            cloneXPos = defaultCloneXPos;
        }
    }
}
