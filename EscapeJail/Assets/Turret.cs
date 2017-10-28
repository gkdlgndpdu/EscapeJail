using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Turret : CharacterInfo
{
    private BulletType bulletType;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isDestroy = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //상태이상 면역
        isImmuneAnyState = true;
    }

    private void OnEnable()
    {
        MonsterManager.Instance.AddToList(this.gameObject);
    }
    private void OnDisable()
    {
        MonsterManager.Instance.DeleteInList(this.gameObject);
    }

    //애니메이션에 연결
    public void TurretOff()
    {
        this.gameObject.SetActive(false);
    }

    private void SetLayer()
    {
        if (bulletType == BulletType.PlayerBullet)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Player");
            this.gameObject.tag = "Player";
        }
        else
        {
            this.gameObject.layer = LayerMask.NameToLayer("Enemy");
            this.gameObject.tag = "Enemy";
        }
    }

    public void Initialize(Vector3 position, BulletType bulletType, int hp)
    {
        this.bulletType = bulletType;
        this.transform.position = position;
        SetHp(hp);
        SetLayer();
        isDestroy = false;
    }

    public void FireBullet()
    {
        Vector3 fireDir = Vector3.one;

        switch (bulletType)
        {
            case BulletType.PlayerBullet:
                {
                    GameObject nearMonster = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
                    if (nearMonster != null)
                        fireDir = nearMonster.transform.position - this.transform.position;
                    else if (nearMonster == null)
                        fireDir = Vector3.up;
                }
                break;
            case BulletType.EnemyBullet:
                {
                    CharacterBase player = GamePlayerManager.Instance.player;
                    if(player!=null)
                    fireDir = player.transform.position - this.transform.position;
                }
                break;
        }

        float reBoundValue = 5f;
        float bulletSpeed = 5f;

        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
        if (bullet != null)
        {
            fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
            bullet.Initialize(this.transform.position, fireDir.normalized, bulletSpeed, bulletType, 0.5f, 1);
            bullet.InitializeImage("white", false);
            bullet.SetEffectName("revolver");
        }


        FlipTurret(fireDir);

    }

    private void FlipTurret(Vector3 fireDir)
    {
        if (spriteRenderer != null)
        {
            if (fireDir.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }


    private void TurretOn()
    {

    }
    private void TurretDestroy()
    {
        if (animator != null)
            animator.SetTrigger("TurretDestroy");


    }

    //애니메이션에 연결
    private void AttackOn()
    {
        if (animator != null)
            animator.SetTrigger("TurretAttackOn");

    }

    //private void AttackOff()
    //{
    //    if (animator != null)
    //        animator.SetTrigger("TurretAttackOff");
    //}


    public override void GetDamage(int damage)
    {
        if (isDestroy == true) return;

        hp -= damage;

        if (hp <= 0)
        {
            isDestroy = true;

            if (animator != null)
                animator.SetTrigger("TurretDestroy");

        }

    }
}
