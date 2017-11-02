using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class CriminalShotGun : Weapon
    {

        private float reBoundValue = 10f;
        public CriminalShotGun()
        {
            weapontype = WeaponType.CriminalShotGun;
            bulletSpeed = 4f;
            weaponScale = Vector3.one * 2.5f;
            relativePosition = new Vector3(0f, 0f, 0f);

             
        }
        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            for(int i = 0; i < 3; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                  

                    bullet.gameObject.SetActive(true);
                    Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
                    Vector3 fireDIr = PlayerPos - firePos;
                    fireDIr = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDIr;

                    firePos += (Vector3)Random.insideUnitCircle*0.3f;

                    bullet.Initialize(firePos, fireDIr.normalized, bulletSpeed, BulletType.EnemyBullet, 0.5f,1,2.5f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }

           
            }

            PlayFireAnim();


        }
    }
}