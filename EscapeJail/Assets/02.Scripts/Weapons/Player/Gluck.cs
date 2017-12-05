using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Gluck : Weapon
    {
      

        public Gluck()
        {
            weapontype = WeaponType.Gluck;
            SetReBound(5f);
            SetWeaponKind(WeaponKind.Pistol);

            bulletSpeed = 13f;
            fireDelay = 0.3f;        
            needBulletToFire = 1;
            damage = 1;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            SoundManager.Instance.PlaySoundEffect("pistol3");
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                fireDir.Normalize();
                bullet.Initialize(firePos+ fireDir*0.1f, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.5f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, CustomColor.Silver);



            }

        }
    }
}