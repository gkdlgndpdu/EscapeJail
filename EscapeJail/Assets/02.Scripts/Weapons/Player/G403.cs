using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class G403 : Weapon
    {
        private float reBoundValue = 0f;
        private GameObject mouseBullet;
        public G403()
        {
            weapontype = WeaponType.G403;
            bulletSpeed = 10f;
            fireDelay = 0.4f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 1;
            LoadPrefab();
        }

        private void LoadPrefab()
        {
            mouseBullet = Resources.Load<GameObject>("Prefabs/Objects/G403Bullet");
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            if (mouseBullet != null)
            {
                GameObject makeObj = GameObject.Instantiate(mouseBullet, TemporaryObjects.Instance.transform);
                if (makeObj != null)
                {
                    G403Bullet g403Bullet = makeObj.GetComponent<G403Bullet>();
                    if (g403Bullet != null)
                    {
                        g403Bullet.Initialize(firePos, damage);
                    }
                }
            }

        }
    }
}