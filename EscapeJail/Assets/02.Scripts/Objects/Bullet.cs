using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    PlayerBullet,
    EnemyBullet
}

public class Bullet : MonoBehaviour
{
    private int power = 0;
    private BulletType bulletType;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector3 startPos, Vector3 moveDir, float moveSpeed, BulletType bulletType, int power = 1)
    {
        SetLayer(bulletType);
        this.bulletType = bulletType;
        this.transform.position = new Vector3(startPos.x, startPos.y, 0f);
        this.power = power;
        if (rb != null)
            rb.velocity = moveDir.normalized * moveSpeed;

    }

    private void SetLayer(BulletType bulletType)
    {
        this.gameObject.layer = LayerMask.NameToLayer(bulletType.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (bulletType)
        {
            case BulletType.PlayerBullet:
                {
                    if (collision.gameObject.CompareTag("Enemy"))
                    {
                        MonsterBase monsterBase = collision.gameObject.GetComponent<MonsterBase>();
                        if (monsterBase != null)
                            monsterBase.GetDamage(this.power);
                    }
                }
                break;
            case BulletType.EnemyBullet:
                {
                    if (collision.gameObject.CompareTag("Player"))
                    {
                        CharacterBase characterBase = collision.gameObject.GetComponent<CharacterBase>();
                        if (characterBase != null)
                            characterBase.GetDamage(this.power);
                    }
                }
                break;
        }
   

        //이펙트 호출

        //이펙트 호출
        this.gameObject.SetActive(false);
    }

}
