using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Bfe : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 0f;

        public Bfe()
        {
            weapontype = WeaponType.Bfe;
            bulletSpeed = 5f;
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
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                fireDir.Normalize();
                bullet.Initialize(firePos + fireDir * 0.1f, fireDir, bulletSpeed, BulletType.PlayerBullet, 1f, damage);
                bullet.InitializeImage("BfeBullet", true);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(false);
            }

        }
    }
}