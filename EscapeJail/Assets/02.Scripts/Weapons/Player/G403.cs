using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class G403 : Weapon
    {
        private GameObject mouseBullet;
        public G403()
        {
            weapontype = WeaponType.G403;
            SetWeaponKind(WeaponKind.Special);
            SetAmmo(50);

            bulletSpeed = 10f;
            fireDelay = 0.4f;
            needBulletToFire = 1;
            damage = 5;
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
            SoundManager.Instance.PlaySoundEffect("Mouse");
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