
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class Revolver : Weapon
    {     

        public Revolver()
        {
            weapontype = WeaponType.Revolver;
            bulletSpeed = 10f;
            fireDelay = 0.4f;
            SetAmmo(1);
            needBulletToFire = 1;
            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(-0.3f, 0f, 0f);
            damage = 1;
            SetReBound(5);
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;
            SoundManager.Instance.PlaySoundEffect("Sample");

            FireDelayOn();
            PlayFireAnim();

            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 0.6f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");


            }

        }
    }

}