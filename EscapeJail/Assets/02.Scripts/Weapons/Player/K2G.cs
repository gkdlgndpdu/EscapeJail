using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class K2G : Weapon
    {

        public K2G()
        {
            weapontype = WeaponType.K2G;
            bulletSpeed = 13f;
            fireDelay = 0.5f;

            SetAmmo(30);
            needBulletToFire = 1;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            Vector3 firePosit = firePos;
            fireDirection.Normalize();
            Vector3 fireDir = fireDirection;
            for (int i = 0; i < 3; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    fireDir = Quaternion.Euler(0f, 0f, -3f + 3f * i) * fireDirection;
                    bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 0.5f, 1, 0.5f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");

                }
            }


        }
    }
}