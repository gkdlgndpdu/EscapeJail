using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

namespace weapon
{
    public class LightSaber : Weapon
    {
        public LightSaber()
        {     
            SetNearWeapon(Color.green, Vector3.one * 8f);


            weapontype = WeaponType.LightSaber;
            fireDelay = 0f;
            maxAmmo = 1;
            nowAmmo = 1;
            needBulletToFire = 1;
            damage = 1;
            weaponScale = Vector3.one * 4f;
            relativePosition = new Vector3(-0.65f, 0f, 0f);
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            //PlayFireAnim();


        }

    }

}
