using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BossEventQueue))]
[RequireComponent(typeof(Rigidbody2D))]
public class GuardBoss : BossBase
{
    //컴포넌트
    private Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    //발사위치
    [SerializeField]
    private Transform missileFirePos1;
    [SerializeField]
    private Transform missileFirePos2;
    [SerializeField]
    private Transform bulletFirePos1;
    [SerializeField]
    private Transform bulletFirePos2;

    //인스펙터에서 할당
    public List<Transform> moveList;

    private new void Awake()
    {
        base.Awake();
        SetComponent();

        SetHp(100);
        RegistPatternToQueue();
    }

    private void SetComponent()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public enum Actions
    {
        FireMissile,
        EndMissile,
        FireMg,
        EndMg,
        DeadTrigger,
        Idle,
        Walk
    }

    public override void StartBossPattern()
    {
        base.StartBossPattern();


        if (bossEventQueue != null)
            bossEventQueue.StartEventQueue();
    }
    private void Action(Actions action)
    {

        if (action != Actions.Idle && action != Actions.Walk)
        {
            if (animator != null)
                animator.SetTrigger(action.ToString());
        }
        else
        {
            if(action == Actions.Walk)
            {
                if (animator != null)
                    animator.SetFloat("Speed", 1.0f);
            }
            else if(action == Actions.Idle)
            {
                if (animator != null)
                    animator.SetFloat("Speed", 0f);
            }
        }
  

    }

    public override void GetDamage(int damage)
    {
        hp -= damage;

        if (bosshpBar != null)
            bosshpBar.UpdateBar(hp, hpMax);
        if (hp <= 0)
        {
            BossDie();
        }

    }

    private void RegistPatternToQueue()
    {

        bossEventQueue.Initialize(this, EventOrder.InOrder);

        bossEventQueue.AddEvent("MoveAttackPattern");
        bossEventQueue.AddEvent("FireMissilePattern");
        bossEventQueue.AddEvent("FireMgPattern");


    }
    #region Pattern
    private IEnumerator FireMissilePattern()
    {

        int fireTimes = 100;
        float bulletSpeed = 10f;

        //애니메이션
        Action(Actions.FireMissile);
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 10; i++)
        {
            Vector3 firstDirection = GamePlayerManager.Instance.player.transform.position-this.transform.position;


            for (int j = 0; j < 3; j++)
            {
                Vector3 fireDirection = Quaternion.Euler(0f, 0f, -5f+j*5f) * firstDirection;
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize(missileFirePos1.transform.position, fireDirection.normalized, bulletSpeed, BulletType.EnemyBullet, 1f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }

            }

            for (int j = 0; j < 3; j++)
            {
                Vector3 fireDirection = Quaternion.Euler(0f, 0f, -5f + j * 5f) * firstDirection;
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize(missileFirePos2.transform.position, fireDirection.normalized, bulletSpeed, BulletType.EnemyBullet, 1f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }

            }

            yield return new WaitForSeconds(0.3f);
        }


        //애니메이션 종료
        Action(Actions.EndMissile);
        yield return new WaitForSeconds(2.0f);
    }

    private IEnumerator FireMgPattern()
    {
        int fireTimes = 100;
        float bulletSpeed = 5f;
        float endDelay = 2f;

        //애니메이션
        Action(Actions.FireMg);
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 20; i++)
        {
            Vector3 firstDirection = Vector3.up;
            if (i % 2 == 0)
                firstDirection = Quaternion.Euler(0f, 0f, 15f) * firstDirection;

            for (int j = 0; j < 12; j++)
            {
               Vector3 fireDirection1 = Quaternion.Euler(0f, 0f, 30f * j) * firstDirection;

                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {                 
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize(bulletFirePos1.transform.position, fireDirection1.normalized, bulletSpeed, BulletType.EnemyBullet,0.5f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }

                Bullet bullet2 = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet2.gameObject.SetActive(true);
                    bullet2.Initialize(bulletFirePos2.transform.position, fireDirection1.normalized, bulletSpeed, BulletType.EnemyBullet, 0.5f);
                    bullet2.InitializeImage("white", false);
                    bullet2.SetEffectName("revolver");
                }


            }

            yield return new WaitForSeconds(0.4f);
        }


        //애니메이션 종료
        Action(Actions.EndMg);
        yield return new WaitForSeconds(endDelay);

    }

    private IEnumerator MoveAttackPattern()
    {
        Action(Actions.Walk);

        Transform playerTr = GamePlayerManager.Instance.player.transform;
        float moveSpeed = 2f;
        float bulletSpeed = 12f;

         yield return new WaitForSeconds(2.0f);
     
        for(int i = 0; i < 50; i++)
        {
            //이동
            Vector3 moveDir = playerTr.position - this.transform.position;
            rb.velocity = moveDir.normalized * moveSpeed;


            //사격
            Vector3 fireDir = playerTr.position - this.transform.position;
            fireDir = Quaternion.Euler(0f, 0f, Random.Range(-10f, 10f))* fireDir;
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.gameObject.SetActive(true);
                bullet.Initialize(this.transform.position, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet, 1f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
            }

            yield return new WaitForSeconds(0.1f);
        }

        rb.velocity = Vector3.zero;
        Action(Actions.Idle);
        yield return new WaitForSeconds(2.0f);
    }

    #endregion


}
