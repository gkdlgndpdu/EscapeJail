using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonsterBase
{
    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Mouse1;
        SetHp(7);
        nearestAcessDistance = 1f;
        weaponPosit.gameObject.SetActive(false);
        attackDelay = 1f;
        moveSpeed = 2f;

    }

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetUpMonsterAttribute();

    }

    private new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    private void Update()
    {
        NearAttackRotate();
        if (canMove() == false) return;
        MoveToTarget();  
        NearAttackLogic();
    }



    protected override IEnumerator AttackRoutine()
    {

        nowAttack = true;
        yield return new WaitForSeconds(0.4f); //애니메이션 재생시간
        SetAnimation(MonsterState.Attack);
        AttackOn();
        yield return new WaitForSeconds(1.0f); //애니메이션 재생시간
        AttackOff();
        yield return new WaitForSeconds(attackDelay - 1.0f);
        nowAttack = false;
    }





}
