using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class LightSaber : Weapon
{
    public LightSaber()
    {
        weapontype = WeaponType.LightSaber;   
        fireDelay = 1.5f;
        maxAmmo = 100;
        nowAmmo = 100;
        needBulletToFire = 1;
        damage = 5;
        weaponScale = Vector3.one * 3;
        relativePosition = new Vector3(-0.65f, 0f, 0f);
        attackType = AttackType.near;
    }

    public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
    {
        if (canFire() == false) return;
       
        FireDelayOn();
        //PlayFireAnim();
     

    }

}
