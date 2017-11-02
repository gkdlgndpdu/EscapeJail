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

    public MonsterPool monsterPool;

    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private Transform EffectParent;
    [SerializeField]
    private Transform MonsterParent;
    [SerializeField]
    private Transform ObjectParent;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        MakePool();

    }

    public void MakePool<T>(ref ObjectPool<T> objectPool, string prefabPath, Transform parent, int size) where T : Component
    {
        GameObject LoadObj = Resources.Load<GameObject>(prefabPath);
        if (LoadObj != null)
            objectPool = new ObjectPool<T>(parent, LoadObj, 10);

    }

    private void MakePool()
    {
        MakePool<Bullet>(ref bulletPool, "Prefabs/Objects/Bullet", bulletParent, 10);
        MakePool<ExplosionEffect>(ref effectPool, "Prefabs/Objects/ExplosionEffect", EffectParent, 10);
        MakePool<SpecialBullet>(ref specialBulletPool, "Prefabs/Objects/SpecialBullet", bulletParent, 10);
        MakePool<DrawLiner>(ref linePool, "Prefabs/Objects/LinePrefab", EffectParent, 3);
        MakePool<CharacterStateEffect>(ref characterStatePool, "Prefabs/Objects/CharacterStateEffect", EffectParent, 10);
        MakePool<PollutedArea>(ref pollutedAreaPool, "Prefabs/Objects/PollutedArea", EffectParent, 5);
        MakePool<Turret>(ref turretPool, "Prefabs/Objects/Turret", ObjectParent, 5);

        

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

 






}
