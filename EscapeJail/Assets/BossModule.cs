using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModule : MapModuleBase
{
  

    private void Awake()
    {
        normalTileList = new List<Tile>();

        Tile[] tiles = GetComponentsInChildren<Tile>();
        for(int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileType == TileType.Normal)
                normalTileList.Add(tiles[i]);
            else if (tiles[i].tileType == TileType.Wall)
                tiles[i].transform.gameObject.AddComponent<BoxCollider2D>();
                
        }
    }
}
