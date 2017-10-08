using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CriminalBoss : BossBase
{
    //컴포넌트
    private Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;


    //인스펙터에서 할당
    public List<Transform> moveList;

    

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


    private new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetHp(10);
    }

    private IEnumerator FirePattern1()
    {
        for(int i = 0; i < 30; i++)
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
        yield return null;
    }

    public override void StartBossPattern()
    { 
        base.StartBossPattern();
        Debug.Log("CriminalPattern Start");
        StartCoroutine(FirePattern1());
    
    }
}

