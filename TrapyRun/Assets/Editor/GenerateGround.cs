using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GroundGenerator))]
public class GenerateGround : Editor
{
    GameObject cube;

    public override void OnInspectorGUI()
    {
        initializeVariables();
        
        base.OnInspectorGUI();

        GroundGenerator gg = (GroundGenerator)target;

        GUILayout.Space(15);

        if (GUILayout.Button("Generate Ground"))
        {
            gg.generateGround(cube, 5, 150);
        }
    }

    void initializeVariables()
    {
        cube = Resources.Load<GameObject>("Prefabs/Cube");
    }
}
