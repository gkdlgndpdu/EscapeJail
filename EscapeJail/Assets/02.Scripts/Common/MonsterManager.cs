using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance;
    private List<MonsterBase> monsterList;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        monsterList = new List<MonsterBase>();
    }

    public void AddToList(MonsterBase monster)
    {
        if (monsterList == null) return;
        monsterList.Add(monster);
    }
    public void DeleteInList(MonsterBase monster)
    {
        if (monsterList == null) return;
        monsterList.Remove(monster);
    }

    //인자 현재 플레이어 위치
    public Vector3 GetNearestMonsterPos(Vector3 playerPosit)
    {
        if (monsterList == null) return Vector3.zero;
        if (monsterList.Count == 0) return Vector3.zero;
        if (monsterList.Count == 1)
        {
            return monsterList[0].transform.position;
        }

        //
        //벽에 막혀있을때의 예외처리 필요
        //

        monsterList.Sort((a, b) => { return Vector3.Distance(a.transform.position, playerPosit).CompareTo(Vector3.Distance(b.transform.position, playerPosit)); });
        return monsterList[0].transform.position;


    }

    public MonsterBase SpawnMonster(MonsterName monsterName,Vector3 SpawnPosit)
    {
       MonsterBase monster =  ObjectManager.Instance.monsterPool.GetItem();
        if (monster == null) return null;

       monster.ResetMonster();
       monster.transform.position = SpawnPosit;
       return monster;
    }
 
}
