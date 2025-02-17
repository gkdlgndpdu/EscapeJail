﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BounceBulletType
{
    tenisBall,
    BillardsBall
}

public class BounceBullet : PlayerSpecialBullet
{
    private Vector3 moveDir;
    private int bouncCount = 0;
    private int bounceMax = 0;
    private bool canCollision = true;
    private BounceBulletType bounceBulletType;

    public List<Sprite> billardsSprites;
    public RuntimeAnimatorController tenisBallAnimator;

    private void DestroyBullet()
    {
        bouncCount = 0;
        bounceMax = 0;
        canCollision = true;
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        if (rb != null)
            rb.velocity = moveDir * moveSpeed;
    }

    public void Initialize(BulletType bulletType, Vector3 startPos, Vector3 moveDir, float moveSpeed, int bounceMax, BounceBulletType bounceBulletType, int damage = 1)
    {
        this.transform.position = startPos;
        this.bulletType = bulletType;
        this.moveDir = moveDir.normalized;
        this.moveSpeed = moveSpeed;
        this.bounceMax = bounceMax;
        this.damage = damage;
        this.bounceBulletType = bounceBulletType;
        SetBulletImage();
        SetLayer();
    }

    private void SetBulletImage()
    {
        switch (bounceBulletType)
        {
            case BounceBulletType.tenisBall:
                {
                    if (animator != null)
                        animator.runtimeAnimatorController = tenisBallAnimator;

                }
                break;
            case BounceBulletType.BillardsBall:
                {
                    if (animator != null)
                        animator.runtimeAnimatorController = null;

                    if (spriteRenderer != null && billardsSprites != null)
                        spriteRenderer.sprite = billardsSprites[Random.Range(0, billardsSprites.Count)];
                }
                break;    
        }
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
        if (bounceBulletType == BounceBulletType.BillardsBall)
        {
            SoundManager.Instance.PlaySoundEffect("cueGunBounce");



        }
        else if (bounceBulletType == BounceBulletType.tenisBall) 
        {
            SoundManager.Instance.PlaySoundEffect("Tenisd");
        }

        ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
        if (effect != null)
            effect.Initilaize(this.transform.position, "revolver", 0.5f, 1f);

        bouncCount += 1;
        if (bouncCount >= bounceMax)
        {
            DestroyBullet();
            return;
        }
        canCollision = false;
        StartCoroutine(CollisionCheckRoutine());

        CharacterInfo chr = collision.gameObject.GetComponent<CharacterInfo>();
        if (chr != null)
        {
            chr.GetDamage(damage);
        }



        int layerMask = MyUtils.GetLayerMaskExcludeName(bulletType.ToString());
        RaycastHit2D rayHit = Physics2D.Raycast(this.transform.position, moveDir, 1f, layerMask);

        Vector2 reflectVector = Vector2.Reflect(moveDir, rayHit.normal);
        reflectVector.Normalize();

        if (rb != null)
            moveDir = reflectVector.normalized;

    }




}

