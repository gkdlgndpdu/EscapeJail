using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : ScriptableObject
{
    #region Monster
    //몬스터 관련 
    public List<MonsterName> spawnEnemyList;
 
    #endregion
    #region Tile
    //타일 관련
    [SerializeField]
    private List<Sprite> normalTileList;
    [SerializeField]
    private Sprite spriteWall;
    [SerializeField]
    private Sprite spriteDoor;

    public Sprite GetRandomNormalTile()
    {
        if (normalTileList == null) return null;
        return normalTileList[Random.Range(0, normalTileList.Count)];
    }

    public List<Sprite> GetNormalTileList()
    {
        return normalTileList;
    }

    public Sprite GetWallTile()
    {
        if (spriteWall == null) return null;
        return spriteWall;
    }

    public Sprite GetDoorTile()
    {
        if (spriteDoor == null) return null;
        return spriteDoor;
    }
    #endregion
}
