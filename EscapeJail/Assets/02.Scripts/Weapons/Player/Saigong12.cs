using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Saigong12 : Weapon
    {

        public Saigong12()
        {
            weapontype = WeaponType.Saigong12;
            SetWeaponKind(WeaponKind.ShotGun);
            bulletSpeed = 15f;
            fireDelay = 0.3f;      
            needBulletToFire = 1;
            SetReboundDuringFire(5f, 5f);
            SetAmmo(100);



        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {

            if (canFire() == false) return;
            lastFireTime += Time.deltaTime*1.1f;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            SoundManager.Instance.PlaySoundEffect("shotgun3");

            Vector3 firePosit = firePos;

            AddRebound();
            fireDirection = ApplyReboundDirection(fireDirection);
       

            for (int i = 0; i < 5; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize(firePosit + (Vector3)Random.insideUnitCircle * 0.4f + fireDirection * 0.25f, fireDirection, bulletSpeed, BulletType.PlayerBullet, 0.5f, 1, 0.6f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }
            }


        }
    }
}