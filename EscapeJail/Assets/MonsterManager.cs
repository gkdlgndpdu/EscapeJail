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

    public Vector3 GetNeariestMonsterPos(Vector3 playerPosit)
    {
        if (monsterList == null)
        {
            Debug.Log("적이 없음");
            return Vector3.zero;
        }
        else
        {
            monsterList.Sort((a, b) => { return Vector3.Distance(a.transform.position, playerPosit).CompareTo(Vector3.Distance(b.transform.position, playerPosit)); });
            return monsterList[0].transform.position;
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
