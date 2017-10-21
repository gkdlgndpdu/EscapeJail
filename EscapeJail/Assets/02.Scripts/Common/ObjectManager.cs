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
        GameObject bullet = Resources.Load<GameObject>("Prefabs/Objects/Bullet");
        if (bullet != null)
            bulletPool = new ObjectPool<Bullet>(bulletParent, bullet, 10);

        GameObject effect = Resources.Load<GameObject>("Prefabs/Objects/ExplosionEffect");
        if (effect != null)
            effectPool = new ObjectPool<ExplosionEffect>(EffectParent, effect, 10);

        GameObject specialBullet = Resources.Load<GameObject>("Prefabs/Objects/SpecialBullet");
        if (specialBullet != null)
            specialBulletPool = new ObjectPool<SpecialBullet>(bulletParent, specialBullet, 10);

        GameObject line = Resources.Load<GameObject>("Prefabs/Objects/LinePrefab");
        if (line != null)
            linePool = new ObjectPool<DrawLiner>(EffectParent, line, 3);

        GameObject characterStateEffect = Resources.Load<GameObject>("Prefabs/Objects/CharacterStateEffect");
        if (characterStateEffect!=null)
            characterStatePool = new ObjectPool<CharacterStateEffect>(EffectParent, characterStateEffect,10);

        MakeMonsterPool();

    }
    public void MakeMonsterPool()
    {
        if (monsterPool != null)
        {
            monsterPool.ReleaseMonsterPool();
            monsterPool = null;
        }

        StageData nowStageData = GameOption.Instance.StageData;
        monsterPool = new MonsterPool(MonsterParent, nowStageData);
    }

    private void DestroyPool()
    {
        if (monsterPool != null)
            monsterPool.ReleaseMonsterPool();




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
