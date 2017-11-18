using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSpecialBullet : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected BulletType bulletType = BulletType.PlayerBullet;
    protected int damage;
    protected float moveSpeed = 0f;
    protected SpriteRenderer spriteRenderer;
    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.gravityScale = 0f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        SetLayer();
    }

    protected void SetLayer()
    {
        this.gameObject.layer = LayerMask.NameToLayer(bulletType.ToString());
    }
}
