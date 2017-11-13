using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class PumpAction : Weapon
    {

        public PumpAction()
        {
            weapontype = WeaponType.PumpAction;
            bulletSpeed = 10f;
            fireDelay = 1f;

            SetAmmo(30);        
            needBulletToFire = 1;       

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            Vector3 firePosit = firePos;
            fireDirection.Normalize();

            for (int i = 0; i < 4; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                Vector3 fd = Quaternion.Euler(0f, 0f, Random.Range(-8f,8f)) * fireDirection;
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);                  
                    bullet.Initialize(firePosit+(Vector3)Random.insideUnitCircle*0.35f, fd.normalized, bulletSpeed, BulletType.PlayerBullet, 0.5f, 1, 0.7f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }
            }
       

        }

    }
}