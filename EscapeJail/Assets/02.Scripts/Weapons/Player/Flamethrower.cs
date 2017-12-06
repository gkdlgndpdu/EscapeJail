using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Flamethrower : Weapon
    {
    
        public Flamethrower()
        {
            weapontype = WeaponType.Flamethrower;
            bulletSpeed = 10f;
            fireDelay = 0.3f;
            SetAmmo(50);        
            needBulletToFire = 1;
            weaponScale = Vector3.one * 3;
    
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;
            PlayFireAnim();
            useBullet();
            FireDelayOn();

            SoundManager.Instance.PlaySoundEffect("FireThrower");
            SpecialBullet bullet = ObjectManager.Instance.specialBulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, SpecialBulletType.Fire, 4f, 1);
             


            }

        }
    }
}