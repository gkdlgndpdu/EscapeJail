using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    Lock,
    UnLock,
}

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
    private int SpawnMonsterNum = 5;




    private void Awake()
    {
        
    }

    private bool IsWaveEnd()
    {
        if (monsterList == null) return true;

        for(int i=0;i< monsterList.Count; i++)
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
        if (mapState == MapState.Lock|| isClear==true) return;

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
        for(int i=0;i< SpawnMonsterNum; i++)
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
}
