using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BossEventQueue))]
public class BossBase : CharacterInfo
{
    protected bool isPatternStart = false;
 
    protected BossModule bossModule;

    protected BossEventQueue bossEventQueue;

    protected bool isBossDie = false;

    protected float moveSpeed = 1f;


    //컴포넌트
    protected Animator animator;
    protected BoxCollider2D boxCollider;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    [SerializeField]
    protected BossHpBar bosshpBar;

    //나머지는 자식에서 구현

        /// <summary>
        /// 하는일 : 보스 UI켜주기, 몬스터매니저에 추가
        /// </summary>
    public virtual void StartBossPattern()
    {
        SetUiOnOff(true);
        isPatternStart = true;
        AddToList();
    }

    protected void AddToList()
    {
        MonsterManager.Instance.AddToList(this.gameObject);
    }

    protected void DeleteInList()
    {
        MonsterManager.Instance.DeleteInList(this.gameObject);
    }

    protected void OnDisable()
    {
        DeleteInList();
    }

    protected void Start()
    {
        SetUiOnOff(false);
    }

    public void SetUiOnOff(bool OnOff)
    {
        if (bosshpBar != null)
            bosshpBar.gameObject.SetActive(OnOff);
    }

    protected void Awake()
    {
        bossModule = GetComponentInParent<BossModule>();
        bossEventQueue = GetComponent<BossEventQueue>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.mass = 100f;

    }

    protected void Initiaize()
    {

    }
    public override void GetDamage(int damage)
    {
        if (isBossDie == true|| isPatternStart==false) return;
        
        hp -= damage;

        if (bosshpBar != null)
            bosshpBar.UpdateBar(hp, hpMax);
        if (hp <= 0)
        {
            isBossDie = true;
            BossDie();
        }

    }


    protected virtual void BossDie()
    {
        StopAllCoroutines();

        if (animator != null)
            animator.SetTrigger("DeadTrigger");
        if (rb != null)
            rb.velocity = Vector3.zero; 
      

        //할꺼 해주고~

        //할꺼 해주고~


        GameManager.Instance.ChangeStage();
     
    }


}
