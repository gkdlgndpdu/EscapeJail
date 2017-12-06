using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class BomberGun : Weapon
    {
        private float reBoundValue = 0f;
        private GameObject bomberGunBullet;
        public BomberGun()
        {
            weapontype = WeaponType.BomberGun;
            SetWeaponKind(WeaponKind.Special);

            bulletSpeed = 10f;
            fireDelay = 0.4f;
            SetAmmo(30);
            needBulletToFire = 1;
            damage = 5;
            LoadPrefab();
        }

        private void LoadPrefab()
        {
            bomberGunBullet = Resources.Load<GameObject>("Prefabs/Objects/BomberGunBullet");
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            SoundManager.Instance.PlaySoundEffect("bombergun");

            if (bomberGunBullet != null)
            {
                GameObject makeObj = GameObject.Instantiate(bomberGunBullet, TemporaryObjects.Instance.transform);
                if (makeObj != null)
                {
                    BomberGunBullet bomberBullet = makeObj.GetComponent<BomberGunBullet>();
                    if (bomberBullet != null)
                    {
                        bomberBullet.Initialize(firePos, damage);
                    }
                }
            }

        }
    }
}