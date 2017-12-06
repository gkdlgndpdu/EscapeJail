using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class M1G1 : Weapon
    {

        public M1G1()
        {
            weapontype = WeaponType.M1G1;
            bulletSpeed = 15f;
            fireDelay = 1.2f;         
            SetWeaponKind(WeaponKind.Sniper);
            needBulletToFire = 1;
            damage = 5;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            SoundManager.Instance.PlaySoundEffect("sniper6");    
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 1f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, CustomColor.Orange);



            }
        }
    }
}