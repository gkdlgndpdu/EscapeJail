using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class KeyBoardShotGun : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 5f;

        public KeyBoardShotGun()
        {
            weapontype = WeaponType.KeyBoardShotGun;
            SetWeaponKind(WeaponKind.Special);
            bulletSpeed = 13f;
            fireDelay = 1f;
            SetAmmo(20);
            needBulletToFire = 1;
            damage = 2;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            SoundManager.Instance.PlaySoundEffect("keyboardshotgun");
            Vector3 firePosit = firePos;
            for (int i = 0; i < 5; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                Vector3 fd = Quaternion.Euler(0f, 0f, Random.Range(-8f, 8f)) * fireDirection;
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize(firePosit + (Vector3)Random.insideUnitCircle * 0.35f, fd.normalized, bulletSpeed, BulletType.PlayerBullet, 1f, damage, 0.7f);
                    bullet.InitializeMultipleImage("KeyBoardShotGunBullet");
                    bullet.SetEffectName("revolver");
                    bullet.SetBloom(false);
                }
            }

        }
    }
}