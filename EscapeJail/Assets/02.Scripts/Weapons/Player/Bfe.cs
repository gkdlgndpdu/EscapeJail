using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Bfe : Weapon
    {
   
        private GameObject bulletPrefab;
        public Bfe()
        {
            weapontype = WeaponType.Bfe;
            SetWeaponKind(WeaponKind.Special);

            bulletSpeed = 5f;
            fireDelay = 0.4f;
            SetAmmo(50);
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
            SoundManager.Instance.PlaySoundEffect("pulse");

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