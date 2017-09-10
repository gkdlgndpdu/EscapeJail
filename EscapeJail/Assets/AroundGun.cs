using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class AroundGun : Weapon
    {
        public AroundGun()
        {
            weaponName = WeaponType.AroundGun;
            bulletSpeed = 5f;
            weaponScale = Vector3.one * 3;
        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {                    
            for(int i = 0; i < 12; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    Vector3 fireDIr= Quaternion.Euler(new Vector3(0f,0f,i * 30))*Vector3.right;
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize(firePos, fireDIr.normalized, bulletSpeed, BulletType.EnemyBullet);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }
            }            

            PlayFireAnim();


        }

    }

}
