using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class RoseGun : Weapon
    {
 

        public RoseGun()
        {
            weapontype = WeaponType.RoseGun;
            SetWeaponKind(WeaponKind.Special);
            bulletSpeed = 10f;
            fireDelay = 0.4f;
            SetAmmo(50);
            needBulletToFire = 1;

        }
        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            SoundManager.Instance.PlaySoundEffect("swings2");

            SpecialBullet bullet = ObjectManager.Instance.specialBulletPool.GetItem();
            if (bullet != null)
            {
                Vector3 fireDir = fireDirection;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, SpecialBulletType.Poison, 1.5f, 1, 2f);
                bullet.SetEffectName("GasGunExplostion", 1);
                bullet.InitializeImage("RoseGunBullet", false);
            }
        }
    }
}
