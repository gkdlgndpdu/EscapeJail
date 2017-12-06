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
            SetNearWeapon(Color.green, Vector3.one * 15f);


            weapontype = WeaponType.LightSaber;
            fireDelay = 0.5f;
            maxAmmo = 1;
            nowAmmo = 1;
            needBulletToFire = 1;
            
            damage = 4;
            weaponScale = Vector3.one * 4f;
       
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("lightsaber");

        }

    }

}
