using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class MemoryEraser : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 5f;

        public MemoryEraser()
        {
            weapontype = WeaponType.MemoryEraser;
            bulletSpeed = 13f;
            fireDelay = 0.4f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 1;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            //스턴효과
            MonsterManager.Instance.StunAllMonster();
        }

    }
}