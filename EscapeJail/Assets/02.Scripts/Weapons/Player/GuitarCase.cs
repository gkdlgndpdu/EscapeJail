using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class GuitarCase : Weapon
    {
            

        public GuitarCase()
        {
            weapontype = WeaponType.GuitarCase;
            SetWeaponKind(WeaponKind.Special);
            bulletSpeed = 13f;
            fireDelay = 1f;
            SetAmmo(30);
            needBulletToFire = 1;
            damage = 5;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            SoundManager.Instance.PlaySoundEffect("guitarcase");

            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                Vector3 fireDir = fireDirection;
           
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 1, damage);
                bullet.InitializeImage("Missile", true);
                bullet.SetEffectName("bazooka", 3f);
                bullet.SetExplosion(1.5f);
                bullet.SetBloom(false);
                bullet.RotateBullet();
            }

        }
    }
}