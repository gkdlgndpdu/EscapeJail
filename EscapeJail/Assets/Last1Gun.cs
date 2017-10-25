using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class Last1Gun : Weapon
    {
        private float reBoundValue = 0f;
        public Last1Gun()
        {
            weapontype = WeaponType.Last1Gun;
            bulletSpeed = 12f;
            weaponScale = Vector3.one * 1.6f;
        }
        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {

            SpecialBullet bullet = ObjectManager.Instance.specialBulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet, SpecialBulletType.LaserBullet, 1.5f, 1);
                bullet.SetBloom(true, Color.red);

            }

        }

    }
}
