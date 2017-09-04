using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModuleGenerator : MonoBehaviour
{
    //임시
    [SerializeField]
    private GameObject normalTile;
    [SerializeField]
    private GameObject moduleObject;

    //타일 사이즈
    private float widthDistance = 0.64f;
    private float heightDistance = 0.64f;

    public List<Color> RandomColor;

    private int doorSize = 4;


    //특성별 타일 저장
    private List<Tile> normalTileList;
    private List<Tile> wallTileList;
    private List<Tile> doorTileList;

    //타일 이미지 풀
    private List<Sprite> tileSpriteList;

    //
    private int mapModuleNum = 10;

    private void ResetLists()
    {
        //새로 할당해줘야 중복안됨
        normalTileList = new List<Tile>();
        wallTileList = new List<Tile>();
        doorTileList = new List<Tile>();
    }

    private void Awake()
    {
        normalTileList = new List<Tile>();
        wallTileList = new List<Tile>();
        doorTileList = new List<Tile>();
        tileSpriteList = new List<Sprite>();

        LoadTileSprites();

    }

    private void LoadTileSprites()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Tiles");
        if (sprites == null) return;
        if (tileSpriteList != null)
        {
            for(int i = 0; i < sprites.Length; i++)
            {
                tileSpriteList.Add(sprites[i]);
            }
        }
    }

    private Sprite GetRandomTileSprite()
    {
        if (tileSpriteList == null) return null;
        if (tileSpriteList.Count == 0) return null;

        return tileSpriteList[Random.Range(0, tileSpriteList.Count)];
    }

    private void Start()
    {
        MakeMap(50);
    }




    private void MakeMap(int RoomNum)
    {
        //센터
        GenerateBaseMap(10, 10, Vector3.zero, true);

        for (int i = 0; i < RoomNum; i++)
        {
            GenerateBaseMap(Random.Range(10,20), Random.Range(10, 20), new Vector3(Random.Range(1f, mapModuleNum*2), Random.Range(1f, 5f), 0));
        }
    }

    private void GenerateBaseMap(int widthNum, int heightNum, Vector3 modulePosit,bool isStartModule =false)
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
        GameObject obj = Instantiate(moduleObject, modulePosit, Quaternion.identity, this.transform);
        MapModule module = null;
        if (obj != null)
            module = obj.GetComponent<MapModule>();

        if (module == null) return;
        module.Initialize(widthNum, heightNum, widthDistance, heightDistance, isStartModule);

        #endregion

        #region TileMaking
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
                        tile = MakeTile(TileType.Door, posit, module.transform, module);
                        tile.OpenDoor();             
                    }
                    //아래쪽문
                    else if (x >= widthNum / 2 - doorSize / 2 && x < widthNum / 2 + doorSize / 2 && y == 0)
                    {
                        tile = MakeTile(TileType.Door, posit, module.transform, module);
                        tile.OpenDoor();
                    }
                    //왼쪽문
                    else if (y >= heightNum / 2 - doorSize / 2 && y < heightNum / 2 + doorSize / 2 && x == 0)
                    {
                        tile = MakeTile(TileType.Door, posit, module.transform, module);
                        tile.OpenDoor();
                    }
                    //오쪽문
                    else if (y >= heightNum / 2 - doorSize / 2 && y < heightNum / 2 + doorSize / 2 && x == widthNum - 1)
                    {
                        tile = MakeTile(TileType.Door, posit, module.transform, module);
                        tile.OpenDoor();
                    }
                    //일반벽
                    else
                    {
                        tile = MakeTile(TileType.Wall, posit, module.transform);
                        SetTileColor(tile, Color.black);                     
                    }            


                    // 벽중에서도 도어 리스트

                }
                //노말타일
                else
                {
                    Tile tile = MakeTile(TileType.Normal, posit, module.transform);
                    SetTileColor(tile, RandomColor[Random.Range(0, RandomColor.Count)]);

               
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

    private Tile MakeTile(TileType type, Vector3 posit, Transform parent, MapModule parentModule =null)
    {
        GameObject obj = Instantiate(normalTile, parent);
        obj.transform.position = posit;
        if (obj == null) return null;

        Tile tile = null;
        tile = obj.GetComponent<Tile>();
        if (tile == null) return null;
        tile.Initialize(type, GetRandomTileSprite(), parentModule);
        
        switch (type)
        {
            case TileType.Normal:
                {
                    if(normalTileList!=null)
                    normalTileList.Add(tile);
                } break;
            case TileType.Wall:
                {
                    if (wallTileList != null)
                        wallTileList.Add(tile);
                } break;
            case TileType.Door:
                {
                    if (doorTileList != null)
                        doorTileList.Add(tile);
                } break;
        }

        return tile;


    }



}






