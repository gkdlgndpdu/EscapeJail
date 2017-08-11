using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponBase : MonoBehaviour
{
    public void ChangeWeapon()
    {

    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate((GameObject)Resources.Load("Prefabs/Objects/Bullet"));

        if (bullet != null)
        {
            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
            if (playerBullet != null)
            {
                //Vector3 fireDIr = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;     
                Vector3 fireDIr = MonsterManager.Instance.GetNeariestMonsterPos(this.transform.position) - this.transform.position;

                playerBullet.Initialize(this.transform.position, fireDIr.normalized, 10f);
            }
        }
    }
}
