using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    public ShotGun()
    {
        weaponName = "Shotgun";
        bulletSpeed = 5f;
    }

    public override void FireBullet(Vector3 firePos)
    {

        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();

        Vector3 fireDIr = Vector3.zero;
        Vector3 nearestEnemyPos = MonsterManager.Instance.GetNearestMonsterPos(firePos);

        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            
            fireDIr = nearestEnemyPos - firePos;
            bullet.Initialize(firePos, fireDIr.normalized, bulletSpeed, BulletType.PlayerBullet);
            bullet.SetBulletColor(Color.yellow);
        }

        bullet = ObjectManager.Instance.bulletPool.GetItem();
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            fireDIr = Quaternion.Euler(0f, 0f, -15f) * fireDIr;
            bullet.Initialize(firePos, fireDIr.normalized, bulletSpeed, BulletType.PlayerBullet);
            bullet.SetBulletColor(Color.yellow);
        
        }

        bullet = ObjectManager.Instance.bulletPool.GetItem();
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            fireDIr = Quaternion.Euler(0f, 0f, 30f) * fireDIr;
            bullet.Initialize(firePos, fireDIr.normalized, bulletSpeed, BulletType.PlayerBullet);
            bullet.SetBulletColor(Color.yellow);
        }



    }




}
