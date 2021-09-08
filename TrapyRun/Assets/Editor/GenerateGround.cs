using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GroundGenerator))]
public class GenerateGround : Editor
{
    GameObject cube, barrier;

    public override void OnInspectorGUI()
    {
        initializeVariables();
        
        base.OnInspectorGUI();

        GroundGenerator gg = (GroundGenerator)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Ground"))
        {
            gg.generateGround(cube, barrier);
        }
        else if (GUILayout.Button("Delete All Ground Items"))
        {
            gg.deleteGroundItems();
        }

        GUILayout.EndHorizontal();
    }

    void initializeVariables()
    {
        cube = Resources.Load<GameObject>("Prefabs/Cube");
        barrier = Resources.Load<GameObject>("Prefabs/Barrier");
    }
}
