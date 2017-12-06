using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class FingerGun : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 5f;

        public FingerGun()
        {
            weapontype = WeaponType.FingerGun;
            bulletSpeed = 13f;
            fireDelay = 0.3f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 4;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            SoundManager.Instance.PlaySoundEffect("fingergun");

            FireHitScan(firePos, fireDirection, damage, CustomColor.SkinColor);

        }
    }
}