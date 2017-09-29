using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class ShotGun : Weapon
    {
        public ShotGun()
        {
            weapontype = WeaponType.ShotGun;
            bulletSpeed = 10f;
            fireDelay = 1f;

            maxAmmo = 10000;
            nowAmmo = maxAmmo;
            needBulletToFire = 3;
            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(-0.65f, 0f, 0f);

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();




            Vector3 fireDir = fireDirection;


            for (int i = 0; i < 3; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    fireDir = Quaternion.Euler(0f, 0f, -15f + 15f * i) * fireDirection;
                    bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 0.5f, 1, 0.4f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");

                }
            }

            for (int i = 0; i < 2; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    fireDir = Quaternion.Euler(0f, 0f, -7.5f + 15f * i) * fireDirection;
                    bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 0.5f, 1, 0.4f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");

                }
            }








        }




    }
}
