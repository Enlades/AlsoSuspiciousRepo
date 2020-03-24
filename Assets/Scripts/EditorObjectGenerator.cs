using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorObjectGenerator : EditorWindow
{
    public Object TargetObject;
    public Object TargetPosition;
    public float Offset;
    public int Amount;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Object Generator")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        EditorObjectGenerator window = (EditorObjectGenerator)EditorWindow.GetWindow(typeof(EditorObjectGenerator));
        window.Show();
    }

    void OnGUI()
    {
        TargetObject = EditorGUILayout.ObjectField("Target Object : ", TargetObject, typeof(GameObject), true);
        TargetPosition = EditorGUILayout.ObjectField("Target Position : ", TargetPosition, typeof(Transform), true);
        Offset = EditorGUILayout.Slider("Offset : ", Offset, 0f, 1f);
        Amount = EditorGUILayout.IntSlider("Amount : ", Amount, 1, 50);

        if (GUILayout.Button("Generate"))
        {

        }

        EditorGUILayout.EndToggleGroup();
    }
}
