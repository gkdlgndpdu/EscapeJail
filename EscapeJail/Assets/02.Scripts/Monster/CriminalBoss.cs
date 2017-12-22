using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CriminalBoss : BossBase
{
    //인스펙터에서 할당
    public List<Transform> moveList;

    [SerializeField]
    private CrossBullet crossBullet;

    [SerializeField]
    private Transform firePos;

    private new void Awake()
    {
        base.Awake();
        SetHp(400);
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

        if (bossEventQueue != null)
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

                }
                break;
            case Actions.FireEnd:
                {
                    if (animator != null)
                        animator.SetTrigger("FireEndTrigger");
                }
                break;
            case Actions.Walk:
                {
                    if (animator != null)
                        animator.SetFloat("Speed", 1f);

                }
                break;
            case Actions.WalkEnd:
                {
                    if (animator != null)
                        animator.SetFloat("Speed", 0f);
                }
                break;

        }
    }

    protected override void BossDie()
    {
        base.BossDie();
        crossBullet.RotationOnOff(false);

    }


    private void RegistPatternToQueue()
    {

        bossEventQueue.Initialize(this, EventOrder.Random);

        //  bossEventQueue.AddEvent("FirePattern1");
        bossEventQueue.AddEvent("FirePattern3");
        bossEventQueue.AddEvent("FirePattern2");
        bossEventQueue.AddEvent("FirePattern1");
    }


    #region Pattern

    //회오리
    private IEnumerator FirePattern1()
    {
        Action(Actions.FireStart);

        float eachFireDelay = 0.2f;
        float endDelay = 1f;
        float bulletSpeed = 3f;
        int fireBulletNum = 30;
        float bulletSize = 0.6f;

        yield return new WaitForSeconds(0.5f);

        Vector3 firstFireDir = Vector3.right;

        for (int i = 0; i < fireBulletNum; i++)
        {

            if (i % 2 == 0)
            {

                for (int j = 0; j < 8; j++)
                {
                    Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                    if (bullet != null)
                    {
                        Vector3 fireDir = Quaternion.Euler(0f, 0f, -22.5f + (float)j * 45f) * firstFireDir;
                        bullet.gameObject.SetActive(true);
                        bullet.Initialize(firePos.position, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet, bulletSize);
                        bullet.InitializeImage("white", false);
                        bullet.SetEffectName("revolver");
                    }
                }
            }
            else
            {
                for (int j = 0; j < 8; j++)
                {
                    Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                    if (bullet != null)
                    {
                        Vector3 fireDir = Quaternion.Euler(0f, 0f, (float)j * 45f) * firstFireDir;
                        bullet.gameObject.SetActive(true);
                        bullet.Initialize(firePos.position, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet,bulletSize);
                        bullet.InitializeImage("white", false);
                        bullet.SetEffectName("revolver");
                    }
                }
            }


            firstFireDir = Quaternion.Euler(0f, 0f, 11.25f) * firstFireDir;

            yield return new WaitForSeconds(eachFireDelay);
        }

        Action(Actions.FireEnd);

        yield return new WaitForSeconds(endDelay);
    }

    private IEnumerator FirePattern2()
    {
        Action(Actions.FireStart);

        float fireDelay = 0.15f;
        float bulletSpeed = 7f;
        float endDelay = 1f;
        int fireBulletNum = 25;
        float reBoundValue = 30f;

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < fireBulletNum; i++)
        {
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                Vector3 fireDir = GamePlayerManager.Instance.player.transform.position - this.transform.position;

                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue))* fireDir;

                bullet.gameObject.SetActive(true);
                bullet.Initialize(firePos.position, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
            }
            yield return new WaitForSeconds(fireDelay);
        }

        Action(Actions.FireEnd);

        yield return new WaitForSeconds(endDelay);
    }

    private IEnumerator FirePattern3()
    {
        float endDelay = 1f;

        Action(Actions.FireStart);
        crossBullet.RotationOnOff(true);
        //십자가 돌리기
        yield return new WaitForSeconds(8f);
        crossBullet.RotationOnOff(false);
        Action(Actions.FireEnd);

        yield return new WaitForSeconds(endDelay);
    }
    #endregion

}

