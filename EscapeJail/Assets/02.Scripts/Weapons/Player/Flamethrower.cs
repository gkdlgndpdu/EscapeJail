using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Flamethrower : Weapon
    {
        
        private float reBoundValue = 0f;

        public Flamethrower()
        {
            weapontype = WeaponType.Flamethrower;
            bulletSpeed = 10f;
            fireDelay = 0.3f;
            maxAmmo = 100;
            nowAmmo = 100;
            needBulletToFire = 1;
            weaponScale = Vector3.one * 3;
    
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;
            PlayFireAnim();
            useBullet();
            FireDelayOn();
         

            SpecialBullet bullet = ObjectManager.Instance.specialBulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, SpecialBulletType.Fire, 4f, 1);
             


            }

        }
    }
}