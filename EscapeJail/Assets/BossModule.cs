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

    private void Awake()
    {
        normalTileList = new List<Tile>();
        doorTileList = new List<Tile>();

        Tile[] tiles = GetComponentsInChildren<Tile>();
        for(int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileType == TileType.Normal)
                normalTileList.Add(tiles[i]);
            else if (tiles[i].tileType == TileType.Wall)
                tiles[i].transform.gameObject.AddComponent<BoxCollider2D>();
            else if (tiles[i].tileType == TileType.Door)
                doorTileList.Add(tiles[i]);
        }

        bossBase = GetComponentInChildren<BossBase>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (bossBase != null)
                bossBase.StartBossPattern();

            CloseDoor();
        }
    }     

  
}

