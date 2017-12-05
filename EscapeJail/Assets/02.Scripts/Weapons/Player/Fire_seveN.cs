using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Fire_seveN : Weapon
    {  

        public Fire_seveN()
        {
            weapontype = WeaponType.Fire_seveN;
            SetReBound(2f);
            SetWeaponKind(WeaponKind.Pistol);

            bulletSpeed = 15f;
            fireDelay = 1f;
            
            needBulletToFire = 1;
            damage = 3;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            SoundManager.Instance.PlaySoundEffect("pistol1");
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 0.7f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, Color.blue);



            }

        }
    }
}