#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridWindow : EditorWindow
{
    Custom_Grid grid;

    public void Init()
    {
        grid = (Custom_Grid)FindObjectOfType(typeof(Custom_Grid));
    }

    private void OnGUI()
    {
        grid.gridColor = EditorGUILayout.ColorField(grid.gridColor, GUILayout.Width(200));
    }

}
#endif