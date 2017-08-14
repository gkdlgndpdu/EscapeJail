using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    public ShotGun()
    {
        weaponName = "Shotgun";
    }

    public override void FireBullet(Vector3 firePos)
    {

        //임시코드
        GameObject bullet = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Objects/Bullet"));

        if (bullet != null)
        {
            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
            if (playerBullet != null)
            {
                Vector3 nearestEnemyPos = MonsterManager.Instance.GetNearestMonsterPos(firePos);
                Vector3 fireDIr = nearestEnemyPos - firePos;
                playerBullet.Initialize(firePos, fireDIr.normalized, 30f);
            }
        }

        bullet = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Objects/Bullet"));

        if (bullet != null)
        {
            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
            if (playerBullet != null)
            {
                Vector3 nearestEnemyPos = MonsterManager.Instance.GetNearestMonsterPos(firePos);
                Vector3 fireDIr = nearestEnemyPos - firePos;
                fireDIr = Quaternion.Euler(0f, 0f, 15f)* fireDIr;
                playerBullet.Initialize(firePos, fireDIr.normalized, 30f);
            }
        }

        bullet = GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Objects/Bullet"));

        if (bullet != null)
        {
            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
            if (playerBullet != null)
            {
                Vector3 nearestEnemyPos = MonsterManager.Instance.GetNearestMonsterPos(firePos);
                Vector3 fireDIr = nearestEnemyPos - firePos;
                fireDIr = Quaternion.Euler(0f, 0f, -15f) * fireDIr;
                playerBullet.Initialize(firePos, fireDIr.normalized, 30f);
            }
        }

    }
}
