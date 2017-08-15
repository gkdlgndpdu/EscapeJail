using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;

    private List<Bullet> bulletPool = new List<Bullet>();
    private List<MonsterBase> monsterPool = new List<MonsterBase>();

    [SerializeField]
    private Transform bulletParent;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        MakeBullets();
    }

    private void MakeBullets()
    {
        if (bulletPool == null) return;

        for(int i = 0; i < 100; i++)
        {
            GameObject bullet = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Objects/Bullet"), bulletParent);

            if (bullet != null)
            {
                Bullet playerBullet = bullet.GetComponent<Bullet>();
                playerBullet.gameObject.SetActive(false);
                bulletPool.Add(playerBullet);
            }
        }
       
    }

    public Bullet GetUsableBullet()
    {
        for(int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].gameObject.activeSelf == true) continue;
            return bulletPool[i];
        }

        Debug.Log("BulletEmpty");
        return null;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
