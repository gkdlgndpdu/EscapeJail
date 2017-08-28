using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : Weapon
{

    public WaterGun()
    {
        weaponName = "Watergun";
        bulletSpeed = 5f;
        fireDelay = 0.3f;
        maxAmmo = 50;
        nowAmmo = 30;
        needBulletToFire = 2;


    }

    public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
    {
        if (canFire() == false) return;

        useBullet();
        FireDelayOn();
        PlayFireAnim();

        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            Vector3 fireDir = fireDirection;     
            bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 1, 1, weaponName);
            bullet.InitializeImage("watergun", true);


        }

    }
}


