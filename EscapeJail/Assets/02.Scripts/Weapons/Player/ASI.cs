using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class ASI : Weapon
    {     

        public ASI()
        {
            weapontype = WeaponType.ASI;
            SetWeaponKind(WeaponKind.Sniper);
            bulletSpeed = 10f;
            fireDelay = 0.9f; 
            needBulletToFire = 1;       
            damage = 5;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("sniper9");
            //   int layerMask = MyUtils.GetLayerMaskByString("Enemy");
            FireHitScan(firePos+Vector3.up*0.1f, fireDirection, damage);
        }

    }
}