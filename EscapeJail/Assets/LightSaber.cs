using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class LightSaber : Weapon
{
    public LightSaber()
    {
        //근접무기에서는 반드시 설정
        attackType = AttackType.near;
        //슬래시 모양
        slashColor = Color.green;
        slashSize = Vector3.one * 7f;



        weapontype = WeaponType.LightSaber;   
        fireDelay = 0f;
        maxAmmo = 1;
        nowAmmo = 1;
        needBulletToFire = 1;
        damage = 1;
        weaponScale = Vector3.one * 3;
        relativePosition = new Vector3(-0.65f, 0f, 0f);
    }

    public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
    {
        if (canFire() == false) return;
       
        FireDelayOn();
        //PlayFireAnim();
     

    }

}
