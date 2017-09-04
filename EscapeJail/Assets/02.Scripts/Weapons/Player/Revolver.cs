
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class Revolver : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 5f;

        public Revolver()
        {
            weaponName = WeaponType.Revolver;
            bulletSpeed = 10f;
            fireDelay = 0.3f;
            maxAmmo = 100;
            nowAmmo = 100;
            needBulletToFire = 1;
            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(-0.3f, 0f, 0f);




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
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 1, 1);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");


            }

        }
    }

}