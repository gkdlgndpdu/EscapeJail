using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : Weapon
{

    public WaterGun()
    {
        weaponName = "Watergun";
        bulletSpeed = 20f;
        fireDelay = 0.3f;
        maxAmmo = 50;
        nowAmmo = 30;
        needBulletToFire = 2;


    }

    public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
    {


    }
}


