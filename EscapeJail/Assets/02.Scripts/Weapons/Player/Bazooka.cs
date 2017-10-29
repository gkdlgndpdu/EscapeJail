﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace weapon
{
    public class Bazooka : Weapon
    {       
        private float reBoundValue = 5f;

        public Bazooka()
        {
            weapontype = WeaponType.Bazooka;
            bulletSpeed = 10f;
            fireDelay = 1.5f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 1;
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
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 1, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("bazooka", 3f);
                bullet.SetExplosion(1.5f);


            }

        }
    }

}