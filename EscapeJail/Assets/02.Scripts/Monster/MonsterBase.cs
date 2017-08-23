using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum MonsterName
{
    Mouse1,
    Mouse2
}

public enum MonsterState
{
    Idle,
    Walk,
    Attack,
    Dead
}

[RequireComponent(typeof(Rigidbody2D))]
public class MonsterBase : MonoBehaviour

{
    //대상 타겟
    protected Transform target;

    //컴포넌트   
    protected Vector3 moveDir;
    private Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    //속성값 (속도,hp,mp etc...)
    protected MonsterName monsterName;
    protected int hp = 1;
    protected int hpMax = 1;
    protected int attackPower = 1;
    protected float moveSpeed = 1f;
    protected float nearestAcessDistance = 1f;
    protected bool nowAttack = false;

    //사정거리 확인용
    protected float activeDistance = 10;
    protected bool isActionStart = false;

    //Hud
    protected Image hudImage;

    //무기
    protected WeaponBase nowWeapon;
    [SerializeField]
    protected Transform weaponPosit;

    public void ResetMonster()
    {
        hp = hpMax;            
        nowAttack = false;
        UpdateHud();
    }


    //임시코드------------------------------------------------------------------풀방식으로 수정 필요
    //임시코드------------------------------------------------------------------풀방식으로 수정 필요
    protected void AddToList()
    {
        MonsterManager.Instance.AddToList(this);
    }

    protected void DeleteInList()
    {
        MonsterManager.Instance.DeleteInList(this);
    }

    protected void OnDisable()
    {
        DeleteInList();
    }

    protected void OnEnable()
    {
        AddToList();    
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
    }

    protected void SetUpCustomScript()
    {
        nowWeapon = GetComponentInChildren<WeaponBase>();
    }
    // Use this for initialization
    protected void Start()
    {
        if (target == null)
            target = GamePlayerManager.Instance.player.transform;

        rb = GetComponent<Rigidbody2D>();

     
    }

    protected void InitiailzeMonster()
    {

    }

    protected void SetUpMonsterAttribute()
    {

    }

    protected void SetHp(int hpMax)
    {
        this.hp = hpMax;
        this.hpMax = hpMax;
    }

    public void GetDamage(int damage)
    {
        this.hp -= damage;
        UpdateHud();
        if (hp <= 0)
        {
            SetDie();
        }

    }

    protected void SetDie()
    {
        //임시코드
        this.gameObject.SetActive(false);
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
        if (isActionStart == false) return;
        if (rb == null) return;
        if (target == null) return;     


        if (IsInAcessArea(nearestAcessDistance) == true)
        {
            //flipx를 위해서 방향계산만 해줌
            CalculateMoveDIr();
            SetAnimation(MonsterState.Idle);
            rb.velocity = Vector3.zero;
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
