using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WeaponBase : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ChangeWeapon()
    {

    }

    public void FlipWeapon(bool value)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipY = value;
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate((GameObject)Resources.Load("Prefabs/Objects/Bullet"));

        if (bullet != null)
        {
            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
            if (playerBullet != null)
            {             
                Vector3 nearestEnemyPos = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
                Vector3 fireDIr = nearestEnemyPos - this.transform.position;

               

                playerBullet.Initialize(this.transform.position, fireDIr.normalized, 10f);
            }
        }
    }
}
