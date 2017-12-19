using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Turret : CharacterInfo
{
    private BulletType bulletType;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isDestroy = false;
    private CapsuleCollider2D capsuleCollider;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        //상태이상 면역
        isImmuneAnyState = true;
    }

    private void ColliderOnOff(bool OnOff)
    {
        if (capsuleCollider != null)
            capsuleCollider.enabled = OnOff;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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
            this.gameObject.layer = LayerMask.NameToLayer("EnemyTurret");
            this.gameObject.tag = "Enemy";
        }
    }

    public void Initialize(Vector3 position, BulletType bulletType, int hp = 10, float lifeTime = 10f)
    {
        this.bulletType = bulletType;

        if (bulletType == BulletType.PlayerBullet)
        {
            StartCoroutine(TurretLifeTimeRoutine(lifeTime));
            ColliderOnOff(false);
        }
        else if(bulletType == BulletType.EnemyBullet)
        {
            MonsterManager.Instance.AddToList(this.gameObject);
            ColliderOnOff(true);
        }

        this.transform.position = position;

        SetHp(hp);

        SetLayer();

        isDestroy = false;

        SetTurretImage();


    }

    IEnumerator TurretLifeTimeRoutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        TurretDestroy();
    }





    public void FireBullet()
    {
        Vector3 fireDir = Vector3.one;
        float reBoundValue = 5f;
        float bulletSpeed = 5f;


        switch (bulletType)
        {
            case BulletType.PlayerBullet:
                {
                    GameObject nearMonster = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
                    if (nearMonster != null)
                        fireDir = nearMonster.transform.position - this.transform.position;
                    else if (nearMonster == null)
                        fireDir = Vector3.left;

                    bulletSpeed = 10f;
                    reBoundValue = 0f;

                }
                break;
            case BulletType.EnemyBullet:
                {
                    CharacterBase player = GamePlayerManager.Instance.player;
                    if (player != null)
                        fireDir = player.transform.position - this.transform.position;
                    SoundManager.Instance.PlaySoundEffect("Sample");
                }
                break;
        }

      

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
                if (bulletType == BulletType.EnemyBullet)
                    spriteRenderer.flipX = false;
                else
                    spriteRenderer.flipX = true;
            }
            else
            {
                if (bulletType == BulletType.EnemyBullet)
                    spriteRenderer.flipX = true;
                else
                    spriteRenderer.flipX = false;
            }
        }
    }

    public void TurretDestroy()
    {
        if (animator != null)
            animator.SetTrigger("TurretDestroy");
        
        MonsterManager.Instance.DeleteInList(this.gameObject);

    }



    //애니메이션에 연결
    private void AttackOn()
    {
        if (animator != null)
            animator.SetTrigger("TurretAttackOn");

    }


    public override void GetDamage(int damage)
    {
        if (isDestroy == true) return;
        VampiricGunEffect();
        hp -= damage;

        if (hp <= 0)
        {
            isDestroy = true;

            TurretDestroy();

            ColliderOnOff(false);

        }

    }

    private void SetTurretImage()
    {
        string path;

        if (bulletType == BulletType.EnemyBullet)
            path = string.Format("Animators/Object/EnemyTurret");
        else
            path = string.Format("Animators/Object/PlayerTurret");

        RuntimeAnimatorController animController = Resources.Load<RuntimeAnimatorController>(path);
        if (animator != null && animController != null)
        {
            animator.runtimeAnimatorController = animController;
        }
    }
}
