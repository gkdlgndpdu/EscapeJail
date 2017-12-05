using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class N16 : Weapon
    {
    

        public N16()
        {
            weapontype = WeaponType.N16;
            SetWeaponKind(WeaponKind.AR);
            SetReBound(0f);

            bulletSpeed = 13f;
            fireDelay = 0.4f;
            damage = 1;
        
            needBulletToFire = 3;
            weaponScale = Vector3.one * 2.5f;


        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("burst");
            for(int i = 1; i < 4; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    Vector3 fireDir = fireDirection;         
                    fireDir.Normalize();
                    bullet.Initialize(firePos + fireDir * 0.2f*i, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.5f, damage);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                    bullet.SetBloom(true, Color.blue);

                }
            }
        


        }
    }
}