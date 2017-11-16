using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class DragonBow : Weapon
    {
        private float reBoundValue = 5f;

        public DragonBow()
        {
            weapontype = WeaponType.DragonBow;
            bulletSpeed = 13f;
            fireDelay = 0.4f;
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

            SpecialBullet bullet = ObjectManager.Instance.specialBulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, SpecialBulletType.LaserBullet, 1f, 1);
                bullet.InitializeImage("DragonArrow", true);
                bullet.SetBloom(true, Color.green);
            }                     

        }
    }
}