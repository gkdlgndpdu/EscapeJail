
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class Revolver : Weapon
    {
        //리볼버 반동
        protected float reBoundValue = 10f;

        public Revolver()
        {
            weapontype = WeaponType.Revolver;
            bulletSpeed = 10f;
            fireDelay = 0.5f;
            SetAmmo(1);
            needBulletToFire = 1;
            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(-0.3f, 0f, 0f);
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;
           
            FireDelayOn();
            PlayFireAnim();

            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 0.5f, 1);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");


            }

        }
    }

}