using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{

    public class Last6Rifle : Weapon
{

    private float reBoundValue = 30f;

    public Last6Rifle()
    {
        weapontype = WeaponType.Last6Rifle;
        bulletSpeed = 5f;
        weaponScale = Vector3.one * 2f; 
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
            bullet.Initialize(firePos, fireDIr.normalized, bulletSpeed, BulletType.EnemyBullet, 0.7f);
            bullet.InitializeImage("white", false);
            bullet.SetEffectName("revolver");
            
        }

        PlayFireAnim();

    }
}
}
