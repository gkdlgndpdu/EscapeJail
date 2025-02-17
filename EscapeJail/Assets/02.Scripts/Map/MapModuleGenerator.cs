﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModuleGenerator
{
    private MapManager mapManager;

    private Transform moduleParent;

    private GameObject normalTile;
    private GameObject moduleObject;

    //타일 사이즈
    private float widthDistance = 0.64f;
    private float heightDistance = 0.64f;

    private int doorSize = 4;


    //특성별 타일 저장
    private List<Tile> normalTileList;
    private List<Tile> wallTileList;
    private List<Tile> doorTileList;
    private List<Tile> bossMakableList;

    //타일 이미지 풀
    private List<Sprite> normalSpriteList;
    private List<Sprite> wallSpriteList;
    private Sprite doorSprite;

    private List<Tile> backGroundTileList = new List<Tile>();

    ////모듈 리스트
    //private List<MapModuleBase> moduleList;


    //벽만들때 필요
    //현재 모듈들의 모서리 벽부분(보스룸 생성에 정보가 필요함)
    public List<Tile> everyWallList;

    //포탈 만들때 필요
    //현재 생성된 모듈들중 맨끝쪽에 있는애들
    public List<Tile> portalMakableList = new List<Tile>();
    public Tile centerTile;

    public MapModuleGenerator(Transform moduleParent, MapManager mapManager)
    {
        normalTile = Resources.Load<GameObject>("Prefabs/Maps/MapObjects/tile");
        moduleObject = Resources.Load<GameObject>("Prefabs/Maps/MapObjects/MapModule");
        this.moduleParent = moduleParent;
        this.mapManager = mapManager;

        Initialize();
    }

    private void ResetLists()
    {
        //새로 할당해줘야 중복안됨
        normalTileList = new List<Tile>();
        wallTileList = new List<Tile>();
        doorTileList = new List<Tile>();
    }

    private void Initialize()
    {
        //tileSpriteList = new List<Sprite>();
        //wallSpriteList = new List<Sprite>();
        everyWallList = new List<Tile>();
        bossMakableList = new List<Tile>();



        LoadTileSprites();

    }

    public void MakeWall(Transform parent = null)
    {
        if (everyWallList == null) return;

        ResetLists();

        //X최대 최소
        everyWallList.Sort((a, b) =>
        {
            return (a.transform.position.x.CompareTo(b.transform.position.x));
        });

        float minX = everyWallList[0].transform.position.x;
        float maxX = everyWallList[everyWallList.Count - 1].transform.position.x;

        portalMakableList.Add(everyWallList[0]);
        portalMakableList.Add(everyWallList[everyWallList.Count - 1]);

        //Y최대 최소
        everyWallList.Sort((a, b) =>
        {
            return (a.transform.position.y.CompareTo(b.transform.position.y));
        });

        float minY = everyWallList[0].transform.position.y;
        float maxY = everyWallList[everyWallList.Count - 1].transform.position.y;

        portalMakableList.Add(everyWallList[0]);
        portalMakableList.Add(everyWallList[everyWallList.Count - 1]);

        float width = maxX - minX;
        float height = maxY - minY;
#if UNITY_EDITOR
        Debug.Log("width = " + width.ToString() + " height = " + height.ToString());
#endif
        float ratio = 0f;
        if (width > height)
        {
            ratio = width / 40f * 0.01f;
        }
        else
        {
            ratio = height / 40f * 0.01f;
        }
      
        MiniMap.Instance.SetRealRatiofloat(0.09f - ratio);


        int num1 = Mathf.RoundToInt(width / 0.64f);
        int num2 = Mathf.RoundToInt(height / 0.64f);

        float offSetX = minX + width / 2f;
        float offSetY = minY + height / 2f;

        int widthNum = (int)(width / widthDistance) + 8;
        int heightNum = (int)(height / heightDistance) + 8;

        Debug.Log("width = " + widthNum.ToString() + " height = "+ heightNum.ToString());

        MiniMap.Instance.SetBackGroundScale(new Vector3((float)widthNum * widthDistance, (float)heightNum * heightDistance, 1f));
      

        //교정
        if (num1 % 2 != 0) offSetX += 0.32f;
        if (num2 % 2 != 0) offSetY += 0.32f;


        Debug.Log("num1 = " + num1.ToString() + " num2 = " + num2.ToString());

        MiniMap.Instance.SetBackGroundPosit(new Vector3(offSetX, offSetY, 1f));

        //랜덤벽스프라이트
        Sprite randomWallSprite = GetRandomWallTileList();


        for (int x = 0; x < widthNum; x++)
        {
            for (int y = 0; y < heightNum; y++)
            {
                Vector3 posit = new Vector3((float)(-widthNum / 2 + x) * widthDistance + offSetX,
                               (float)(-heightNum / 2 + y) * heightDistance + offSetY,
                               0f);


                if (y == 0 || y == heightNum - 1 || x == 0 || x == widthNum - 1)
                {

                    //일반벽   
                    Tile tile;
                    tile = MakeTile(TileType.Wall, posit, x, y, parent, null, -1, randomWallSprite);
                    SetTileColor(tile, Color.white);

                    //위쪽문
                    if (x >= widthNum / 2 && y == heightNum - 1)
                    {
                        bossMakableList.Add(tile);
                    }
                    ////아래쪽문
                    //else if (x >= widthNum / 2  / 2 && x < widthNum / 2  / 2 && y == 0)
                    //{

                    //}
                    ////왼쪽문
                    //else if (y >= heightNum / 2 / 2 && y < heightNum / 2  / 2 && x == 0)
                    //{

                    //}
                    ////오쪽문
                    //else if (y >= heightNum / 2 - doorSize / 2 && y < heightNum / 2 + doorSize / 2 && x == widthNum - 1)
                    //{

                    //}    

                    if (backGroundTileList != null)
                        backGroundTileList.Add(tile);
                }
                else
                {
              
                    //Tile tile;
                    //tile = MakeTile(TileType.Normal, posit, x, y, parent, null, GameConstants.BackgroundLayerMin);

                    //SetTileColor(tile, Color.white);
                    //if (backGroundTileList != null)
                    //    backGroundTileList.Add(tile);
                }
            }
        }

        if (widthNum % 2 == 0) offSetX -= 0.32f;
        if (heightNum % 2 == 0) offSetY -= 0.32f;

        BackGroundTile.Instance.Initialize(new Vector3(offSetX, offSetY, 1f), new Vector2((widthNum - 2) * GameConstants.tileSize, (heightNum - 2) * GameConstants.tileSize), new Vector3(widthNum - 2, heightNum - 2, 0f));


    }
    /// <summary>
    /// 반드시 벽 생성후 호출
    /// </summary>
    public void MakePortal()
    {
        if (portalMakableList == null) return;
        if (portalMakableList.Count == 0) return;

        for(int i=0;i< portalMakableList.Count; i++)
        {
            if (portalMakableList[i].ParentModule == null) continue;
            portalMakableList[i].ParentModule.MakePortal();
        }

        if(centerTile!=null)
        {
            if (centerTile.ParentModule != null)
                centerTile.ParentModule.MakePortal();
        }

    }

    public void MakeBossModule(Transform moduleParent, Vector3 spawnPosit)
    {
        BossModule bossModuleObject = StagerController.Instance.stageData.bossModule.GetComponent<BossModule>();

        if (bossModuleObject == null) return;

        BossModule module = GameObject.Instantiate(bossModuleObject, spawnPosit, Quaternion.identity, moduleParent);
        module.AddToMapManager(mapManager);
        module.GetMyTiles();

        List<Tile> bossWallList = module.GetWallList();
        for(int i=0;i< bossWallList.Count; i++)
        {
            everyWallList.Add(bossWallList[i]);
        }

        if (module != null)
            module.MakePortal();
        //
        //everyWallList.add

    }

    public void MakeShopModule(Transform moduleParent,Vector3 spawnPosit)
    {
        GameObject shopModuleObj = Resources.Load<GameObject>("Prefabs/Maps/ETC/ShopModule");
        if (shopModuleObj != null)
        {
            ShopModule shopModule = GameObject.Instantiate(shopModuleObj, spawnPosit, Quaternion.identity, moduleParent).GetComponent<ShopModule>();
            shopModule.AddToMapManager(mapManager);
            shopModule.GetMyTiles();

            List<Tile> shopWallList = shopModule.GetWallList();
            for (int i = 0; i < shopWallList.Count; i++)
            {
                everyWallList.Add(shopWallList[i]);
            }

        }

    }




    private void LoadTileSprites()
    {
        StageData nowStageData = StagerController.Instance.stageData;
        if (nowStageData == null) return;

        //일반타일 로드

        normalSpriteList = nowStageData.GetNormalTileList();



        //벽타일 로드
        wallSpriteList = nowStageData.GetWallTileList();



        //문타일 로드
        doorSprite = nowStageData.GetDoorTile();

    }

    private Sprite GetRandomTileSprite()
    {
        if (normalSpriteList == null) return null;
        if (normalSpriteList.Count == 0) return null;

        return normalSpriteList[Random.Range(0, normalSpriteList.Count)];
    }

    private Sprite GetRandomWallTileList()
    {
        if (wallSpriteList == null) return null;
        if (wallSpriteList.Count == 0) return null;

        return wallSpriteList[Random.Range(0, wallSpriteList.Count)];
    }


    public void MakeMap(StageData stageData)
    {
        if (stageData == null) return;

        //센터
        GenerateBaseMap(10, 10, Vector3.zero, true);

        int minsize = stageData.RoomMinSize;
        int maxSize = stageData.RoomMaxSize;

        if (minsize % 2 != 0) minsize += 1;
        if (maxSize % 2 != 0) maxSize += 1;



        int roomNum = Random.Range(stageData.MinRoomNum, stageData.MaxRoomNum + 1);

        for (int i = 0; i < roomNum; i++)
        {
            GenerateBaseMap
                (
                Random.Range(minsize, maxSize),  //x크기
                Random.Range(minsize, maxSize),  //y크기
                new Vector3((float)Random.Range(-14, 14) * 0.64f, (float)Random.Range(-14, 14) * 0.64f, 0) //초기 생성 좌표
                );

        }

        //랜덤위치
        float randX = Random.Range(0, 2) == 0 ? 1f : -1f;
        float randY = Random.Range(0, 2) == 0 ? 1f : -1f;
        Vector3 bossSpawnPosit = new Vector3((30 * 0.64f - 0.32f) * randX, (30 * 0.64f - 0.32f) * randY, 0f);

        //보스모듈
        MakeBossModule(mapManager.transform, bossSpawnPosit);

        //랜덤위치  
        //randX = Random.Range(0, 2) == 0 ? 0.5f : -0.5f;
        //randY = Random.Range(0, 2) == 0 ? 0.5f : -0.5f;        
        randX = Random.Range(0, 2) == 0 ? 1f : -1f;
        randY = Random.Range(0, 2) == 0 ? 1f : -1f;

        //상점    
        Vector3 shopSpawnPosit = new Vector3((30 * 0.64f - 0.32f) * randX, (30 * 0.64f - 0.32f) * randY, 0f);

        if (GamePlayerManager.Instance.PlayerName !=CharacterType.Trader)
        MakeShopModule(mapManager.transform, shopSpawnPosit);

    }

    private void GenerateBaseMap(int widthNum, int heightNum, Vector3 modulePosit, bool isStartModule = false)
    {
        ResetLists();

        #region Exception
        //예외처리

        //짝수로 교정
        if (widthNum % 2 != 0)
            widthNum += 1;

        if (heightNum % 2 != 0)
            heightNum += 1;

        //만들수있는 최소사이즈
        if (widthNum < 4)
            widthNum = 4;

        if (heightNum < 4)
            heightNum = 4;

        //문사이즈는 반드시 짝수
        if (doorSize % 2 != 0)
            doorSize += 1;

        //문 사이즈가 벽보다 크다
        if (doorSize >= widthNum || doorSize >= heightNum)
            doorSize -= 2;
        #endregion

        #region MakeModule
        //모듈 생성
        GameObject obj = GameObject.Instantiate(moduleObject, modulePosit, Quaternion.identity, moduleParent);
        MapModule module = null;
        if (obj != null)
            module = obj.GetComponent<MapModule>();

        if (module == null) return;
        module.Initialize(widthNum, heightNum, widthDistance, heightDistance, isStartModule, mapManager);


        #endregion

        #region TileMaking

        Sprite randomWallSprite = GetRandomWallTileList();

        //타일,벽 생성
        for (int x = 0; x < widthNum; x++)
        {
            for (int y = 0; y < heightNum; y++)
            {
                Vector3 posit = new Vector3((float)(-widthNum / 2 + x) * widthDistance,
                                            (float)(-heightNum / 2 + y) * heightDistance, 0f);
                posit += modulePosit;

                //벽 (네방향 테두리부분)
                if (y == 0 || y == heightNum - 1 || x == 0 || x == widthNum - 1)
                {
                    Tile tile;

                    //위쪽문
                    if (x >= widthNum / 2 - doorSize / 2 && x < widthNum / 2 + doorSize / 2 && y == heightNum - 1)
                    {
                        tile = MakeTile(TileType.Door, posit, x, y, module.transform, module);
                    }
                    //아래쪽문
                    else if (x >= widthNum / 2 - doorSize / 2 && x < widthNum / 2 + doorSize / 2 && y == 0)
                    {
                        tile = MakeTile(TileType.Door, posit, x, y, module.transform, module);
                    }
                    //왼쪽문
                    else if (y >= heightNum / 2 - doorSize / 2 && y < heightNum / 2 + doorSize / 2 && x == 0)
                    {
                        tile = MakeTile(TileType.Door, posit, x, y, module.transform, module);
                    }
                    //오쪽문
                    else if (y >= heightNum / 2 - doorSize / 2 && y < heightNum / 2 + doorSize / 2 && x == widthNum - 1)
                    {
                        tile = MakeTile(TileType.Door, posit, x, y, module.transform, module);
                    }
                    //일반벽
                    else
                    {
                        tile = MakeTile(TileType.Wall, posit, x, y, module.transform, module, 0, randomWallSprite);



                        //전체 외각 계산을 위해 필요

                        //LeftTop
                        if (x == 0 && y == heightNum - 1)
                            everyWallList.Add(tile);
                        //RightTop
                        else if (x == widthNum - 1 && y == heightNum - 1)
                            everyWallList.Add(tile);
                        //LeftBottom
                        else if (x == 0 && y == 0)
                            everyWallList.Add(tile);
                        //RightBottom
                        else if (x == widthNum - 1 && y == 0)
                            everyWallList.Add(tile);


                        SetTileColor(tile, Color.white);
                    }
                }
                //노말타일
                else
                {
                    Tile tile = MakeTile(TileType.Normal, posit, x, y, module.transform, module);
                    //SetTileColor(tile, StagerController.Instance.stageData.GetRandomTileColor());
                    if (isStartModule == true)
                        centerTile = tile;
                }


            }
        }
        #endregion

        #region LinkModule
        module.LinkTile(normalTileList, wallTileList, doorTileList);
        #endregion

    }
    private void SetTileColor(Tile tile, Color color)
    {
        if (tile == null) return;
        tile.ChangeColor(color);

    }
    //벽의경우 여기에 넣어주면 해당 텍스쳐로만 생성
    private Tile MakeTile(TileType type, Vector3 posit, int x, int y, Transform parent, MapModule parentModule, int layerOrder = 0, Sprite specificSprite = null)
    {
        //GameObject obj = GameObject.Instantiate(normalTile, parent);
        //obj.transform.position = posit;
        //if (obj == null) return null;
        //Tile tile = null;
        //tile = obj.GetComponent<Tile>();
        //if (tile == null) return null;

        Tile tile = ObjectManager.Instance.tilePool.GetItem();
        tile.transform.position = posit;
        if(parentModule!=null)
        tile.transform.parent = parentModule.transform;

        if (tile == null) return null;
        switch (type)
        {
            case TileType.Normal:
                {
                    if (normalTileList != null)
                        normalTileList.Add(tile);

                    if (normalSpriteList != null)
                        tile.SetSprite(GetRandomTileSprite());
                }
                break;
            case TileType.Wall:
                {

                    if (wallTileList != null)
                        wallTileList.Add(tile);

                    if (specificSprite == null)
                    {
                        if (wallSpriteList != null)
                            tile.SetSprite(GetRandomWallTileList());
                    }
                    else
                    {
                        tile.SetSprite(specificSprite);
                    }



                    tile.gameObject.name = "Wall";
                }
                break;
            case TileType.Door:
                {
                    if (doorTileList != null)
                        doorTileList.Add(tile);

                    if (normalSpriteList != null)
                        tile.SetSprite(GetRandomTileSprite());
                }
                break;
        }

        tile.Initialize(type, parentModule, layerOrder);
        tile.SetIndex(x, y);

        return tile;


    }

    public void PullBackGroundTiles()
    {
        if (backGroundTileList == null) return;
        for(int i = 0; i < backGroundTileList.Count; i++)
        {
            backGroundTileList[i].PullToParentPool();
        }

    }

}






