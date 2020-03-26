using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
public class LevelDesigner : EditorWindow
{
    private Object _targetObject;
    private Object _targetPosition;
    private float _offset;
    private Vector3 _size;
    private Vector2Int _amount;

    private bool _isDiagonal;
    private GameObject _currentParent; 

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Level Designer")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LevelDesigner window = (LevelDesigner)EditorWindow.GetWindow(typeof(LevelDesigner));
        window.Show();
    }

    void OnGUI()
    {
        _targetObject = EditorGUILayout.ObjectField("Target Object : ", _targetObject, typeof(GameObject), true);
        _targetPosition = EditorGUILayout.ObjectField("Target Position : ", _targetPosition, typeof(Transform), true);
        _offset = EditorGUILayout.Slider("Offset : ", _offset, 1f, 2f);
        _size = EditorGUILayout.Vector3Field("Size : ", _size);
        _amount = EditorGUILayout.Vector2IntField("Amount : ", _amount);
        _isDiagonal = EditorGUILayout.Toggle("Diagonal : ", _isDiagonal);

        if (GUILayout.Button("Generate") && _targetPosition != null && _targetObject != null)
        {
            _currentParent = new GameObject("GeneratedObjectsParent");
            Vector3 centerPosition = Vector3.zero;
            int generatedCount = 0;

            for(int i = 0; i < _amount.x; i++){
                for(int j = 0; j < _amount.y - (_isDiagonal == true ? i : 0); j++){
                    GameObject temp = (GameObject)PrefabUtility.InstantiatePrefab((GameObject)_targetObject);
                    temp.transform.localScale = _size;

                    temp.transform.position = ((Transform)_targetPosition).position + Vector3.left * _amount.x / 2 + Vector3.back * _amount.y / 2
                    + Vector3.forward * i * _offset * temp.transform.localScale.z + Vector3.right * j * _offset * temp.transform.localScale.x;

                    temp.transform.SetParent((Transform)_targetPosition);

                    centerPosition += temp.transform.position;

                    generatedCount++;
                }
            }

            centerPosition /= generatedCount;

            _currentParent.transform.position = centerPosition;

            int childCount = ((Transform)_targetPosition).childCount;

            for (int i = 0; i < childCount; i++)
            {
                ((Transform)_targetPosition).GetChild(0).SetParent(_currentParent.transform);
            }

            Selection.activeGameObject = _currentParent;

            _currentParent = null;
        }
    }
}
#endif