using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum MonsterName
{
    Mouse1,
    Mouse2,
    Mouse3,
    Mouse4,
    Criminal1,
    Criminal2,
    Criminal3,
    Criminal4,
    Criminal5,
    Guard1,
    Guard2,
    Guard3,
    Guard4,
    Scientist1,
    Scientist2,
    Scientist3,
    Scientist4,
    Slime,
    EndMonster
}

public enum MonsterState
{
    Idle,
    Walk,
    Attack,
    Dead
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class MonsterBase : CharacterInfo

{
    //대상 타겟
    protected Transform target;

    //컴포넌트   
    protected Vector3 moveDir;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected CapsuleCollider2D capsuleCollider;

    //속성값 (속도,hp,mp etc...)
    protected MonsterName monsterName;
    protected int attackPower = 1;
    protected float moveSpeed = 1f;
    protected float nearestAcessDistance = 1f;
    protected bool nowAttack = false;
    protected float attackDelay = 0f;
    protected bool isDead = false;
    protected bool isMoveRandom = false;
    //사정거리 확인용
    protected float activeDistance = 10;

    //Hud
    protected Image hudImage;

    //무기
    protected WeaponHandler nowWeapon;
    [SerializeField]
    protected Transform weaponPosit;

    [SerializeField]
    protected AttackObject attackObject;

    /// <summary>
    /// 풀에서 나올때의 생성자
    /// </summary>
    public virtual void ResetMonster()
    {

        ResetCondition();
        hp = hpMax;
        nowAttack = false;
        isDead = false;
        if (capsuleCollider != null)
            capsuleCollider.enabled = true;

        isMoveRandom = false;

        AddToList();

        UpdateHud();
    }


    protected bool canMove()
    {
        //죽었거나             랜덤이동중이면
        if (isDead == true || isMoveRandom == true) return false;

        return true;
    }


    //임시코드------------------------------------------------------------------풀방식으로 수정 필요
    //임시코드------------------------------------------------------------------풀방식으로 수정 필요
    protected void AddToList()
    {
        if (MonsterManager.Instance != null)
            MonsterManager.Instance.AddToList(this.gameObject);
    }

    protected void DeleteInList()
    {
        if (MonsterManager.Instance != null)
            MonsterManager.Instance.DeleteInList(this.gameObject);
    }

    protected void OnEnable()
    {
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(false);


    }
    //임시코드------------------------------------------------------------------풀방식으로 수정 필요
    //임시코드------------------------------------------------------------------풀방식으로 수정 필요

    protected void Awake()
    {
        SetUpComponent();
        SetUpCustomScript();
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        this.gameObject.tag = "Enemy";


    }

    protected void SetUpComponent()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        hudImage = GetComponentInChildren<Image>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    protected void SetUpCustomScript()
    {
        nowWeapon = GetComponentInChildren<WeaponHandler>();
    }
    // Use this for initialization
    protected void Start()
    {
        if (target == null)
            target = GamePlayerManager.Instance.player.transform;

        rb = GetComponent<Rigidbody2D>();

    }

    protected void SetAttackPower(int power)
    {
        if (attackObject != null)
            attackObject.Initialize(power);
    }

    protected void InitiailzeMonster()
    {

    }

    protected void SetUpMonsterAttribute()
    {

    }

    protected new void SetHp(int hpMax)
    {
        this.hp = hpMax;
        this.hpMax = hpMax;
        UpdateHud();
    }

 

    protected virtual IEnumerator FireRoutine()
    {
        yield return null;
    }

    protected void SetDie()
    {
        //상태
        isDead = true;

        //속도
        if (rb != null)
            rb.velocity = Vector3.zero;

        //충돌체
        if (capsuleCollider != null)
            capsuleCollider.enabled = false;

        //애니메이션
        if (animator != null)
            animator.SetTrigger("DeadTrigger");

        //다시 풀로 돌아가기
        Invoke("ObjectOff", 3f);

        //근접공격대상에서 벗어나게
        DeleteInList();

        //실행중인 모든 코루틴 종료
        StopAllCoroutines();

        //스코어 올려줌
        ScoreBoard.Instance.GetScore();

        //무기꺼중
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(false);

    

    }

    protected void ObjectOff()
    {
        //임시코드
        this.gameObject.SetActive(false);
    }


    public void AttackOn()
    {
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(true);


    }

    public void AttackOff()
    {
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(false);


    }






    protected float GetDistanceToPlayer()
    {
        return Vector3.Distance(this.transform.position, GamePlayerManager.Instance.player.transform.position);
    }



    protected bool IsInAcessArea()
    {
        return GetDistanceToPlayer() <= nearestAcessDistance;
    }

    protected void MoveToTarget()
    {
        if (rb == null) return;

        rb.velocity = Vector3.zero;

        if (target == null) return;
        if (nowAttack == true) return;

        if (IsInAcessArea() == true)
        {
            //flipx를 위해서 방향계산만 해줌
            CalculateMoveDIr();
            SetAnimation(MonsterState.Idle);
            return;
        }

        CalculateMoveDIr();
        rb.velocity = moveDir.normalized * moveSpeed;

        SetAnimation(MonsterState.Walk);


    }

    protected void CalculateMoveDIr()
    {
        moveDir = target.position - this.transform.position;
    }

    protected void UpdateHud()
    {
        if (hudImage != null)
            hudImage.fillAmount = (float)hp / (float)hpMax;

    }

    protected virtual IEnumerator AttackRoutine()
    {
        yield break;
    }

    protected void NearAttackLogic()
    {
        if (IsInAcessArea() == true && nowAttack == false)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    

    protected void SetAnimation(MonsterState state)
    {
        if (animator == null) return;

        FlipCharacterByMoveDir();

        switch (state)
        {
            case MonsterState.Idle:
                {
                    animator.SetFloat("Speed", 0f);
                }
                break;
            case MonsterState.Walk:
                {
                    animator.SetFloat("Speed", 1f);
                }
                break;
            case MonsterState.Attack:
                {
                    animator.SetTrigger("AttackTrigger");
                }
                break;
            case MonsterState.Dead:
                {
                    animator.SetTrigger("DeadTrigger");
                }
                break;
        }

    }

    protected void FlipCharacterByMoveDir()
    {
        //임시
        if (spriteRenderer == null) return;
        if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    protected IEnumerator RandomMoveRoutine(Vector3 direction, int moveDistance)
    {
        isMoveRandom = true;
        Vector3 moveDirection = direction.normalized;
        moveDirection = -moveDirection;
        moveDirection = Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f)) * moveDirection;

        for (int i = 0; i < moveDistance; i++)
        {
            if (rb != null)
                rb.velocity = moveDirection;

            yield return new WaitForSeconds(0.1f);
        }

        isMoveRandom = false;


    }
    protected IEnumerator RandomMovePattern()
    {
        while (true)
        {
            if (isMoveRandom == false)
            {
                nearestAcessDistance = UnityEngine.Random.Range(1f, 5f);
                if (IsInAcessArea() == true)
                {
                    //백무빙
                    isMoveRandom = true;
                    StartCoroutine(RandomBackMove());

                }
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    protected IEnumerator RandomBackMove()
    {
        float moveTime = 2f;
        float count = 0f;
        Vector3 randomDirection = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-90f, 90f)) * -(moveDir.normalized);

        while (true)
        {
            if (rb != null)
                rb.velocity = randomDirection * moveSpeed;

            SetAnimation(MonsterState.Walk);

            count += Time.deltaTime;

            if (count > moveTime)
            {
                isMoveRandom = false;
                SetAnimation(MonsterState.Idle);
                yield break;
            }
            else
                yield return null;
        }
    }

    protected virtual void FireWeapon()
    {
        if (nowWeapon != null)
        {
            Vector3 fireDir = GamePlayerManager.Instance.player.transform.position - this.transform.position;
            nowWeapon.FireBullet(this.transform.position, fireDir.normalized);
        }
    }

    public override void GetDamage(int damage)
    {
        this.hp -= damage;
        UpdateHud();
        if (hp <= 0)
        {
            SetDie();
        }

    }


    protected void RotateWeapon()
    {
        float angle = MyUtils.GetAngle(this.transform.position, target.position);
        if (weaponPosit != null)
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, angle);

        //flip
        if ((angle >= 0f && angle <= 90) ||
              angle >= 270f && angle <= 360)
        {
            if (nowWeapon != null)
                nowWeapon.FlipWeapon(false);
        }
        else
        {
            if (nowWeapon != null)
                nowWeapon.FlipWeapon(true);
        }

    }
}
