﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class WellRoad : Weapon
    {


        public WellRoad()
        {
            weapontype = WeaponType.WellRoad;
            SetWeaponKind(WeaponKind.Pistol);
            SetReBound(0f);

            bulletSpeed = 15f;
            fireDelay = 0.65f;
        
            needBulletToFire = 1;
            damage = 2;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            SoundManager.Instance.PlaySoundEffect("pistol9");
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();            
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 1f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, Color.black);



            }
        }
    }
}
