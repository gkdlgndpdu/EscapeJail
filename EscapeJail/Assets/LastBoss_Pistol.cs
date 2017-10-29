using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class LastBoss_Pistol : Weapon
    {
        private float reBoundValue = 5f;
        public LastBoss_Pistol()
        {
            weapontype = WeaponType.LastBoss_Pistol;
            bulletSpeed = 13f;
            weaponScale = Vector3.one * 3f;
            bulletType = BulletType.EnemyBullet;
        }
        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
       
                Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
                Vector3 fireDIr = PlayerPos - firePos;
                fireDIr = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDIr;
                fireDIr.Normalize();
                bullet.Initialize(firePos+ fireDIr*0.7f, fireDIr, bulletSpeed, bulletType);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
            }

            PlayFireAnim();


        }
    }
}