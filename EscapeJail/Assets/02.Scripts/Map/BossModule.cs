﻿using System.Collections;
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
    
    private BossBase bossBase; 
    private bool isBossStart = false;
    [SerializeField]
    private GameObject maskObject;

    private Portal myPortal;

    private void SetMaskSize(Vector3 position, Vector3 scale)
    {
        if (maskObject == null) return;
        maskObject.transform.localPosition = position;
        maskObject.transform.localScale = scale;
    }

    private void MaskOff()
    {
        if (maskObject == null) return;
        maskObject.SetActive(false);
    }

    private void Awake()
    {
        GetBossData();

        boxcollider2D = GetComponent<BoxCollider2D>();

        SetColliderSizeBig();

        moduleType = MapModuleType.BossModule;
    }

    public void HidePortal()
    {
        //포탈
        if(myPortal==null)
        myPortal = GetComponentInChildren<Portal>();
        if (myPortal != null)
        {
            myPortal.gameObject.SetActive(false);
            myPortal.SetBossPortal();
        }
    }
    public void WhenBossDie()
    {
        //포탈
        if (myPortal == null)
            myPortal = GetComponentInChildren<Portal>();

        if (myPortal != null)
        {
            myPortal.gameObject.SetActive(true);
            myPortal.WhenBossDie();
        }

    }

    private void SetColliderSizeBig()
    {
        widthDistance = GameConstants.tileSize;
        heightDistance = GameConstants.tileSize;

        if (boxcollider2D != null)
        {
            boxcollider2D.size = new Vector2((widthNum + 2) * widthDistance, (heightNum + 2) * heightDistance);
            boxcollider2D.offset = new Vector2(0f,0f);
            SetMaskSize(boxcollider2D.offset, new Vector2((widthNum - 2) * widthDistance, (heightNum - 2) * heightDistance));
        }
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

        OpenDoor();
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
        base.PositioningComplete();

        if (boxcollider2D != null)
            boxcollider2D.size = new Vector2((widthNum - 3) * widthDistance, (heightNum - 3) * heightDistance) - Vector2.one * 0.2f;


        minimap_Icon = MiniMap.Instance.MakeRoomIcon(this.transform.localPosition, new Vector3(widthNum * GameConstants.tileSize, heightNum * GameConstants.tileSize, 1f),true,this);
               
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {        

        if (isBossStart == true|| isPositioningComplete==false) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            BossIntroduceWindow.Instance.ShowImage(bossBase.StartBossPattern);
            //if (bossBase != null)
            //    bossBase.StartBossPattern();

            isBossStart = true;
      
            CloseDoor();
            HidePortal();
            MaskOff();
        }
    }     

  
}

