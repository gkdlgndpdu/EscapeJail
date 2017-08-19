using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    PlayerBullet,
    EnemyBullet
}

//public struct bulletOption
//{
//    public Vector3 startPos;
//    public Vector3 moveDir;
//    public float moveSpeed;
//    public BulletType bulletType;
//    public int bulletPower;
//    public float bulletSize;
//    public bulletOption(Vector3 startPos, Vector3 moveDir, float moveSpeed, BulletType bulletType, int bulletPower ,float bulletSize)
//    {
//        this.startPos = startPos;
//        this.moveDir = moveDir;
//        this.moveSpeed = moveSpeed;
//        this.bulletType = bulletType;
//        this.bulletPower = bulletPower;
//        this.bulletSize = bulletSize;
//    }
//}

public class Bullet : MonoBehaviour
{
    private int power = 0;
    private BulletType bulletType;
    private Rigidbody2D rb;
    private SpriteGlow spriteGlow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteGlow = GetComponent<SpriteGlow>();
    }

    public void Initialize(Vector3 startPos, Vector3 moveDir, float moveSpeed, BulletType bulletType, float bulletScale = 1f, int power = 1)
    {
        this.transform.position = new Vector3(startPos.x, startPos.y, 0f);

        if (rb != null)
            rb.velocity = moveDir.normalized * moveSpeed;

        this.bulletType = bulletType;
        SetLayer(bulletType);

        this.transform.localScale = Vector3.one * bulletScale;

        this.power = power;


    }

    public void SetBulletColor(Color color)
    {
        if (spriteGlow != null)
            spriteGlow.GlowColor = color;
    }

    public void SetBulletLifeTime(float lifeTime)
    {
        Invoke("BulletDestroy", lifeTime);
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
        ExplosionEffect effect =  ObjectManager.Instance.effectPool.GetItem();
        effect.Initilaize(this.transform.position, null,0.5f);
        //이펙트 호출
        BulletDestroy();
    }

    private void BulletDestroy()
    {
        this.gameObject.SetActive(false);
    }

}
