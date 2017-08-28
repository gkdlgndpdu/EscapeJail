using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    public ShotGun()
    {
        weaponName = "Shotgun";
        bulletSpeed = 5f;
        fireDelay = 1f;

        //임시
        fireDelay = 0f;

        maxAmmo = 10000;
        nowAmmo = 10000;
        needBulletToFire = 3;

    }

    public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
    {
        if (canFire() == false) return;

        useBullet();
        FireDelayOn();
        PlayFireAnim();




        Vector3 fireDir = fireDirection;


        for (int i = 0; i < 3; i++)
        {
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.gameObject.SetActive(true);
                fireDir =Quaternion.Euler(0f, 0f, -15f + 15f * i)*fireDirection;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet,1,1);
                bullet.InitializeImage("white", false);

            }
        }







    }




}
