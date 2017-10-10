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
    protected float attackDelay =0f;
    protected bool isDead = false;
    //사정거리 확인용
    protected float activeDistance = 10;
    protected bool isActionStart = false;

    //Hud
    protected Image hudImage;

    //무기
    protected WeaponHandler nowWeapon;
    [SerializeField]
    protected Transform weaponPosit;

    [SerializeField]
    protected AttackObject attackObject;

    public void ResetMonster()
    {
        hp = hpMax;            
        nowAttack = false;
        isDead = false;
        if (capsuleCollider != null)
            capsuleCollider.enabled = true;

        AddToList();

        UpdateHud();
    }

    


    //임시코드------------------------------------------------------------------풀방식으로 수정 필요
    //임시코드------------------------------------------------------------------풀방식으로 수정 필요
    protected void AddToList()
    {
        if(MonsterManager.Instance!=null)
        MonsterManager.Instance.AddToList(this.gameObject);
    }

    protected void DeleteInList()
    {
        if (MonsterManager.Instance != null)
            MonsterManager.Instance.DeleteInList(this.gameObject);
    }


    public virtual void StartFirstAction()
    {
        StartCoroutine(FireRoutine());
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

    public override void GetDamage(int damage)
    {
        this.hp -= damage;
        UpdateHud();
        if (hp <= 0)
        {
            SetDie();
        }

    }

    protected virtual IEnumerator FireRoutine()
    {
        yield return null;
    }

    protected void OnDisable()
    {
        isDead = false;
    }

    private void SetDie()
    {
        Debug.Log("die들어옴");

        isDead = true;

        if (rb != null)
            rb.velocity = Vector3.zero;

        if (capsuleCollider != null)
            capsuleCollider.enabled = false;

        if (animator != null)
            animator.SetTrigger("DeadTrigger");

        Invoke("ObjectOff", 3f);

        DeleteInList();

        StopAllCoroutines();
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




    protected void ActionCheck()
    {
        if (isActionStart == true) return;
        if (GetDistanceToPlayer() < activeDistance)
        {
            ActionStart();
        }

    }

    protected float GetDistanceToPlayer()
    {
        return Vector3.Distance(this.transform.position, GamePlayerManager.Instance.player.transform.position);
    }

    void ActionStart()
    {
        isActionStart = true;
    }

    protected bool IsInAcessArea(float AcessValue)
    {
        return GetDistanceToPlayer() <= AcessValue;
    }

    protected void MoveToTarget()
    {
        if (rb == null) return;
        
        rb.velocity = Vector3.zero;

        if (isActionStart == false) return;
        if (target == null) return;
        if (nowAttack == true) return;       
      
        if (IsInAcessArea(nearestAcessDistance) == true)
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
        if (IsInAcessArea(nearestAcessDistance) == true && nowAttack == false)
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
                } break;
            case MonsterState.Walk:
                {
                    animator.SetFloat("Speed", 1f);
                } break;
            case MonsterState.Attack:
                {
                    animator.SetTrigger("AttackTrigger");
                } break;
            case MonsterState.Dead:
                {
                    animator.SetTrigger("DeadTrigger");
                } break;
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

  

}
