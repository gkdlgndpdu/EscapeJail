using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoObject : ScriptableObject
{
    public int RoomNum;

}


public class StageData : ScriptableObject
{
    public List<GameObject> SpawnMonsterList;
    public int roomNum;


    public static StageData CreateInstance()
    {
        var data = ScriptableObject.CreateInstance<StageData>();      
        return data;
    }
}
