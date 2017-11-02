using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//보스방 만들때 해줘야 할것
//타일 타입 정확히 맞춰주기 (wall인지 normal인지 door인지)
//레이어 순서 정확히 맞춰줄것


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
                tiles[i].Initialize(TileType.Wall,this);

            }
            else if (tiles[i].tileType == TileType.Door)
            {
                doorTileList.Add(tiles[i]);
                tiles[i].Initialize(TileType.Door, this);
            }
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

