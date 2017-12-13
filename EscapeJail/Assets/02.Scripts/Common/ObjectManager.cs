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
    [HideInInspector]
    public ObjectPool<SpecialBullet> specialBulletPool;
    [HideInInspector]
    public ObjectPool<DrawLiner> linePool;
    [HideInInspector]
    public ObjectPool<CharacterStateEffect> characterStatePool;
    [HideInInspector]
    public ObjectPool<PollutedArea> pollutedAreaPool;
    [HideInInspector]
    public ObjectPool<Turret> turretPool;
    [HideInInspector]
    public ObjectPool<MonsterSpawnEffect> monsterSpawnEffectPool;
    [HideInInspector]
    public ObjectPool<BounceBullet> bounceBulletPool;
    [HideInInspector]
    public ObjectPool<ThunderLine> thunderLinePool;
    [HideInInspector]
    public ObjectPool<DropGoods> coinPool;
    public MonsterPool monsterPool;

    [HideInInspector]
    public FastObjectPool<Tile> tilePool;

    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private Transform EffectParent;
    [SerializeField]
    private Transform MonsterParent;
    [SerializeField]
    private Transform ObjectParent;
    [SerializeField]
    public Transform TileParent;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        MakePool();

    }

    public static void MakePool<T>(ref ObjectPool<T> objectPool, string prefabPath, Transform parent, int size) where T : Component
    {
        GameObject LoadObj = Resources.Load<GameObject>(prefabPath);
        if (LoadObj != null)
            objectPool = new ObjectPool<T>(parent, LoadObj, size);

    }

    public static void MakeFastPool<T>(ref FastObjectPool<T> objectPool, string prefabPath, Transform parent, int size) where T : Component
    {
        GameObject LoadObj = Resources.Load<GameObject>(prefabPath);
        if (LoadObj != null)
            objectPool = new FastObjectPool<T>(parent, LoadObj, size);

    }

    private void MakePool()
    {
        MakePool<Bullet>(ref bulletPool, "Prefabs/Objects/Bullet", bulletParent,500);
        MakePool<ExplosionEffect>(ref effectPool, "Prefabs/Objects/ExplosionEffect", EffectParent, 10);
        MakePool<SpecialBullet>(ref specialBulletPool, "Prefabs/Objects/SpecialBullet", bulletParent, 10);
        MakePool<DrawLiner>(ref linePool, "Prefabs/Objects/LinePrefab", EffectParent, 3);
        MakePool<CharacterStateEffect>(ref characterStatePool, "Prefabs/Objects/CharacterStateEffect", EffectParent, 10);
        MakePool<PollutedArea>(ref pollutedAreaPool, "Prefabs/Objects/PollutedArea", EffectParent, 5);
        MakePool<Turret>(ref turretPool, "Prefabs/Objects/Turret", ObjectParent, 5);
        MakePool<MonsterSpawnEffect>(ref monsterSpawnEffectPool, "Prefabs/Objects/MonsterSpawnEffect", EffectParent, 10);
        MakePool<BounceBullet>(ref bounceBulletPool, "Prefabs/Objects/BounceBullet", bulletParent, 1);
        MakePool<ThunderLine>(ref thunderLinePool, "Prefabs/Objects/ThunderLine", EffectParent, 10);
        MakePool<DropGoods>(ref coinPool, "Prefabs/Objects/Coin", ObjectParent, 10);

        MakeFastPool<Tile>(ref tilePool, "Prefabs/Maps/MapObjects/tile", TileParent,1000);

        MakeMonsterPool();

    }
    public void MakeMonsterPool()
    {
        if (monsterPool != null)
        {
            monsterPool.ReleaseMonsterPool();
            monsterPool = null;
        }

        StageData nowStageData = StagerController.Instance.stageData;
        monsterPool = new MonsterPool(MonsterParent, nowStageData);
    }

    private void DestroyPool()
    {
        if (monsterPool != null)
            monsterPool.ReleaseMonsterPool();
    }


    public MonsterBase GetSpecificMonster(MonsterName name)
    {
        if (monsterPool == null) return null;

        return monsterPool.GetSpecificMonster(name);
    }


    public void AllEnemyBulletDestroy()
    {
        List<Bullet> allBulletList = bulletPool.UsingList;
        if (allBulletList == null) return;

        for (int i=0;i< allBulletList.Count; i++)
        {
            if (allBulletList[i].gameObject.activeSelf == false) continue;

            if (allBulletList[i].BulletType == BulletType.EnemyBullet)
                allBulletList[i].BulletDestroy();
        }
    }

    public void Update()
    {
     

    }





}
