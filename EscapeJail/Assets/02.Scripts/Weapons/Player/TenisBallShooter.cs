using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class TenisBallShooter : Weapon
    {

        //리볼버 반동
        private float reBoundValue = 5f;
        private GameObject bounceBullet;
        public TenisBallShooter()
        {
            weapontype = WeaponType.TenisBallShooter;
            bulletSpeed = 13f;
            fireDelay = 0.4f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 1;

            LoadPrefab();

        }

        private void LoadPrefab()
        {
            bounceBullet = Resources.Load<GameObject>("Prefabs/Objects/BounceBullet");
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            if (bounceBullet != null)
            {
               GameObject makeObj = GameObject.Instantiate(bounceBullet, TemporaryObjects.Instance.transform);
                if (makeObj != null)
                {
                    BounceBullet bounceBullet = makeObj.GetComponent<BounceBullet>();
                    if (bounceBullet != null)
                    {
                        bounceBullet.Initialize(BulletType.PlayerBullet, firePos+ fireDirection.normalized*0.1f, fireDirection, 10f, 5);
                    }
                }
            }

        }
    }
}