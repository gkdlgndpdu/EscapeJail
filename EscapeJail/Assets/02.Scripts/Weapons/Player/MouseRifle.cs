using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    //쥐4 총임
    public class MouseRifle : Weapon
    {


        private float reBoundValue = 25f;

        public MouseRifle()
        {
            weapontype = WeaponType.MouseRifle;
            bulletSpeed = 3.5f;
            weaponScale = Vector3.one * 2;
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.gameObject.SetActive(true);
                Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
                Vector3 fireDIr = PlayerPos - firePos;

                firePos += fireDIr.normalized * 0.5f;
                fireDIr = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDIr;
                bullet.Initialize(firePos, fireDIr.normalized, bulletSpeed, BulletType.EnemyBullet, 0.8f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                
            }

            PlayFireAnim();
            SoundManager.Instance.PlaySoundEffect("Sample");

        }

    }

}
