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

    public void Initialize(Vector3 startPos, Vector3 moveDir, float moveSpeed,BulletType bulletType)
    {
        SetLayer(bulletType);
        this.bulletType = bulletType;
        this.transform.position = new Vector3(startPos.x, startPos.y, 0f);
   
        if (rb == null) return;
        rb.velocity = moveDir.normalized*moveSpeed;

    }

    private void SetLayer(BulletType bulletType)
    {
        this.gameObject.layer = LayerMask.NameToLayer(bulletType.ToString());      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        this.gameObject.SetActive(false);
    }

}
