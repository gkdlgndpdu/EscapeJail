using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class PPAP900 : Weapon
    {      

        public PPAP900()
        {
            weapontype = WeaponType.PPAP900;
            SetReBound(10f);
            SetWeaponKind(WeaponKind.SMG);

            bulletSpeed = 10f;
            fireDelay = 0.06f;           
            needBulletToFire = 1;
            damage = 1;
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
                fireDir.Normalize();
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                bullet.Initialize(firePos + fireDir * 0.6f, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.3f, damage, 0.4f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, Color.yellow);


            }


        }
    }
}