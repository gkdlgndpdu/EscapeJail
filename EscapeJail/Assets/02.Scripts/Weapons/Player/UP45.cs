using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class UP45 : Weapon
    {


        public UP45()
        {
            weapontype = WeaponType.UP45;
            SetWeaponKind(WeaponKind.Pistol);
            SetReBound(5f);
            bulletSpeed = 10f;
            fireDelay = 0.5f;
            
            needBulletToFire = 1;
            damage = 2;
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();

            SoundManager.Instance.PlaySoundEffect("pistol1");

            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 0.5f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, Color.blue);


            }

        }
    }
}