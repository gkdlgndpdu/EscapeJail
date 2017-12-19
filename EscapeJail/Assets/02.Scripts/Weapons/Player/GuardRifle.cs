using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class GuardRifle : Weapon
    {
        private float reBoundValue = 0f;
        public GuardRifle()
        {
            weapontype = WeaponType.GuardRifle;
            bulletSpeed = 5f;
            weaponScale = Vector3.one * 1.8f;
            relativePosition = new Vector3(0f, 0f, 0f);
        }
        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.gameObject.SetActive(true);
           

                firePos += fireDirection.normalized * 1f;
                fireDirection = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDirection;
                bullet.Initialize(firePos, fireDirection.normalized, bulletSpeed, BulletType.EnemyBullet, 0.5f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, Color.blue);
            }

            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("burst");

        }
    }
}