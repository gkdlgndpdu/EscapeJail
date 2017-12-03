using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModule : MapModuleBase
{
    public List<Tile> GetWallList()
    {
        return wallTileList;
    }

    public void AddToMapManager(MapManager manager)
    {
        manager.AddToModuleList(this);
    }

    private void Awake()
    {     

        boxcollider2D = GetComponent<BoxCollider2D>();
        moduleType = MapModuleType.ShopModule;


    }

    public override void PositioningComplete()
    {
        base.PositioningComplete();

        //if (boxcollider2D != null)
        //    boxcollider2D.size = new Vector2((widthNum - 3) * widthDistance, (heightNum - 3) * heightDistance) - Vector2.one * 0.2f;


        MiniMap.Instance.MakeRoomIcon(this.transform.localPosition, new Vector3(widthNum * GameConstants.tileSize, heightNum * GameConstants.tileSize, 1f));

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
                tiles[i].Initialize(TileType.Wall, this);

            }
            else if (tiles[i].tileType == TileType.Door)
            {
                doorTileList.Add(tiles[i]);
                tiles[i].Initialize(TileType.Door, this);
            }
        }
  
    }


}
