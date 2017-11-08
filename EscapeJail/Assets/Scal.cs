using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Scal : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 10f;

        public Scal()
        {
            weapontype = WeaponType.Scal;
            bulletSpeed = 15f;
            fireDelay = 0.13f;
            damage = 1;
            maxAmmo = 1000;
            nowAmmo = maxAmmo;
            needBulletToFire = 1;
            weaponScale = Vector3.one * 2.5f;
     

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
                bullet.Initialize(firePos + fireDir * 0.5f, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.5f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, CustomColor.Orange);
            }


        }
    }
}