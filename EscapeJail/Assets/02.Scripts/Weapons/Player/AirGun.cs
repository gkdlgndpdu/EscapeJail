using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class AirGun : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 5f;

        public AirGun()
        {
            weapontype = WeaponType.AirGun;
            bulletSpeed = 13f;
            fireDelay = 0.3f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 1;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();


            FireHitScan(firePos, fireDirection, damage, Color.white,true,6f);

        }
    }
}