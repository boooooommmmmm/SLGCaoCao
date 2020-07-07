using Framework.TBS.Grid.UnitGenerators;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorInspector : Editor
{
    GridGenerator unitGenerator;

    private void OnEnable()
    {
        unitGenerator = target as GridGenerator;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //unitGenerator.V2CellSize = EditorGUILayout.Vector2Field("size: ", unitGenerator.V2CellSize);

        if (GUILayout.Button("Generate Grid"))
        {
            unitGenerator.GenerateGrid();
        }

        if (GUILayout.Button("Snap Grid"))
        {
            unitGenerator.SnapToGrid();
        }

        serializedObject.Update();
    }
}
