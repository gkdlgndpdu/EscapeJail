using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class MindArrow : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 5f;
        private GameObject arrowPrefab;

        public MindArrow()
        {
            weapontype = WeaponType.MindArrow;
            SetWeaponKind(WeaponKind.Special);
            bulletSpeed = 13f;
            fireDelay = 0.4f;
            SetAmmo(15);
            needBulletToFire = 1;
            damage = 1;
            LoadPrefab();
        }

        private void LoadPrefab()
        {
            arrowPrefab = Resources.Load<GameObject>("Prefabs/Objects/MindArrow_Arrow");
        }



        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            SoundManager.Instance.PlaySoundEffect("magicstick2");
            if (arrowPrefab != null)
            {
                GameObject InstObj = GameObject.Instantiate(arrowPrefab,firePos,Quaternion.identity);
                if (InstObj != null)
                {
                    MindArrow_Arrow arrow = InstObj.GetComponent<MindArrow_Arrow>();
                    if (arrow != null)
                    {
                        arrow.Initialize(firePos, fireDirection.normalized, damage);
                    }
                }
            }
       

        }
    }
}