using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class War2000 : Weapon
    {

        //리볼버 반동
        private float reBoundValue = 5f;

        public War2000()
        {
            weapontype = WeaponType.War2000;
            SetWeaponKind(WeaponKind.Sniper);

            bulletSpeed = 10f;
            fireDelay = 0.4f;         
            needBulletToFire = 1;
            SetAmmo(50);
            damage = 3;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("sniper1");
            //   int layerMask = MyUtils.GetLayerMaskByString("Enemy");
            FireHitScan(firePos + Vector3.up * 0.1f, fireDirection, damage,CustomColor.Orange);
        }
    }
}