using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Bfe : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 0f;
        private GameObject bulletPrefab;
        public Bfe()
        {
            weapontype = WeaponType.Bfe;
            bulletSpeed = 5f;
            fireDelay = 0.4f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 1;
            LoadPrefab();
        }

        private void LoadPrefab()
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/Objects/BfeBullet");
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            if (bulletPrefab != null)
            {
                GameObject loadObj = GameObject.Instantiate(bulletPrefab);
                if (loadObj != null)
                {
                    BfeBullet bullet = loadObj.GetComponent<BfeBullet>();
                    if (bullet != null)
                    {
                        bullet.Initialize(firePos, fireDirection);
                        
                    }
                }
            

            }
          

        }
    }
}