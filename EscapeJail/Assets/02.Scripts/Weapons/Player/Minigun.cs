using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class Minigun : Weapon
    {

    

        public Minigun()
        {
            weapontype = WeaponType.Minigun;
            SetWeaponKind(WeaponKind.MG);
            SetReBound(20f);
            bulletSpeed = 10f;
            fireDelay = 0.05f;        
            needBulletToFire = 1;
            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(-0.3f, 0f, 0f);


        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("pistol5");
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir.Normalize();
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                bullet.Initialize(firePos + fireDir*0.6f, fireDir, bulletSpeed, BulletType.PlayerBullet, 0.3f, 1);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, Color.yellow);


            }

        }
    }
}
