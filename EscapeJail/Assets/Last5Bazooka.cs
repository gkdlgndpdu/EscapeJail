using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Last5Bazooka : Weapon
    {

        private float reBoundValue = 5f;
        public Last5Bazooka()
        {
            weapontype = WeaponType.Last5Bazooka;
            bulletSpeed = 3f;
            weaponScale = Vector3.one * 1.6f;
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
                bullet.Initialize(firePos, fireDIr.normalized, bulletSpeed, BulletType.EnemyBullet,1.5f,1,1f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBulletDestroyAction(BulletDestroyAction.aroundFire);
            }

            PlayFireAnim();


        }
    }
}