using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Saigong12 : Weapon
    {

        public Saigong12()
        {
            weapontype = WeaponType.Saigong12;
            bulletSpeed = 10f;
            fireDelay = 0.3f;
            SetAmmo(10000);
            needBulletToFire = 5;
            SetReboundDuringFire(5f, 5f);

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {

            if (canFire() == false) return;
            lastFireTime += Time.deltaTime*1.1f;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            Vector3 firePosit = firePos;

            AddRebound();
            fireDirection = ApplyReboundDirection(fireDirection);
       

            for (int i = 0; i < needBulletToFire; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize(firePosit + (Vector3)Random.insideUnitCircle * 0.4f + fireDirection * 0.25f, fireDirection, bulletSpeed, BulletType.PlayerBullet, 0.5f, 1, 0.7f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }
            }


        }
    }
}