using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class GravityGun : Weapon
    {

        private float reBoundValue = 5f;
        private float pullRadius = 3f;
        private float pullPower = 5f;

        public GravityGun()
        {
            weapontype = WeaponType.GravityGun;
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

            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                fireDir.Normalize();
                bullet.Initialize(firePos + fireDir * 0.1f, fireDir, bulletSpeed, BulletType.PlayerBullet, 2f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, CustomColor.Silver);
                bullet.SetPushOption(1f, 4f, 1);



            }


        }
    }
}