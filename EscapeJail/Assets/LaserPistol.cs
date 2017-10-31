using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class LaserPistol : Weapon
    {

        private float reBoundValue = 0f;

        public LaserPistol()
        {
            weapontype = WeaponType.LaserPistol;
            bulletSpeed = 20f;
            fireDelay = 0.1f;
            maxAmmo = 100;
            nowAmmo = 100;
            needBulletToFire = 1;
            weaponScale = Vector3.one * 2;
  
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;
            PlayFireAnim();
            useBullet();
            FireDelayOn();

            firePos += Vector3.up * 0.15f;

            SpecialBullet bullet = ObjectManager.Instance.specialBulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, SpecialBulletType.LaserBullet, 1f, 1);
                bullet.SetBloom(true, Color.green);

            }

        }
    }

}
