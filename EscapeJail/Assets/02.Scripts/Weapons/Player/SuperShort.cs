using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class SuperShort : Weapon
    {

        public SuperShort()
        {
            weapontype = WeaponType.SuperShort;
            SetWeaponKind(WeaponKind.ShotGun);
            SetAmmo(100);
            bulletSpeed = 10f;
            fireDelay = 0.9f;        
            needBulletToFire = 1;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            Vector3 firePosit = firePos;
            fireDirection.Normalize();
            SoundManager.Instance.PlaySoundEffect("shotgun2");
            for (int i = 0; i < 5; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize(firePosit + (Vector3)Random.insideUnitCircle * 0.3f + fireDirection * 0.25f, fireDirection, bulletSpeed, BulletType.PlayerBullet, 0.5f, 1, 0.4f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }
            }


        }
    }
}