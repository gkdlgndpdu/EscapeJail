using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    public Revolver()
    {
        weaponName = "Revolver";
    }
    public override void FireBullet(Vector3 firePos)
    {
        GameObject bullet = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Objects/Bullet"));

        if (bullet != null)
        {
            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
            if (playerBullet != null)
            {
                Vector3 nearestEnemyPos = MonsterManager.Instance.GetNearestMonsterPos(firePos);
                Vector3 fireDIr = nearestEnemyPos - firePos;
                playerBullet.Initialize(firePos, fireDIr.normalized, 10f);
            }
        }



    }
}

