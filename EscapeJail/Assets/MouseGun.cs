using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGun : Weapon
{
    private float reBoundValue = 5f;
    public MouseGun()
    {
        weaponName = "MouseGun";
    }
    public override void FireBullet(Vector3 firePos)
    {
        Bullet bullet = ObjectManager.Instance.GetUsableBullet();
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            Vector3 PlayerPos =GamePlayerManager.Instance.player.transform.position;
            Vector3 fireDIr = PlayerPos - firePos;
            fireDIr = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDIr;
            bullet.Initialize(firePos, fireDIr.normalized, 10f, BulletType.EnemyBullet);
        }



    }
}
