using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class AssaultRifle : Weapon
    {
   

        public AssaultRifle()
        {
            weapontype = WeaponType.AssaultRifle;
            SetReBound(3f);
            SetWeaponKind(WeaponKind.AR);

            bulletSpeed = 15f;
            fireDelay = 0.2f;
            damage = 1;       
            needBulletToFire = 1;
            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(-0.56f, 0f, 0f);

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
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                fireDir.Normalize();
                bullet.Initialize(firePos+ fireDir*0.5f, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.5f, 1);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");

            }


        }

    }
}
