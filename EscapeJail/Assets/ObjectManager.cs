using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;

    //private List<Bullet> bulletPool = new List<Bullet>();
    //private List<MonsterBase> monsterPool = new List<MonsterBase>();
    //private List<ExplosionEffect> effectPool = new List<ExplosionEffect>();

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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;


        GameObject bullet =(GameObject)Resources.Load("Prefabs/Objects/Bullet");
        bulletPool = new ObjectPool<Bullet>(null, bullet, 10);

    }

   

}
