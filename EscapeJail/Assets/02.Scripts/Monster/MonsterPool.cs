using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool
{
    private Dictionary<MonsterName, ObjectPool<MonsterBase>> pool ;

    private Transform parent;

    private StageData nowStageData;

    private RandomGenerator<MonsterName> randomGenerator;

    public MonsterPool(Transform monsterParent, StageData stageData)
    {
        pool = new Dictionary<MonsterName, ObjectPool<MonsterBase>>();
        parent = monsterParent;
        nowStageData = stageData;

        randomGenerator = new RandomGenerator<MonsterName>();

        Initialize();
    }

    public void ReleaseMonsterPool()
    {
        if (pool == null) return;

        foreach(KeyValuePair<MonsterName,ObjectPool<MonsterBase>> data in pool)
        {
            data.Value.ReleasePool();
        }
        pool.Clear();
        pool = null;
    }

    public void Initialize()
    {
    
        if (nowStageData == null) return;

        if (nowStageData.spawnEnemyList == null) return;

        for (int i = 0; i < nowStageData.spawnEnemyList.Count; i++)
        {
            MonsterName monsterName = nowStageData.spawnEnemyList[i];
            ObjectPool<MonsterBase> monsterPool = null;

            //랜덤
            if (randomGenerator != null)
            {
                MonsterDB monsterData = DatabaseLoader.Instance.GetMonsterData(monsterName);

                if(monsterData!=null)
                randomGenerator.AddToList(monsterName, monsterData.Probability);
            }

            string path = string.Format("Prefabs/Monsters/{0}", monsterName.ToString());
            GameObject obj = Resources.Load<GameObject>(path);

            if (obj == null) return;

            monsterPool = new ObjectPool<MonsterBase>(parent, obj, 1);

            if (monsterPool != null)
            {
                if(pool.ContainsKey(monsterName)==false)
                pool.Add(monsterName, monsterPool);
            }
        }
    }

    public MonsterBase GetRandomMonster()
    {
        if (pool == null) return null;
        if (randomGenerator == null) return null;
             
        List<MonsterName> keyList = new List<MonsterName>(pool.Keys);

        MonsterName RandomKey = randomGenerator.GetRandomData();

        if (pool.ContainsKey(RandomKey) == false) return null;

        return pool[RandomKey].GetItem();
    }

    public MonsterBase GetSpecificMonster(MonsterName name)
    {
        if (pool == null) return null;
        if (pool.ContainsKey(name) == false) return null;
        return pool[name].GetItem();
    }




}
