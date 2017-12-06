using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{

    public class Hammer : Weapon
    {

        public Hammer()
        {           
            SetNearWeapon(Color.gray, Vector3.one * 30f);


            weapontype = WeaponType.Hammer;
            fireDelay = 30f;
       
      
            needBulletToFire = 1;
            damage = 50;

            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(0f, 0f, 0f);
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("hammer");
        }


    }
}
