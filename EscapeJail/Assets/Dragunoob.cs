using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class Dragunoob : Weapon
    {

        //리볼버 반동
        private float reBoundValue = 0f;

        public Dragunoob()
        {
            weapontype = WeaponType.Dragunoob;
            bulletSpeed = 10f;
            fireDelay = 0.4f;
            maxAmmo = 100;
            nowAmmo = 100;
            needBulletToFire = 1;
            damage = 2;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            //   int layerMask = MyUtils.GetLayerMaskByString("Enemy");
            FireHitScan(firePos + Vector3.up * 0.1f, fireDirection, damage);
        }

    }
}