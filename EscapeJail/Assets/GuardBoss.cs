using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BossEventQueue))]
public class GuardBoss : BossBase
{
    //컴포넌트
    private Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

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
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetHp(10);
        RegistPatternToQueue();
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

        if (animator != null)
            animator.SetTrigger(action.ToString());

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

         //bossEventQueue.AddEvent("FireMissilePattern");
        bossEventQueue.AddEvent("FireMgPattern");
        //bossEventQueue.AddEvent("MoveAttackPattern");

    }
    #region Pattern
    private IEnumerator FireMissilePattern()
    {

        yield return null;
    }

    private IEnumerator FireMgPattern()
    {
        int fireTimes = 100;
        float bulletSpeed = 8f;

        //애니메이션
        Action(Actions.FireMg);
        yield return new WaitForSeconds(1.0f);

        Vector3 firstFireDirection1 =  Vector3.right;
        Vector3 firstFireDirection2 = Vector3.right;
        for (int i = 0; i < fireTimes; i++)
        {
            firstFireDirection1 = Quaternion.Euler(0f, 0f, 360f / (float)fireTimes)* firstFireDirection1;
            firstFireDirection2 = Quaternion.Euler(0f, 0f,360f- 360f / (float)fireTimes) * firstFireDirection2;

            Bullet bullet1 = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet1 != null)
                {
                    bullet1.gameObject.SetActive(true);
                    bullet1.Initialize(bulletFirePos1.transform.position, firstFireDirection1.normalized, bulletSpeed, BulletType.EnemyBullet);
                    bullet1.InitializeImage("white", false);
                    bullet1.SetEffectName("revolver");
                }

                Bullet bullet2 = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet1 != null)
                {
                    bullet2.gameObject.SetActive(true);
                    bullet2.Initialize(bulletFirePos2.transform.position, firstFireDirection2.normalized, bulletSpeed, BulletType.EnemyBullet);
                    bullet2.InitializeImage("white", false);
                    bullet2.SetEffectName("revolver");
                }
            
            yield return new WaitForSeconds(0.02f);
        }


        //애니메이션 종료
        Action(Actions.EndMg);

        yield return new WaitForSeconds(2.0f);

    }

    private  IEnumerator MoveAttackPattern()
    {
        Debug.Log("MoveAttackPattern");
        yield return new WaitForSeconds(2.0f);
    }

    #endregion


}
