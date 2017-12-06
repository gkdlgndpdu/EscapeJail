using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Car98 : Weapon
    {
        
        public Car98()
        {
            weapontype = WeaponType.Car98;
            SetWeaponKind(WeaponKind.Sniper);
      
            fireDelay = 2f;     
            needBulletToFire = 1;   
            damage = 15;



        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("sniper8");
            //   int layerMask = MyUtils.GetLayerMaskByString("Enemy");
            FireHitScan(firePos+Vector3.up*0.1f, fireDirection, damage,CustomColor.Orange);
        }

    }
}
