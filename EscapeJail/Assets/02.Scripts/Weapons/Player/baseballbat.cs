using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Baseballbat : Weapon
    {

        public Baseballbat()
        {
            SetNearWeapon(new Color(147f/255f,0f,0f,1f), Vector3.one * 8f);


            weapontype = WeaponType.Baseballbat;
            fireDelay = 1f;


            needBulletToFire = 1;
            damage = 5;

            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(0f, 0f, 0f);
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;
            SoundManager.Instance.PlaySoundEffect("baseballbat2");
            FireDelayOn();
            PlayFireAnim();

        }
    }
}
