using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class AKL : Weapon
    {

        //리볼버 반동
        private float reBoundValue = 15f;

        public AKL()
        {
            weapontype = WeaponType.AKL;
            bulletSpeed = 15f;
            fireDelay = 0.18f;
            damage = 2;
            maxAmmo = 1000;
            nowAmmo = maxAmmo;
            needBulletToFire = 1;          
           

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

          

            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                fireDir.Normalize();
                bullet.Initialize(firePos + fireDir * 0.6f+Vector3.up*0.1f, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.5f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");

            }


        }

    }
}
