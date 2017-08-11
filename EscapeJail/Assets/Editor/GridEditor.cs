using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Custom_Grid))]
public class GridEditor : Editor
{
    Custom_Grid grid;

    private int oldIndex = 0;


    //에디터가 처음 호출하는 함수
    private void OnEnable()
    {
        grid = (Custom_Grid)target;
    }

    [MenuItem("Assets/Create/TileSet")]
    static void CreateTileSet()
    {
        var asset = ScriptableObject.CreateInstance<TileSet>();
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);

        path = "Assets/08.Maptool/";
        Debug.Log(path);
        //if (string.IsNullOrEmpty(path))
        //{
        //}
        //else if (Path.GetExtension(path) != "")
        //{
        //    path = path.Replace(Path.GetFileName(path), "");
        //}
        //else
        //{
        //    path += "/";
        //}

        var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "TileSet.asset");
        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
        asset.hideFlags = HideFlags.DontSave;


    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
     
        grid.width = createFloatSlider("Width", grid.width);
        grid.height = createFloatSlider("Height", grid.height);
        grid.isRenderGrid = createToggle("RenderGrid", grid.isRenderGrid);
        grid.nowLayerNumber = createIntSlider("RenderLayer", grid.nowLayerNumber);

        //버튼이 눌렸을때
        if (GUILayout.Button("Open Grid Window"))
        {
            GridWindow window = (GridWindow)EditorWindow.GetWindow(typeof(GridWindow));
            window.Init();
        }

        //프리팹 불러오기

        EditorGUI.BeginChangeCheck();
        var newTilePrefab = (Transform)EditorGUILayout.ObjectField("Tile Prefab", grid.tilePrefab, typeof(Transform), false);
        if (EditorGUI.EndChangeCheck())
        {
            grid.tilePrefab = newTilePrefab;
            Undo.RecordObject(target, "Grid Changed");
        }


        //타일맵
        EditorGUI.BeginChangeCheck();
        var newTileSet = (TileSet)EditorGUILayout.ObjectField("TileSet", grid.tileSet, typeof(TileSet), false);
        if (EditorGUI.EndChangeCheck())
        {
            grid.tileSet = newTileSet;
            Undo.RecordObject(target, "Grid changed");
        }


        if (grid.tileSet != null)
        {
            EditorGUI.BeginChangeCheck();
            var names = new string[grid.tileSet.prefabs.Length];
            var values = new int[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = grid.tileSet.prefabs[i] != null ? grid.tileSet.prefabs[i].name : "";
                values[i] = i;

            }

            var index = EditorGUILayout.IntPopup("Select Tile", oldIndex, names, values);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Grid Changed");
                if (oldIndex != index)
                {
                    oldIndex = index;
                    grid.tilePrefab = grid.tileSet.prefabs[index];

                    //float width = grid.tilePrefab.GetComponent<Renderer>().bounds.size.x;
                    //float height = grid.tilePrefab.GetComponent<Renderer>().bounds.size.y;

                    //grid.width = width;
                    //grid.height = height;

                }
            }
        }




    }


    private float createFloatSlider(string labelName, float sliderPosition)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid " + labelName);
        sliderPosition = EditorGUILayout.Slider(sliderPosition, 1f, 100f, null);
        GUILayout.EndHorizontal();

        return sliderPosition;
    }

    private int createIntSlider(string labelName, float sliderPosition)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid " + labelName);
        sliderPosition = EditorGUILayout.Slider(sliderPosition, 0f, 100f, null);
        GUILayout.EndHorizontal();

        return (int)sliderPosition;
    }

    private bool createToggle(string labelName,bool isCheck)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(labelName);
        isCheck = EditorGUILayout.Toggle(isCheck);
        GUILayout.EndHorizontal();

        return isCheck;
    }

    //에디터 모드에서 액션에 반응
    private void OnSceneGUI()
    {
        int controlId = GUIUtility.GetControlID(FocusType.Passive);
        Event e = Event.current;
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = ray.origin;


        if (e.isMouse&&e.type == EventType.MouseDrag && e.button == 0)
        {
          //  Event.current.Use();
            GUIUtility.hotControl = controlId;
            e.Use();

            GameObject gameObject;
            Transform prefab = grid.tilePrefab;

            if (prefab)
            {
                Undo.IncrementCurrentGroup();
                Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2.0f, Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2.0f, 0f);

                //중복은 덮어씌움
                if (GetTransformFromPosition(aligned) != null)
                {
                    Transform transform = GetTransformFromPosition(aligned);
                    if (transform != null)
                    {
                        DestroyImmediate(transform.gameObject);
                    }
                }

                gameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab.gameObject);
                gameObject.transform.position = aligned;
                gameObject.transform.parent = grid.transform;
                SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.sortingOrder = grid.nowLayerNumber;
                }
                Undo.RegisterCreatedObjectUndo(gameObject, "Create" + gameObject.name);
            }
        }

        ////설치                                                //0번 왼클릭
        //if (e.isMouse && e.type == EventType.MouseDown&&e.button==0)
        //{
        //    GUIUtility.hotControl = controlId;
        //    e.Use();

        //    GameObject gameObject;
        //    Transform prefab = grid.tilePrefab;

        //    if (prefab)
        //    {
        //        Undo.IncrementCurrentGroup();
        //        Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2.0f, Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2.0f,0f);

        //        //중복 방지
        //        if (GetTransformFromPosition(aligned) != null) return;     

        //        gameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab.gameObject);
        //        gameObject.transform.position = aligned;
        //        gameObject.transform.parent = grid.transform;
        //        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        //        if (renderer != null)
        //        {
        //            renderer.sortingOrder = grid.nowLayerNumber;
        //        }
        //        Undo.RegisterCreatedObjectUndo(gameObject, "Create" + gameObject.name);
        //    }
        //}

        if (e.isMouse && e.type == EventType.MouseDrag && e.button == 1)
        {
            GUIUtility.hotControl = controlId;
            e.Use();
            Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2.0f, Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2.0f, 0f);
            Transform transform = GetTransformFromPosition(aligned);
            if (transform != null)
            {
                DestroyImmediate(transform.gameObject);
            }

        }

            //삭제                                                //1번 오른클릭
        //    if (e.isMouse & e.type == EventType.MouseDown && e.button == 1)
        //{
        //    GUIUtility.hotControl = controlId;
        //    e.Use();
        //    Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2.0f, Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2.0f, 0f);
        //    Transform transform = GetTransformFromPosition(aligned);
        //    if (transform != null)
        //    {
        //        DestroyImmediate(transform.gameObject);
        //    }
        //}

        if (e.isMouse&&e.type == EventType.MouseUp)
        {
            GUIUtility.hotControl = 0;
        }
    }




    Transform GetTransformFromPosition(Vector3 aligned)
    {
        int i = 0;
        while (i < grid.transform.childCount)
        {
            Transform transform = grid.transform.GetChild(i);
            if (transform.position == aligned)
            {
                return transform;
            }
            i++;
        }
        return null;
    }

}
