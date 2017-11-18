using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class GiraffeSword : Weapon
    {
        public GiraffeSword()
        {
            SetNearWeapon(Color.yellow, Vector3.one * 10f);

            weapontype = WeaponType.GiraffeSword;
            fireDelay = 1f;
            needBulletToFire = 1;
            damage = 3;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
        }
    }
}