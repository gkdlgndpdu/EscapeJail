using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorDirection
{
    Default,
    Up,
    Down,
    Left,
    Right
}


public class BossModule : MapModuleBase
{
    public DoorDirection doorDirection;
    
    public BossBase bossBase;
    public int widthNum;
    public int heightNum;
    private bool isBossStart = false;
    private void Awake()
    {
        GetBossData();



    }

    public void GetMyTiles()
    {
        normalTileList = new List<Tile>();
        doorTileList = new List<Tile>();
        wallTileList = new List<Tile>();

        Tile[] tiles = GetComponentsInChildren<Tile>();
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileType == TileType.Normal)
                normalTileList.Add(tiles[i]);
            else if (tiles[i].tileType == TileType.Wall)
            {
                wallTileList.Add(tiles[i]);
                tiles[i].transform.gameObject.AddComponent<BoxCollider2D>();

            }
            else if (tiles[i].tileType == TileType.Door)
                doorTileList.Add(tiles[i]);
        }
    }

    public List<Tile> GetWallList()
    {
        return wallTileList;
    }

    private void GetBossData()
    {
        bossBase = GetComponentInChildren<BossBase>();
    }

    public void AddToMapManager(MapManager manager)
    {
        manager.AddToModuleList(this);
    }

    public override void PositioningComplete()
    {
        isPositioningComplete = true;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (isBossStart == true|| isPositioningComplete==false) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (bossBase != null)
                bossBase.StartBossPattern();

            isBossStart = true;

            CloseDoor();
        }
    }     

  
}

