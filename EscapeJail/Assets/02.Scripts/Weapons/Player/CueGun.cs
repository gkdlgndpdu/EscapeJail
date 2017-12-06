using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class CueGun : Weapon
    {  
        private GameObject bounceBullet;
        public CueGun()
        {
            weapontype = WeaponType.CueGun;
            SetWeaponKind(WeaponKind.Special);
            bulletSpeed = 13f;
            fireDelay = 0.4f;
            SetAmmo(50);
            needBulletToFire = 1;
            damage = 1;

        }


        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            SoundManager.Instance.PlaySoundEffect("cuegunfire");

            for(int i = 0; i < 5; i++)
            {
                Vector3 fd = Quaternion.Euler(0f, 0f, -20f +i*10f) * fireDirection;

                BounceBullet bounceBullet = ObjectManager.Instance.bounceBulletPool.GetItem();
                if (bounceBullet != null)
                {
                    bounceBullet.Initialize(BulletType.PlayerBullet, firePos + fd.normalized * 0.1f, fd.normalized, 10f, 3, BounceBulletType.BillardsBall);
                }
            }
  



        }
    }
}