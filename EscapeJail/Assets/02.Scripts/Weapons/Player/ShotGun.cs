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
            SetWeaponKind(WeaponKind.ShotGun);
            bulletSpeed = 10f;
            fireDelay = 1f;       
            needBulletToFire = 1;


        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("ShotGun");
            Vector3 fireDir = fireDirection;


            for (int i = 0; i < 3; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {                 
                    fireDir = Quaternion.Euler(0f, 0f, -10f + 10f * i) * fireDirection;
                    bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 0.5f, 1, 0.5f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");

                }
            }   

        }




    }
}
