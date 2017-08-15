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
        Bullet bullet = ObjectManager.Instance.GetUsableBullet();
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            Vector3 nearestEnemyPos = MonsterManager.Instance.GetNearestMonsterPos(firePos);
            Vector3 fireDIr = nearestEnemyPos - firePos;
            bullet.Initialize(firePos, fireDIr.normalized, 10f, BulletType.Player);
        }     



    }
}

