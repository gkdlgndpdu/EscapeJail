using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class HP5 : Weapon
    {


        public HP5()
        {
            weapontype = WeaponType.HP5;
            SetWeaponKind(WeaponKind.SMG);
            SetReBound(20f);

            bulletSpeed = 10f;
            fireDelay = 0.1f;         
            needBulletToFire = 1;
            damage = 1;
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("smg5");
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir.Normalize();
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                bullet.Initialize(firePos + fireDir * 0.6f, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.4f, damage, 0.6f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, CustomColor.Orange);


            }


        }
    }
}