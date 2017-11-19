
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class BasicSniper : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 5f;

        public BasicSniper()
        {
            weapontype = WeaponType.BasicSniper;
            bulletSpeed = 10f;
            fireDelay = 2f;
            maxAmmo = 100;
            nowAmmo = 100;
            needBulletToFire = 1;
            weaponScale = Vector3.one * 0.7f;
            relativePosition = new Vector3(-0.3f, 0f, 0f);
            damage = 10;



        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            //   int layerMask = MyUtils.GetLayerMaskByString("Enemy");
            FireHitScan(firePos, fireDirection, damage);   
        }

  

        
    }

}