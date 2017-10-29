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
            bulletSpeed = 5f;
            weaponScale = Vector3.one * 3f;
        }
        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.gameObject.SetActive(true);
                Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
                Vector3 fireDIr = PlayerPos - firePos;
                fireDIr = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDIr;
                fireDIr.Normalize();
                bullet.Initialize(firePos+ fireDIr, fireDIr, bulletSpeed, BulletType.EnemyBullet,2f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");


            }

            PlayFireAnim();


        }
    }
}