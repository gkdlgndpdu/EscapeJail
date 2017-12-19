using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class LastBoss_Bazooka : Weapon
    {
        private float reBoundValue = 5f;
        public LastBoss_Bazooka()
        {
            weapontype = WeaponType.LastBoss_Bazooka;
            bulletSpeed = 10f;
            weaponScale = Vector3.one * 3f;
            fireDelay = 1f;
        }
        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();

            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.gameObject.SetActive(true);
                Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
                Vector3 fireDIr = PlayerPos - firePos;
                fireDIr = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDIr;
                bullet.Initialize(firePos, fireDIr.normalized, bulletSpeed, BulletType.EnemyBullet, 1.5f, 1, 1f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, CustomColor.Orange);
                bullet.SetBulletDestroyAction(BulletDestroyAction.aroundFire);
            }

            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("rocket2");





        }
    }
}