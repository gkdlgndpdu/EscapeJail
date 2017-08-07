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

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
