using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class H249 : Weapon
    {


        public H249()
        {
            weapontype = WeaponType.H249;
            bulletSpeed = 10f;
            fireDelay = 0.1f;
            SetAmmo(999);
            needBulletToFire = 1;
            damage = 1;

            SetReboundDuringFire(3f, 5f);
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;
            lastFireTime += Time.deltaTime * 1.1f;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            AddRebound();
            fireDirection = ApplyReboundDirection(fireDirection);

            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.Initialize(firePos + fireDirection * 0.6f, fireDirection, bulletSpeed, BulletType.PlayerBullet, 0.5f, damage, 1f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, Color.yellow);
            }


        }
    }
}