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
    private int spawnMonsterNum = 0;
    private MiniMap_MapIcon minimap_Icon;



    private int nowWaveNum = 0;
    private int waveNum = 0;

    //몬스터 랜덤 생성
    private List<MonsterName> spawnableMonsterList = new List<MonsterName>();
    private RandomGenerator<MonsterName> randomGenerator = new RandomGenerator<MonsterName>();

    public void OnDisable()
    {
        monsterList = null;
    }


    void Awake()
    {
        boxcollider2D = GetComponent<BoxCollider2D>();
        moduleType = MapModuleType.NormalModule;
    }

    private void Start()
    {

        SetSpawnableMonsterList();
    }

    private void SetSpawnableMonsterList()
    {
        List<MonsterName> monsterList = ObjectManager.Instance.monsterPool.GetMonsterList();

        if (monsterList == null) return;

        monsterList.ListShuffle(30);


        int spawnMonsterTypeNum = 0;
        if (monsterList.Count > 1)
            spawnMonsterTypeNum = Random.Range(2, monsterList.Count);
        else
            spawnMonsterTypeNum = Random.Range(1, monsterList.Count);
        for (int i = 0; i < spawnMonsterTypeNum; i++)
        {
            spawnableMonsterList.Add(monsterList[i]);
        }

        monsterList.Clear();
        monsterList = null;


        //랜덤
        if (randomGenerator != null)
        {
            for (int i = 0; i < spawnableMonsterList.Count; i++)
            {
                MonsterDB monsterData = DatabaseLoader.Instance.GetMonsterData(spawnableMonsterList[i]);

                if (monsterData != null)
                    randomGenerator.AddToList(spawnableMonsterList[i], monsterData.Probability);
            }

        }
    }

    public MonsterName GetRandomMonster()
    {
        MonsterName RandomKey = randomGenerator.GetRandomData();
        return RandomKey;
    }

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
            EndWave();
        }

        if (boxcollider2D != null)
        {
            boxcollider2D.size = new Vector2((widthNum + 2) * widthDistance, (heightNum + 2) * heightDistance);
            boxcollider2D.offset = new Vector2(-widthDistance / 2, -heightDistance / 2);
            SetMaskSize(boxcollider2D.offset, new Vector2((widthNum - 2) * widthDistance, (heightNum - 2) * heightDistance));

        }

        this.mapManager = mapManager;

        if (mapManager != null)
            mapManager.AddToModuleList(this);

       
    }

    private bool IsWaveEnd()
    {
        if (monsterList == null) return true;
        if (monsterList.Count == 0) return false;

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
        for (int i = 0; i < spawnableMonsterList.Count; i++)
        {
            Debug.Log(spawnableMonsterList[i].ToString());
        }

        //이동속도 정상화
        GamePlayerManager.Instance.player.SetBurstSpeed(false);


        //중복진입 방지
        if (mapState == MapState.Lock || isClear == true) return;

        waveNum = Random.Range(1, 4);
        nowWaveNum = waveNum;



        MaskOff();
        mapState = MapState.Lock;
        StartCoroutine(SpawnRandomMonsterRoutine());
        CloseDoor();
    }

    public void EndWave()
    {
        //이동속도 빠르게      
        GamePlayerManager.Instance.player.SetBurstSpeed(true);

        mapState = MapState.UnLock;

        //사용한 리소스 정리
        if (monsterList != null)
        {
            monsterList.Clear();
            monsterList = null;
        }
        isClear = true;
        OpenDoor();

        if (minimap_Icon != null)
            minimap_Icon.SetClear();

        //메달 생성
        if (isStartModule != true)
        {
            DropGoods medal = ObjectManager.Instance.coinPool.GetItem();
            medal.Initiatlize(this.transform.position, 1, GoodsType.Medal);
        }
  

    }

    public void AddtoMonsterList(MonsterBase monster)
    {
        if (monsterList == null) return;
        monsterList.Add(monster);

    }

    public void SpawnRandomMonsterInModule(Vector3 spawnPos)
    {
        MonsterBase spawnMonster = MonsterManager.Instance.SpawnSpecificMonster(GetRandomMonster(), spawnPos);

        if (spawnMonster != null && monsterList != null)
        {
            spawnMonster.SetMapModule(this);
            AddtoMonsterList(spawnMonster);
        }
    }

    //슬라임용
    public MonsterBase SpawnSpecificMonsterInModule(MonsterName name, Vector3 spawnPos)
    {
        MonsterBase spawnMonster = MonsterManager.Instance.SpawnSpecificMonster(name, spawnPos);

        if (spawnMonster != null && monsterList != null)
        {
            spawnMonster.SetMapModule(this);
            AddtoMonsterList(spawnMonster);
        }

        return spawnMonster;
    }

    private void SpawnMonster(int monsterNum)
    {
        if (monsterList != null)
            monsterList.Clear();

        //몬스터 스폰
        for (int i = 0; i < monsterNum; i++)
        {
            int RandomIndex = Random.Range(0, normalTileList.Count - 1);
            Vector3 RandomSpawnPosit = normalTileList[RandomIndex].transform.position;

            MonsterSpawnEffect spawnEffect =ObjectManager.Instance.monsterSpawnEffectPool.GetItem();
            spawnEffect.Initialize(RandomSpawnPosit, SpawnRandomMonsterInModule);

        //    SpawnRandomMonsterInModule(RandomSpawnPosit);

        }


    }

    IEnumerator SpawnRandomMonsterRoutine()
    {
        yield return new WaitForSeconds(1f);

        if (normalTileList == null) yield break;

        spawnMonsterNum = (widthNum + heightNum) / 7;

        SpawnMonster(Random.Range(spawnMonsterNum, spawnMonsterNum + 1));


     

        //종료 체크
        while (true)
        {
            if (IsWaveEnd() == true && nowWaveNum > 0)
            {
                nowWaveNum -= 1;

                if (nowWaveNum == 0)
                {
                    EndWave();
                    yield break;
                }

                SpawnMonster(Random.Range(spawnMonsterNum, spawnMonsterNum + 1));
                

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

    public override void PositioningComplete()
    {
        base.PositioningComplete();

        if (boxcollider2D != null)
            boxcollider2D.size = new Vector2((widthNum - 3) * widthDistance, (heightNum - 3) * heightDistance) - Vector2.one * 0.2f;

       minimap_Icon = MiniMap.Instance.MakeRoomIcon(this.transform.localPosition, new Vector3(widthNum * GameConstants.tileSize, heightNum * GameConstants.tileSize, 1f),hasPortal,this);
        if (isStartModule == true)
            minimap_Icon.SetClear();
    }

    public override void MakeObjects()
    {
        //계수가 생성확률
        float makeNum = (float)(widthNum * heightNum) * 0.005f;

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


        GameObject obj = mapManager.GetRandomArticle();

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
                article.transform.localPosition = Vector3.zero;
                return;
            }
        }

    }




}
