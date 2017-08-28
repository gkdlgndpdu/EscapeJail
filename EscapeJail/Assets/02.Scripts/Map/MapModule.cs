using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Lock,
    UnLock,
}

[RequireComponent(typeof(BoxCollider2D))]
public class MapModule : MonoBehaviour
{
    //상태
    private MapState mapState = MapState.UnLock;
    private bool isClear = false;

    //저장소
    public List<Tile> normalTileList;
    public List<Tile> wallTileList;
    public List<Tile> doorTileList;
    private List<MonsterBase> monsterList = new List<MonsterBase>();

    //속성
    private int SpawnMonsterNum = 1;
    private int widthNum;
    private int heightNum;
    private float widthDistance;
    private float heightDistance;
    private bool isStartModule = false;
    private bool isPositioningComplete = false;
    private float eachModuleDistance = 2f;

    //컴포넌트
    public BoxCollider2D boxcollider2D;

    void Awake()
    {
        boxcollider2D = GetComponent<BoxCollider2D>();
    }

    public void Initialize(int widthNum, int heightNum, float widthDistance, float heightDistance, bool isStartModule)
    {
        this.widthNum = widthNum;
        this.heightNum = heightNum;
        this.widthDistance = widthDistance;
        this.heightDistance = heightDistance;
        this.isStartModule = isStartModule;

        if (boxcollider2D != null)
        {
            boxcollider2D.size = new Vector2((widthNum+2) * widthDistance, (heightNum+2) * heightDistance) - Vector2.one * 0.1f;
            boxcollider2D.offset = new Vector2(-widthDistance / 2, -heightDistance / 2);
        }

       MapManager.Instance.AddToModuleList(this);
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

    private void OpenDoor()
    {
        if (doorTileList == null) return;
        for (int i = 0; i < doorTileList.Count; i++)
        {
            doorTileList[i].OpenDoor();
        }
    }

    private void CloseDoor()
    {
        if (doorTileList == null) return;
        for (int i = 0; i < doorTileList.Count; i++)
        {
            doorTileList[i].CloseDoor();
        }
    }

    public void StartWave()
    {
        //중복진입 방지
        if (mapState == MapState.Lock || isClear == true) return;

        mapState = MapState.Lock;
        StartCoroutine(SpawnMonster());
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

    IEnumerator SpawnMonster()
    {
        if (normalTileList == null) yield break;


        //몬스터 스폰
        for (int i = 0; i < SpawnMonsterNum; i++)
        {
            int RandomIndex = Random.Range(0, normalTileList.Count - 1);
            Vector3 RandomSpawnPosit = normalTileList[RandomIndex].transform.position;
            MonsterBase spawnMonster = MonsterManager.Instance.SpawnMonster(MonsterName.Mouse1, RandomSpawnPosit);

            if (spawnMonster != null && monsterList != null)
                monsterList.Add(spawnMonster);

            yield return new WaitForSeconds(0.5f);
        }


        //종료 체크
        while (true)
        {
            if (IsWaveEnd() == true)
            {
                EndWave();
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
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

        if (boxcollider2D != null) ;
        boxcollider2D.size = new Vector2((widthNum - 2) * widthDistance, (heightNum - 2) * heightDistance) - Vector2.one * 0.1f;

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (isStartModule == true) return;
        if (collision.CompareTag("MapModule"))
        {
            MapModule anotherModule = collision.gameObject.GetComponent<MapModule>();
            if (anotherModule != null)
            {
                MapManager.Instance.ResetMakeCount();
            
                if (this.transform.position.x < collision.bounds.center.x)
                {
                    this.transform.position -= Vector3.right* eachModuleDistance;
                }
                else if (this.transform.position.x >= collision.bounds.center.x)
                {
                    this.transform.position += Vector3.right * eachModuleDistance;
                }

                if (this.transform.position.y < collision.bounds.center.y)
                {
                    this.transform.position -= Vector3.up * eachModuleDistance;
                }
                else if (this.transform.position.y >= collision.bounds.center.y)
                {
                    this.transform.position += Vector3.up * eachModuleDistance;
                }
            }
        }
    }

 
}
