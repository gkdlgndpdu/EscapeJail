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
    public ObjectPool<MonsterBase> monsterPool;
    [HideInInspector]
    public ObjectPool<ExplosionEffect> effectPool;


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
        bulletPool = new ObjectPool<Bullet>(bulletParent, bullet, 10);

        GameObject effect = (GameObject)Resources.Load("Prefabs/Objects/ExplosionEffect");
        effectPool = new ObjectPool<ExplosionEffect>(EffectParent, effect, 10);

        GameObject mouse1 = (GameObject)Resources.Load("Prefabs/Monsters/Mouse2");
        monsterPool = new ObjectPool<MonsterBase>(MonsterParent, mouse1, 1);

        
    }

    public static UnityEngine.Object LoadGameObject(string name)
    {
        return Resources.Load(name) ;
    }




   

}
