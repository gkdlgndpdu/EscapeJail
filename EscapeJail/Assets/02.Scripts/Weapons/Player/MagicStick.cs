using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class MagicStick : Weapon
    {
        
        public MagicStick()
        {
            SetWeaponKind(WeaponKind.Special);     
            weapontype = WeaponType.MagicStick;
            bulletSpeed = 13f;
            fireDelay = 0.4f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 1;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            fireDirection.Normalize();

            SoundManager.Instance.PlaySoundEffect("magic");
        

            for (int i = 0; i < 5; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    Vector3 startPos = Quaternion.Euler(0f, 0f, 72f * i) * Vector3.up*0.4f;
                    bullet.Initialize(firePos + startPos, fireDirection, bulletSpeed, BulletType.PlayerBullet, 1f, damage);
                    bullet.InitializeImage("MagicStickBullet", false);
                    bullet.SetEffectName("revolver");
                    bullet.SetBloom(true, Color.yellow);
                }
            }

            Bullet centerBullet = ObjectManager.Instance.bulletPool.GetItem();
            if (centerBullet != null)
            {

                centerBullet.Initialize(firePos, fireDirection, bulletSpeed, BulletType.PlayerBullet, 1f, damage);
                centerBullet.InitializeImage("MagicStickBullet", false);
                centerBullet.SetEffectName("revolver");
                centerBullet.SetBloom(true, Color.yellow);
            }


        }
    }
}
