using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StageDataMaker : MonoBehaviour
{

    [MenuItem("GameObject/CreateCustomData")]
    static void CreateCustomData()
    {
        StageData newData = ScriptableObject.CreateInstance<StageData>();

        AssetDatabase.CreateAsset(newData, "Assets/Resources/StageData/DefaultStageData.asset");

    }

    void Start()
    {
        CreateCustomData();
    }

}
