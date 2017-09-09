using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;

    [HideInInspector]
    public ObjectPool<Bullet> bulletPool;
    [HideInInspector]
    public ObjectPool<ExplosionEffect> effectPool;

    public MonsterPool monsterPool;

    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private Transform EffectParent;
    [SerializeField]
    private Transform MonsterParent;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        MakePool();
    }

    private void MakePool()
    {
        GameObject bullet = (GameObject)Resources.Load("Prefabs/Objects/Bullet");
        if (bullet != null)
            bulletPool = new ObjectPool<Bullet>(bulletParent, bullet, 10);

        GameObject effect = (GameObject)Resources.Load("Prefabs/Objects/ExplosionEffect");
        if (effect != null)
            effectPool = new ObjectPool<ExplosionEffect>(EffectParent, effect, 10);
        
        MakeMonsterPool();

    }

    private void MakeMonsterPool()
    {
        StageData nowStageData = GameOption.Instance.StageData;
        monsterPool = new MonsterPool(MonsterParent, nowStageData);
    }

    public MonsterBase GetRandomMonster()
    {
        if (monsterPool == null) return null;

        return monsterPool.GetRandomMonster();
    }

    public static UnityEngine.Object LoadGameObject(string name)
    {
        return Resources.Load(name);
    }






}
