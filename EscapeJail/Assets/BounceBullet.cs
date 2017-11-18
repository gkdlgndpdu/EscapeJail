using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : PlayerSpecialBullet
{
    private Vector3 moveDir;
  //  private Rigidbody2D rb;
 //   private float moveSpeed =10f;
    private int bouncCount = 0;
    private int bounceMax = 0;
   // private BulletType bulletType;
    private bool canCollision = true;
   // private int damage;


    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        if (rb != null)
            rb.velocity = moveDir * moveSpeed;
    }

    public void Initialize(BulletType bulletType,Vector3 startPos,Vector3 moveDir,float moveSpeed,int bounceMax,int damage =1)
    {
        this.transform.position = startPos;         
        this.bulletType = bulletType;
        this.moveDir = moveDir.normalized;
        this.moveSpeed = moveSpeed;
        this.bounceMax = bounceMax;
        this.damage = damage;
        SetLayer();
    }

    //중복충돌 방지
    IEnumerator CollisionCheckRoutine()
    {
        yield return new WaitForSeconds(0.01f);
        canCollision = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCollision == false) return;

        bouncCount += 1;
        canCollision = false;
        StartCoroutine(CollisionCheckRoutine());

        CharacterInfo chr = collision.gameObject.GetComponent<CharacterInfo>();
        if (chr != null)
        {
            chr.GetDamage(damage);
        }

        if (bouncCount>= bounceMax)
        {
            Destroy(this.gameObject);
            return;
        }

        int layerMask = MyUtils.GetLayerMaskExcludeName(bulletType.ToString());
        RaycastHit2D rayHit= Physics2D.Raycast(this.transform.position, moveDir, 1f, layerMask);

        Vector2 reflectVector =  Vector2.Reflect(moveDir, rayHit.normal);
        reflectVector.Normalize();

        if (rb != null)
            moveDir = reflectVector.normalized;    
        
    }


}
