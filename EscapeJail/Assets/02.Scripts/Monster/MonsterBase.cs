using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MonsterBase : MonoBehaviour

{   
    //대상 타겟
    private Transform target;

    //컴포넌트   
    protected Vector3 moveDir;
    private Rigidbody2D rb;
    [SerializeField]
    protected Animator animator;

    //속성값 (속도,hp,mp etc...)
    protected int hp =1;
    protected int hpMax = 1;
    protected int attackPower =1;
    protected float moveSpeed = 1f;

    //사정거리 확인용
    protected float activeDistance = 10;
    private bool isActionStart = false;

    //Hud
    protected Image hudImage;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        this.gameObject.tag = "Enemy";
        hudImage = GetComponentInChildren<Image>();
    }
    // Use this for initialization
    protected void Start ()
    {
        if (target == null)
            target = GamePlayerManager.Instance.player.transform;

        rb = GetComponent<Rigidbody2D>();

        AddToList();
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
        Destroy(this.gameObject);
    }

    protected void AddToList()
    {
        MonsterManager.Instance.AddToList(this);
    }

    protected void DeleteInList()
    {
        MonsterManager.Instance.DeleteInList(this);
    }

    protected void OnDestroy()
    {
        DeleteInList();  
    }

    // Update is called once per frame
    protected void Update ()
    {
        ActionCheck();
        MoveRoutine();
    }

    void MoveRoutine()
    {
        MoveToTarget();
    }

    void ActionCheck()
    {
        if (isActionStart == true) return;
        if (Vector3.Distance(this.transform.position, GamePlayerManager.Instance.player.transform.position) < activeDistance)
        {
            isActionStart = true;
        }

    }

    void MoveToTarget()
    {
        if (isActionStart == false) return;

        if (target == null) return;
        moveDir = target.position - this.transform.position;

        if (rb != null)
        {
            rb.velocity = moveDir* moveSpeed;
        }
                

    }

    protected void UpdateHud()
    {
        if (hudImage != null)
        hudImage.fillAmount = (float)hp / (float)hpMax;

    }
}
