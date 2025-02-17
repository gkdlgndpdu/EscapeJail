﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class LastBoss_MinuGun : Weapon
    {
        private float reBoundValue = 30f;
        public LastBoss_MinuGun()
        {
            weapontype = WeaponType.LastBoss_MinuGun;
            bulletSpeed =6f;
            weaponScale = Vector3.one * 3f;
            fireDelay = 0.1f;
        }
        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();

            for(int i = 0; i < 3; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
                    Vector3 fireDIr = PlayerPos - firePos;
                    fireDIr = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDIr;
                    fireDIr.Normalize();
                    bullet.Initialize(firePos + fireDIr * 1.5f, fireDIr, bulletSpeed, BulletType.EnemyBullet, 0.5f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }
            }

            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("lastbossminigun");

        }
    }
}