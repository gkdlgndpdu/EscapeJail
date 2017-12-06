using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class MemoryEraser : Weapon
    { 

        public MemoryEraser()
        {
            weapontype = WeaponType.MemoryEraser;
            SetWeaponKind(WeaponKind.Special);      
            SetAmmo(10);

            fireDelay = 10f;
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            SoundManager.Instance.PlaySoundEffect("Memory");
            //스턴효과
            MonsterManager.Instance.StunAllMonster();
        }

    }
}