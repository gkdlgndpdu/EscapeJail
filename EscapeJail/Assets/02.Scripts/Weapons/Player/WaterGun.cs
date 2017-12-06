using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class WaterGun : Weapon
    {

        public WaterGun()
        {
            weapontype = WeaponType.WaterGun;
            SetWeaponKind(WeaponKind.Special);
            SetReBound(0f);
            bulletSpeed = 13f;
            fireDelay = 0.3f;
            SetAmmo(1);
            needBulletToFire = 1;
         
            damage = 1;
 

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;
            
            FireDelayOn();
            PlayFireAnim();

            SoundManager.Instance.PlaySoundEffect("watergun");
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 1f, damage);
                bullet.InitializeImage("watergun", true);
                bullet.SetBloom(false);
                bullet.SetEffectName("revolver");


            }

        }
    }
}


