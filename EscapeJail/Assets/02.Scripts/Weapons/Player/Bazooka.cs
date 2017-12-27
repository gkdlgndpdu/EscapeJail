using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace weapon
{
    public class Bazooka : Weapon
    {       
     

        public Bazooka()
        {
            weapontype = WeaponType.Bazooka;
            SetWeaponKind(WeaponKind.Special);
            SetReBound(5f);
            bulletSpeed = 10f;
            fireDelay = 0.8f;
            SetAmmo(30);
            needBulletToFire = 1;
            damage = 5;
            weaponScale = Vector3.one * 3;
            relativePosition = new Vector3(-0.3f, 0f, 0f);

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("rocket3");
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-ReBoundValue, ReBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 1, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("bazooka", 3f);
                bullet.SetExplosion(1.5f);


            }

        }
    }

}