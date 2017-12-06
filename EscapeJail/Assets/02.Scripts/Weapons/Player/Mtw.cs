using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Mtw : Weapon
    {

        //리볼버 반동
        private float reBoundValue = 0f;

        public Mtw()
        {
            weapontype = WeaponType.Mtw;
            SetWeaponKind(WeaponKind.Sniper);

            fireDelay = 3f;           
            needBulletToFire = 1;
            damage = 22;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("sniper10");
            //   int layerMask = MyUtils.GetLayerMaskByString("Enemy");
            FireHitScan(firePos + Vector3.up * 0.1f, fireDirection, damage);
        }
    }
}