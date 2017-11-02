using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//박치기형
public class Mouse3 : MonsterBase
{

    private float RushPower = 5f;
    private float RushAfterDelay = 1f;
    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Mouse2;
        SetHp(10);
        nearestAcessDistance = 3f;

    } 

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetUpMonsterAttribute();
    }
    

    protected new void OnEnable()
    {
        base.OnEnable();

    }

    private new void Awake()
    {
        base.Awake();

    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove() == false) return;
        if (nowAttack == true) return;

        MoveToTarget();
        NearAttackLogic();


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
