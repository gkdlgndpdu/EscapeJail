﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Shortknife : Weapon
    {
        public Shortknife()
        {
            SetNearWeapon(Color.black, Vector3.one * 6f);


            weapontype = WeaponType.Shortknife;
            fireDelay = 0.2f;


            needBulletToFire = 1;
            damage = 1;

            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(0f, 0f, 0f);
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;
            SoundManager.Instance.PlaySoundEffect("swings2");
            FireDelayOn();
            PlayFireAnim();
        }
    }
}