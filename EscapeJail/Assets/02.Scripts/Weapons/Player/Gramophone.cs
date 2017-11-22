using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Gramophone : Weapon
    {

        //리볼버 반동
        private float reBoundValue = 5f;

        public Gramophone()
        {
            weapontype = WeaponType.Gramophone;
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
            for(int i = 0; i < 3; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {

                    Vector3 fireDir = Quaternion.Euler(0f,0f,-5f+i*5f)*fireDirection;              
                    fireDir.Normalize();
                    bullet.Initialize(firePos + fireDir * 0.1f, fireDir, bulletSpeed, BulletType.PlayerBullet, 1f, damage);
                    bullet.InitializeImage("GramophoneBullet", true);
                    bullet.SetEffectName("revolver");
                    bullet.SetBloom(false);
                }

            }          

        }
    }
}