using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class sword : Weapon
    {

        public sword()
        {
            SetNearWeapon(Color.white, Vector3.one * 8f);


            weapontype = WeaponType.sword;
            fireDelay = 1f;


            needBulletToFire = 1;
            damage = 3;

            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(0f, 0f, 0f);
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
         
        }

    }
}