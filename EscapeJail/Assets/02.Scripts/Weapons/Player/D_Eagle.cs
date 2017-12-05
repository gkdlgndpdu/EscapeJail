using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class D_Eagle : Weapon
    {
  

        public D_Eagle()
        {
            weapontype = WeaponType.D_Eagle;
            SetWeaponKind(WeaponKind.Pistol);     
            SetReBound(5f);

            bulletSpeed = 13f;
            fireDelay = 0.6f; 
            needBulletToFire = 1;
            damage = 4;
          
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            SoundManager.Instance.PlaySoundEffect("pistol4");
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet,1.2f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, CustomColor.Silver);



            }

        }
    }
}