using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Scientist_GasGun : Weapon
    {
        private float reBoundValue = 0f;

        public Scientist_GasGun()
        {
            weapontype = WeaponType.Scientist_GasGun;
            bulletSpeed = 3f;
            fireDelay = 0.3f;
            maxAmmo = 100;
            nowAmmo = 100;
            needBulletToFire = 1;
            weaponScale = Vector3.one * 4;
            relativePosition = new Vector3(-0.3f, 0f, 0f);
        }
        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {

            SpecialBullet bullet = ObjectManager.Instance.specialBulletPool.GetItem();
            if (bullet != null)
            {
                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet, SpecialBulletType.PoisionGranade, 1.5f,1,2f);
                bullet.SetEffectName("GasGunExplostion", 5);
                bullet.SetExplosion(1.5f);
            }
        }







    }
}
