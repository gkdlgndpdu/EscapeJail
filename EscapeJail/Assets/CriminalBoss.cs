using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BossEventQueue))]
public class CriminalBoss : BossBase
{
    //컴포넌트
    private Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;


    //인스펙터에서 할당
    public List<Transform> moveList;


    private new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetHp(10);
        RegistPatternToQueue();
    }

 

    public enum Actions
    {
        FireStart,
        FireEnd,
        Walk,
        WalkEnd           
    }

    public override void StartBossPattern()
    {
        base.StartBossPattern();
        Debug.Log("CriminalPattern Start");

        if(bossEventQueue!=null)
        bossEventQueue.StartEventQueue();
    }

 

    private void Action(Actions action)
    {
        switch (action)
        {
            case Actions.FireStart:
                {
                    if (animator != null)
                        animator.SetTrigger("FireTrigger");
                        
                } break;
            case Actions.FireEnd:
                {
                    if (animator != null)
                        animator.SetTrigger("FireEndTrigger");
                }
                break;
            case Actions.Walk:
                {
                    if (animator != null)
                        animator.SetFloat("Speed",1f);

                }
                break;
            case Actions.WalkEnd:
                {
                    if (animator != null)
                        animator.SetFloat("Speed", 0f);
                } break;
                
        }
    }

    public override void GetDamage(int damage)
    {
        hp -= damage;
        Debug.Log("boss hp : " + hp);
        //자식에서 구현!
        if (bosshpBar != null)
            bosshpBar.UpdateBar(hp, hpMax);
        if (hp <= 0)
        {
            BossDie();
        }

    }

    private void RegistPatternToQueue()
    {
        
        bossEventQueue.Initialize(this, EventOrder.Random);

        bossEventQueue.AddEvent("FirePattern1");

    }


    #region Pattern

    private IEnumerator FirePattern1()
    {
        Action(Actions.FireStart);
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 30; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < 8; j++)
                {
                    Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                    if (bullet != null)
                    {
                        Vector3 fireDir = Quaternion.Euler(0f, 0f, -22.5f + (float)j * 45f) * Vector3.right;
                        float bulletSpeed = 4f;

                        bullet.gameObject.SetActive(true);
                        bullet.Initialize(this.transform.position, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet);
                        bullet.InitializeImage("white", false);
                        bullet.SetEffectName("revolver");
                    }
                }

            }
            else
            {
                for (int j= 0; j < 8; j++)
                {
                    Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                    if (bullet != null)
                    {
                        Vector3 fireDir = Quaternion.Euler(0f, 0f,   (float)j * 45f) * Vector3.right;
                        float bulletSpeed = 4f;

                        bullet.gameObject.SetActive(true);
                        bullet.Initialize(this.transform.position, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet);
                        bullet.InitializeImage("white", false);
                        bullet.SetEffectName("revolver");
                    }
                }
            }

            yield return new WaitForSeconds(0.3f);
        }

        Action(Actions.FireEnd);
        yield return null;
    }
    #endregion

}

