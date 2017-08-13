using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    protected float activeDistance = 10;
    protected float moveSpeed = 1f;
    protected float reactionDistance = 10f;
    private bool isActionStart = false;

    // Use this for initialization
    protected void Start ()
    {
        if (target == null)
            target = GamePlayerManager.Instance.player.transform;

        rb = GetComponent<Rigidbody2D>();

        AddToList();
    }

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
