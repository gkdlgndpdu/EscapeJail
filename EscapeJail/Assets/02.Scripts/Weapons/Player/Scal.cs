using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Scal : Weapon
    {

        public Scal()
        {
            weapontype = WeaponType.Scal;
            SetWeaponKind(WeaponKind.AR);
            SetReBound(10f);
            bulletSpeed = 15f;
            fireDelay = 0.13f;
            damage = 1;
       
            needBulletToFire = 1;
            weaponScale = Vector3.one * 2.5f;
     

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            SoundManager.Instance.PlaySoundEffect("pistol7");

            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                fireDir.Normalize();
                bullet.Initialize(firePos + fireDir * 0.5f, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.5f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, CustomColor.Orange);
            }


        }
    }
}