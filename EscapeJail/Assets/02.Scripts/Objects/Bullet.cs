using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Player,
    Enemy
}

public class Bullet : MonoBehaviour
{
    private int power =0;
    private BulletType bulletType;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector3 startPos, Vector3 moveDir, float moveSpeed,BulletType bulletType,int power=1)
    {
        SetLayer(bulletType);
        this.bulletType = bulletType;
        this.transform.position = new Vector3(startPos.x, startPos.y, 0f);
        this.power = power;
        if (rb != null)
        rb.velocity = moveDir.normalized*moveSpeed;

    }

    private void SetLayer(BulletType bulletType)
    {
        this.gameObject.layer = LayerMask.NameToLayer(bulletType.ToString());      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            MonsterBase monsterBase = collision.gameObject.GetComponent<MonsterBase>();
            if (monsterBase != null)
                monsterBase.GetDamage(this.power);
        }

        this.gameObject.SetActive(false);
    }

}
