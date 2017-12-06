using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class TenisBallShooter : Weapon
    {
    

        public TenisBallShooter()
        {
            weapontype = WeaponType.TenisBallShooter;
            SetWeaponKind(WeaponKind.Special);
            bulletSpeed = 13f;
            fireDelay = 0.4f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 2;
        }



        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            SoundManager.Instance.PlaySoundEffect("tenisfire");



            BounceBullet bounceBullet = ObjectManager.Instance.bounceBulletPool.GetItem();
            if (bounceBullet != null)
            {
                bounceBullet.Initialize(BulletType.PlayerBullet, firePos + fireDirection.normalized * 0.1f, fireDirection, 10f, 5, BounceBulletType.tenisBall, damage);
            }



        }
    }
}