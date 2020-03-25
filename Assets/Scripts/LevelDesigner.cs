using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
public class LevelDesigner : EditorWindow
{
    public Object TargetObject;
    public Object TargetPosition;
    public float Offset;
    public int Amount;

    private static bool _isGenerated;
    private static GameObject _currentParent; 

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Level Designer")]
    static void Init()
    {
        _isGenerated = false;

        // Get existing open window or if none, make a new one:
        LevelDesigner window = (LevelDesigner)EditorWindow.GetWindow(typeof(LevelDesigner));
        window.Show();
    }

    void OnGUI()
    {
        TargetObject = EditorGUILayout.ObjectField("Target Object : ", TargetObject, typeof(GameObject), true);
        TargetPosition = EditorGUILayout.ObjectField("Target Position : ", TargetPosition, typeof(Transform), true);
        Offset = EditorGUILayout.Slider("Offset : ", Offset, 1f, 2f);
        Amount = EditorGUILayout.IntSlider("Amount : ", Amount, 1, 50);

        if (!_isGenerated && GUILayout.Button("Generate") && TargetPosition != null && TargetObject != null)
        {
            _currentParent = new GameObject("GeneratedObjectsParent");
            Vector3 centerPosition = Vector3.zero;

            for(int i = 0; i < Amount; i++){
                for(int j = 0; j < Amount; j++){
                    GameObject temp = Instantiate((GameObject)TargetObject);
                    temp.transform.position = ((Transform)TargetPosition).position + Vector3.left * Amount / 2 + Vector3.back * Amount / 2
                    + Vector3.forward * i * Offset * temp.transform.localScale.z + Vector3.right * j * Offset * temp.transform.localScale.x;

                    temp.transform.SetParent((Transform)TargetPosition);

                    centerPosition += temp.transform.position;
                }
            }

            centerPosition /= Amount * Amount;

            for (int i = 0; i < Amount; i++)
            {
                for (int j = 0; j < Amount; j++)
                {
                    _currentParent.transform.position = centerPosition;
                }
            }

            Selection.activeGameObject = ((Transform)TargetPosition).gameObject;

            _isGenerated = true;
        }

        if (_isGenerated && GUILayout.Button("Finalize"))
        {
            int childCount = ((Transform)TargetPosition).childCount;

            for(int i = 0; i < childCount; i++){
                ((Transform)TargetPosition).GetChild(0).SetParent(_currentParent.transform);
            }

            Selection.activeGameObject = _currentParent;

            _currentParent = null;

            _isGenerated = false;
        }

        if(_isGenerated && GUILayout.Button("Revert")){
            DestroyImmediate(_currentParent);

            _isGenerated = false;
        }
    }
}
#endif