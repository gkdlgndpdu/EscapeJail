using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : CharacterBase
{


    private new void Awake()
    {
        base.Awake();
    }
    // Use this for initialization
    private new void Start()
    {

    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate((GameObject)Resources.Load("Prefabs/Objects/Bullet"));

        if (bullet != null)
        {
            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
            if (playerBullet != null)
            {
                Vector3 fireDIr = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;                
                playerBullet.Initialize(this.transform.position, fireDIr.normalized, 10f);
            }
        }
    }
}
