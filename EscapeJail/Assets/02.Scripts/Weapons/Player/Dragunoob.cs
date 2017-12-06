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
            SetWeaponKind(WeaponKind.Sniper);
            bulletSpeed = 10f;
            fireDelay = 0.4f;           
            needBulletToFire = 1;
            damage = 2;
            SetAmmo(50);

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("sniper5");   
            FireHitScan(firePos + Vector3.up * 0.1f, fireDirection, damage);
        }

    }
}