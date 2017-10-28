using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Lock,
    UnLock,
}


public class MapModule : MapModuleBase
{


    private List<MonsterBase> monsterList = new List<MonsterBase>();
    [SerializeField]
    private GameObject maskObject;

    //속성
    private int SpawnMonsterNum = 0;
    private int widthNum;
    private int heightNum;

    private bool isPositioningComplete = false;

    public void OnDisable()
    {
        monsterList = null;
    }


    void Awake()
    {
        boxcollider2D = GetComponent<BoxCollider2D>();
    }

    private void SetMakeSize(Vector3 position, Vector3 scale)
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

    public void Initialize(int widthNum, int heightNum, float widthDistance, float heightDistance, bool isStartModule, MapManager mapManager)
    {
        this.widthNum = widthNum;
        this.heightNum = heightNum;
        this.widthDistance = widthDistance;
        this.heightDistance = heightDistance;
        this.isStartModule = isStartModule;
        if (isStartModule == true)
        {
            MaskOff();
        }

        if (boxcollider2D != null)
        {
            boxcollider2D.size = new Vector2((widthNum + 2) * widthDistance, (heightNum + 2) * heightDistance);
            boxcollider2D.offset = new Vector2(-widthDistance / 2, -heightDistance / 2);
            SetMakeSize(boxcollider2D.offset, new Vector2((widthNum - 2) * widthDistance, (heightNum - 2) * heightDistance));

        }

        this.mapManager = mapManager;

        if (mapManager != null)
            mapManager.AddToModuleList(this);
    }

    private bool IsWaveEnd()
    {
        if (monsterList == null) return true;

        for (int i = 0; i < monsterList.Count; i++)
        {
            //살아있는적이 하나라도 있으면 
            if (monsterList[i].gameObject.activeSelf == true)
                return false;
        }
        return true;
    }

    public void LinkTile(List<Tile> normalTileList, List<Tile> wallTileList, List<Tile> doorTileList)
    {
        this.normalTileList = normalTileList;
        this.wallTileList = wallTileList;
        this.doorTileList = doorTileList;

    }



    public void StartWave()
    {
        //중복진입 방지
        if (mapState == MapState.Lock || isClear == true) return;

        MaskOff();
        mapState = MapState.Lock;
        StartCoroutine(SpawnRandomMonsterRoutine());
        CloseDoor();
    }

    public void EndWave()
    {
        mapState = MapState.UnLock;

        //사용한 리소스 정리
        if (monsterList != null)
        {
            monsterList.Clear();
            monsterList = null;
        }
        isClear = true;
        OpenDoor();
    }

    public void AddtoMonsterList(MonsterBase monster)
    {
        if (monsterList == null) return;
        monsterList.Add(monster);

    }

    public MonsterBase SpawnRandomMonsterInModule(Vector3 spawnPos)
    {
        MonsterBase spawnMonster = MonsterManager.Instance.SpawnRandomMonster(spawnPos);

        if (spawnMonster != null && monsterList != null)
        {
            spawnMonster.SetMapModule(this);
            AddtoMonsterList(spawnMonster);
        }

        return spawnMonster;
    }

    public MonsterBase SpawnSpecificMonsterInModule(MonsterName name,Vector3 spawnPos)
    {
        MonsterBase spawnMonster = MonsterManager.Instance.SpawnSpecificMonster(name,spawnPos);

        if (spawnMonster != null && monsterList != null)
        {
            spawnMonster.SetMapModule(this);
            AddtoMonsterList(spawnMonster);
        }

        return spawnMonster;
    }

    IEnumerator SpawnRandomMonsterRoutine()
    {
        if (normalTileList == null) yield break;

        SpawnMonsterNum = (widthNum * heightNum) /30;

        //몬스터 스폰
        for (int i = 0; i < SpawnMonsterNum; i++)
        {
            int RandomIndex = Random.Range(0, normalTileList.Count - 1);
            Vector3 RandomSpawnPosit = normalTileList[RandomIndex].transform.position;

            SpawnRandomMonsterInModule(RandomSpawnPosit);

             yield return new WaitForSeconds(2f);
        }


        //종료 체크
        while (true)
        {
            if (IsWaveEnd() == true)
            {
                EndWave();
                yield break;
            }
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (mapState == MapState.UnLock         //문이 열려있어야함
                && isClear == false                 //클리어된 모듈이 아니어야함
                && isStartModule == false           //처음 생성모듈이 아니어야함
                && isPositioningComplete == true)   //배치가 끝난 모듈이어야 함
            {
                Debug.Log("진입");
                StartWave();
            }
        }
    }

    public void PositioningComplete()
    {
        isPositioningComplete = true;


        if (boxcollider2D != null)
            boxcollider2D.size = new Vector2((widthNum - 3) * widthDistance, (heightNum - 3) * heightDistance) - Vector2.one * 0.2f;

    }

    public void MakeObjects()
    {
        //계수가 생성확률
        float makeNum = (float)(widthNum * heightNum) * 0.015f;

        for (int i = 0; i < (int)makeNum; i++)
        {
            MakeEachObject();
        }

    }

    private void MakeEachObject()
    {
        if (isPositioningComplete == false) return;
        if (normalTileList == null) return;
        if (normalTileList.Count == 0) return;
        if (mapManager == null) return;


        GameObject obj = mapManager.GetRandomObject();

        if (obj == null) return;

        for (int i = 0; i < 10; i++)
        {
            Tile targetTile = normalTileList[Random.Range(0, normalTileList.Count)];

            //제일 외각은 생성 X
            if (
               targetTile.x == 1 ||
               targetTile.x == widthNum - 2 ||
               targetTile.y == 1 ||
               targetTile.y == heightNum - 2
               )
                continue;

            //중복생성 체크
            if (targetTile.canSpawned == false)
            {
                continue;
            }
            else
            {
                targetTile.canSpawned = false;
                GameObject article = GameObject.Instantiate(obj, targetTile.transform);

                return;
            }
        }



    }




}
