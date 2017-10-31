using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : ScriptableObject
{
    #region Monster
    //몬스터 관련 
    public List<MonsterName> spawnEnemyList;
    public GameObject bossModule;
    #endregion
    #region Tile
    //타일 관련
    [SerializeField]
    private List<Sprite> normalTileList;
    [SerializeField]
    private List<Sprite> wallTileList;
    [SerializeField]
    private Sprite spriteDoor;
    [SerializeField]
    private List<Color> randomTileColor;


    public List<Sprite> GetNormalTileList()
    {
        return normalTileList;
    }

    public List<Sprite> GetWallTileList()
    {
        return wallTileList;
    }

    public Sprite GetDoorTile()
    {
        if (spriteDoor == null) return null;
        return spriteDoor;
    }

    public Color GetRandomTileColor()
    {
        if (randomTileColor == null) return Color.white;

        return randomTileColor[Random.Range(0, randomTileColor.Count)];

    }
    #endregion
    #region Map
    public int MinRoomNum;
    public int MaxRoomNum;
    public int RoomMaxSize;
    public int RoomMinSize;

    private int hideNum = 10;    

    #endregion

}
