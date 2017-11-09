using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class HP5 : Weapon
    {

        //리볼버 반동
        private float reBoundValue = 20f;

        public HP5()
        {
            weapontype = WeaponType.HP5;
            bulletSpeed = 10f;
            fireDelay = 0.1f;
            SetAmmo(999);
            needBulletToFire = 1;
            damage = 1;
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
                fireDir.Normalize();
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                bullet.Initialize(firePos + fireDir * 0.6f, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.4f, damage, 0.6f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, CustomColor.Orange);


            }


        }
    }
}