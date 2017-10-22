using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance;
    public List<GameObject> monsterList;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        monsterList = new List<GameObject>();
    }

    public void AddToList(GameObject monster)
    {
        if (monsterList == null) return;
        monsterList.Add(monster);
    }
    public void DeleteInList(GameObject monster)
    {
        if (monsterList == null) return;
        monsterList.Remove(monster);
    }

    //인자 현재 플레이어 위치 , null체크 필요
    public GameObject GetNearestMonsterPos(Vector3 playerPosit)
    {
        if (monsterList == null) return null;
        if (monsterList.Count == 0) return null;
        if (monsterList.Count == 1)
        {
            return monsterList[0];
        }

        //
        //벽에 막혀있을때의 예외처리 필요
        //

        monsterList.Sort((a, b) => { return Vector3.Distance(a.transform.position, playerPosit).CompareTo(Vector3.Distance(b.transform.position, playerPosit)); });

        float distance = Vector3.Distance(monsterList[0].transform.position, playerPosit);
     
        return monsterList[0];

    }

    public MonsterBase SpawnRandomMonster(Vector3 SpawnPosit)
    {
        MonsterBase monster =  ObjectManager.Instance.GetRandomMonster();
      
        if (monster == null) return null;

       monster.transform.position = SpawnPosit;
       monster.ResetMonster();    
       return monster;
    }

    public MonsterBase SpawnSpecificMonster(MonsterName name, Vector3 SpawnPosit)
    {
        MonsterBase monster = ObjectManager.Instance.GetSpecificMonster(name);

        if (monster == null) return null;

        monster.transform.position = SpawnPosit;
        monster.ResetMonster();
        return monster;
    }
 
}
