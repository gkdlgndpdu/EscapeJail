using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist1 : MonsterBase
{
    private ScientistState scientistState = ScientistState.Normal;
    private int originHp = 5;
    private float RushPower = 7f;
    private float transformMoveSpeed = 2f;
    private float RushAfterDelay = 1f;
    private Vector3 orginSize = Vector3.one;
    private Vector3 transformSize = Vector3.one * 2f;

    private bool UseBullet = false;
    //변신중에 무적상태가됨
    private bool isImmune = false;

    //도망
    private float avoidTime = 3f;
    private float avoidSpeed = 6f;

    private enum ScientistState
    {
        Normal,
        Avoid,
        Transform
    }

    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Scientist1;
        
        nearestAcessDistance = 4f;
        weaponPosit.gameObject.SetActive(false);
        attackDelay = 1f;
        moveSpeed = 4f;
    }

    public override void ResetMonster()
    {
        base.ResetMonster();
        SetHp(originHp);
        SetAnimation(MonsterState.Idle);
        this.transform.localScale = Vector3.one;
        scientistState = ScientistState.Normal;
        UseBullet = false;
        isImmune = false;
    }

    protected new void Start()
    {
        base.Start();
        originHp = hp;

    }

    private IEnumerator TransformRoutine()
    {

        //랜덤이동

        yield break;
    }

   

    private void SetTransform()
    {
        if (scientistState == ScientistState.Transform) return;

        if (rb != null)
            rb.velocity = Vector3.zero;      

        if (animator != null)
            animator.SetTrigger("TransformTrigger");

        //변신중에는 무적
        isImmune = true;

        AbilityChange();
    
    }
    public void TransformEnd()
    {
        this.scientistState = ScientistState.Transform;
        isImmune = false;
    }

    private void AbilityChange()
    {
        SetHp(originHp * 3);
        moveSpeed = transformMoveSpeed;
        this.transform.localScale = Vector3.one * 2f;
    }

    private void SpeedUp()
    {
        moveSpeed = avoidSpeed;

        //
        //도망패턴 시작
        //
    }

    public override void GetDamage(int damage)
    {
        if (isImmune == true)
        {
            Debug.Log("변신중에는 무적입니다");
            return;
        }

        //한대맞으면 그떄부터 도망감
        if (scientistState == ScientistState.Normal)
        {
            scientistState = ScientistState.Avoid;
            StartCoroutine(AvoidRoutine());
        }

        this.hp -= damage;
        UpdateHud();
        if (hp <= 0)
        {
            if ((scientistState !=ScientistState.Transform)&& UseBullet==false)
            {
                UseBullet = true;
                for (int i = 0; i < 12; i++)
                {
                    Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                    if (bullet != null)
                    {
                        Vector3 fireDIr = Quaternion.Euler(new Vector3(0f, 0f, i * 30)) * Vector3.right;
                        bullet.gameObject.SetActive(true);
                        bullet.Initialize(this.transform.position, fireDIr.normalized, 8f, BulletType.EnemyBullet,0.7f);
                        bullet.InitializeImage("white", false);
                        bullet.SetEffectName("revolver");
                        bullet.SetBloom(true, Color.red);
                    }
                }
            }

            SetDie();
        }

    }

    private IEnumerator AvoidRoutine()
    {
        float count = 0f;
        while (true)
        {
            Vector3 moveDir = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)) * Vector3.right;

            this.moveDir = moveDir;

            if (rb != null)
                rb.velocity = moveDir.normalized * moveSpeed;

            SetAnimation(MonsterState.Walk);

            FlipCharacterByMoveDir();

            count += 0.5f;
            if (count >= avoidTime)
            {

                SetTransform();
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }

    }


    // Update is called once per frame
    private void Update()
    {
        if (canMove() == false) return;
        if (nowAttack == true) return;
              
        if (scientistState == ScientistState.Transform)
        {
            MoveToTarget();
            NearAttackLogic();
        }
        else if(scientistState==ScientistState.Normal)
        {
            if (rb != null)
                rb.velocity = Vector3.zero; 
        }

    }



    protected override IEnumerator AttackRoutine()
    {
        nowAttack = true;

        SetAnimation(MonsterState.Attack);
        //선딜
        yield return new WaitForSeconds(0.5f);
        AttackOn();

        Vector3 RushDir = GamePlayerManager.Instance.player.transform.position - this.transform.position;
        RushDir.Normalize();

        if (rb != null)
            rb.velocity = RushDir * RushPower;
        yield return new WaitForSeconds(1.0f);
        if (animator != null)
            animator.SetTrigger("AttackEndTrigger");
        if (rb != null)
            rb.velocity = Vector3.zero;
        AttackOff();

        yield return new WaitForSeconds(RushAfterDelay);
        nowAttack = false;

    }





}
