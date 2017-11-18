using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Boomerang : Weapon
    {

        //리볼버 반동
        private float reBoundValue = 5f;
        private GameObject loadObject;
        public Boomerang()
        {
            weapontype = WeaponType.Boomerang;
            bulletSpeed = 13f;
            fireDelay = 0.4f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 1;

            LoadPrefab();
            weaponScale = Vector3.one *1.5f;

        }

        private void LoadPrefab()
        {
            loadObject = Resources.Load<GameObject>("Prefabs/Objects/BoomerangBullet");
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            if (loadObject != null)
            {
                GameObject makeObj = GameObject.Instantiate(loadObject, TemporaryObjects.Instance.transform);
                if (makeObj != null)
                {
                    BoomerangBullet bounceBullet = makeObj.GetComponent<BoomerangBullet>();
                    bounceBullet.Initialize(firePos, fireDirection, damage);
                }
            }

        }
    }
}
