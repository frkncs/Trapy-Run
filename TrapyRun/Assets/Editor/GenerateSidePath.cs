using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SidePathGenerator))]
public class GenerateSidePath : Editor
{
    #region Variables

    // Public Variables

    // Private Variables
    GameObject cube;

    #endregion

    public override void OnInspectorGUI()
    {
        initializeVariables();

        base.OnInspectorGUI();

        SidePathGenerator spg = (SidePathGenerator)target;

        if (GUILayout.Button("Generate Side Path"))
        {
            spg.GenerateSidePath(cube);
        }
    }

    void initializeVariables()
    {
        cube = Resources.Load<GameObject>("Prefabs/Cube");
    }
}
