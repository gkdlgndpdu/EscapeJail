
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    private float reBoundValue = 5f;

    public Revolver()
    {
        weaponName = "Revolver";
        bulletSpeed = 10f;
        fireDelay = 0.3f;
        maxAmmo =100;
        nowAmmo =100;
        needBulletToFire = 1;
    }

    public override void FireBullet(Vector3 firePos)
    {
        if (canFire()==false) return;

        useBullet();
        FireDelayOn();
        PlayFireAnim();

        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            Vector3 nearestEnemyPos = MonsterManager.Instance.GetNearestMonsterPos(firePos);
            Vector3 fireDIr = nearestEnemyPos - firePos;
            fireDIr = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue))* fireDIr;
            bullet.Initialize(firePos, fireDIr.normalized, bulletSpeed, BulletType.PlayerBullet);
            bullet.SetBulletColor(Color.yellow);
        }


        

    }
}

