using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class ChickenGun : Weapon
    {
        private GameObject chickenEggPrefab;

        private void LoadPrefab()
        {
            chickenEggPrefab = Resources.Load<GameObject>("Prefabs/Objects/ChickenEgg");     
        }

        public ChickenGun()
        {
            LoadPrefab();

            weapontype = WeaponType.ChickenGun;
            SetWeaponKind(WeaponKind.Special);

            bulletSpeed = 13f;
            fireDelay = 0.4f;
            SetAmmo(50);
            needBulletToFire = 1;
            damage = 1;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            SoundManager.Instance.PlaySoundEffect("chickengun");
            if(chickenEggPrefab!=null)
            GameObject.Instantiate(chickenEggPrefab, firePos, Quaternion.identity);
          

        }
    }
}