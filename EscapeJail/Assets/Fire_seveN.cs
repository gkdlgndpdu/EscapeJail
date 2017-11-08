using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Fire_seveN : Weapon
    {

        //리볼버 반동
        private float reBoundValue = 2f;

        public Fire_seveN()
        {
            weapontype = WeaponType.Fire_seveN;
            bulletSpeed = 15f;
            fireDelay = 0.3f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 2;

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
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 0.7f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, Color.blue);



            }

        }
    }
}