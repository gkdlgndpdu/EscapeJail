using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class M400 : Weapon
    {

  

        public M400()
        {
            weapontype = WeaponType.M400;
   
            SetWeaponKind(WeaponKind.AR);
            SetReBound(6f);
            damage = 2;
            bulletSpeed = 13f;
            fireDelay = 0.1f;
            needBulletToFire = 1;
            weaponScale = Vector3.one * 2.5f;


        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            SoundManager.Instance.PlaySoundEffect("Sample");
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                fireDir.Normalize();
                bullet.Initialize(firePos + fireDir * 0.5f+Vector3.up*0.1f, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.5f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, Color.blue);

            }


        }
    }
}