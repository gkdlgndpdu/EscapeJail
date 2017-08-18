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
        bulletPool = new ObjectPool<Bullet>(null, bullet, 100);


    }

    //private void MakeBullets()
    //{
    //    if (bulletPool == null) return;

    //    for (int i = 0; i < 100; i++)
    //    {
    //        GameObject bullet = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Objects/Bullet"), bulletParent);

    //        if (bullet != null)
    //        {
    //            Bullet playerBullet = bullet.GetComponent<Bullet>();
    //            playerBullet.gameObject.SetActive(false);
    //            bulletPool.Add(playerBullet);
    //        }
    //    }

    //}

    //private void MakeEffects()
    //{
    //    if (effectPool == null) return;

    //    for (int i = 0; i < 100; i++)
    //    {
    //        GameObject effect = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Objects/ExplosionEffect"), bulletParent);

    //        if (effect != null)
    //        {
    //            ExplosionEffect Expeffect = effect.GetComponent<ExplosionEffect>();
    //            Expeffect.gameObject.SetActive(false);
    //            effectPool.Add(playerBullet);
    //        }
    //    }
    //}

    //public Bullet GetUsableBullet()
    //{
    //    for (int i = 0; i < bulletPool.Count; i++)
    //    {
    //        if (bulletPool[i].gameObject.activeSelf == true) continue;
    //        return bulletPool[i];
    //    }

    //    Debug.Log("BulletEmpty");
    //    return null;
    //}

    // Use this for initialization

}
