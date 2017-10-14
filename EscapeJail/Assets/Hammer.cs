using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{

    public class Hammer : Weapon
    {

        public Hammer()
        {
            //근접무기에서는 반드시 설정
            attackType = AttackType.near;
            //슬래시 모양
            slashColor = Color.gray;
            slashSize = Vector3.one * 15f;



            weapontype = WeaponType.Hammer;
            fireDelay = 3f;
            maxAmmo = 1;
            nowAmmo = 1;
            needBulletToFire = 1;
            damage = 100;
            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(0f, 0f, 0f);
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            //PlayFireAnim();


        }


    }
}
