using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardBoss : BossBase
{


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

        SetHp(150);
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

        if (rb != null)
            rb.velocity = Vector3.zero;

        float bulletSpeed = 7f;
        float reboundValue = 10f;
        //애니메이션
        Action(Actions.FireMissile);
        SoundManager.Instance.PlaySoundEffect("changewithsiren");
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 10; i++)
        {
            Vector3 firstDirection = GamePlayerManager.Instance.player.transform.position-this.transform.position;
            SoundManager.Instance.PlaySoundEffect("rocket2");

            for (int j = 0; j < 5; j++)
            {
                Vector3 fireDirection = Quaternion.Euler(0f, 0f, -reboundValue + j* reboundValue) * firstDirection;
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize(missileFirePos1.transform.position, fireDirection.normalized, bulletSpeed, BulletType.EnemyBullet, 1f);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }

            }

            for (int j = 0; j < 5; j++)
            {
                Vector3 fireDirection = Quaternion.Euler(0f, 0f, -reboundValue + j * reboundValue) * firstDirection;
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
        if (rb != null)
            rb.velocity = Vector3.zero;

        float bulletSpeed = 3f;
        float endDelay = 2f;
        float bulletSize = 0.7f;

        //애니메이션
        Action(Actions.FireMg);
        yield return new WaitForSeconds(1.0f);
        SoundManager.Instance.PlaySoundEffect("changewithsiren");
        for (int i = 0; i < 5; i++)
        {
            Vector3 firstDirection = Vector3.up;            
            SoundManager.Instance.PlaySoundEffect("explosion3");
            if (i % 2 == 0)
                firstDirection = Quaternion.Euler(0f, 0f, 15f) * firstDirection;

            for (int j = 0; j < 36; j++)
            {
               Vector3 fireDirection1 = Quaternion.Euler(0f, 0f, 10f * j) * firstDirection;

                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {                 
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize(bulletFirePos1.transform.position, fireDirection1.normalized, bulletSpeed, BulletType.EnemyBullet, bulletSize);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }

                Bullet bullet2 = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet2.gameObject.SetActive(true);
                    bullet2.Initialize(bulletFirePos2.transform.position, fireDirection1.normalized, bulletSpeed, BulletType.EnemyBullet, bulletSize);
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
        if (rb != null)
            rb.velocity = Vector3.zero;

        Action(Actions.Walk);
        SoundManager.Instance.PlaySoundEffect("flyattack");

        Transform playerTr = GamePlayerManager.Instance.player.transform;
        float moveSpeed = 1f;
        float bulletSpeed = 8f;
        float reboundValue = 30f;
        yield return new WaitForSeconds(2.0f);
     
        for(int i = 0; i < 50; i++)
        {
            //이동
            Vector3 moveDir = playerTr.position - this.transform.position;
            rb.velocity = moveDir.normalized * moveSpeed;


            //사격
            Vector3 fireDir = playerTr.position - this.transform.position;
            fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reboundValue, reboundValue))* fireDir;
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.gameObject.SetActive(true);
                bullet.Initialize(this.transform.position, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet, 1f);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                SoundManager.Instance.PlaySoundEffect("smg5");
            }

            yield return new WaitForSeconds(0.1f);
        }

        rb.velocity = Vector3.zero;
        Action(Actions.Idle);
        yield return new WaitForSeconds(2.0f);
    }

    #endregion


}
