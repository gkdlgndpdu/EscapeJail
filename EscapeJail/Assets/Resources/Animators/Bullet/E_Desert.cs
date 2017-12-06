using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class E_Desert : Weapon
    {
             

        public E_Desert()
        {
            weapontype = WeaponType.E_Desert;
            SetReBound(5f);
            SetWeaponKind(WeaponKind.Special);

            bulletSpeed = 10f;
            fireDelay = 0.5f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 4;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            SoundManager.Instance.PlaySoundEffect("E_desert");
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                fireDir.Normalize();
                bullet.Initialize(firePos + fireDir * 0.1f, fireDir, bulletSpeed, BulletType.PlayerBullet, 1.5f, damage);
                bullet.InitializeImage("E_Desert", true);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(false);
            }

        }
    }
}