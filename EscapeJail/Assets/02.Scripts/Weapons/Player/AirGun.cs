using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class AirGun : Weapon
    {
     

        public AirGun()
        {
            weapontype = WeaponType.AirGun;
            SetWeaponKind(WeaponKind.Special);
            bulletSpeed = 13f;
            fireDelay = 0.3f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 3;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            SoundManager.Instance.PlaySoundEffect("AirGun");
            FireHitScan(firePos, fireDirection, damage, Color.white,true,6f);

        }
    }
}