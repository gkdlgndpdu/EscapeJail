using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonsterBase
{
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Mouse1;
   
        nearestAcessDistance = 1f;
        weaponPosit.gameObject.SetActive(false);
        attackDelay = 1f;
        moveSpeed = 1f;

    }

    public override void ResetMonster()
    {
        base.ResetMonster();
        StartMyCoroutine();
    }

    protected override void StartMyCoroutine()
    {
        StartCoroutine(PathFindRoutine());
    }


    // Update is called once per frame
    private void Update()
    {
        //임시코드
        if (hasWall == true) return;
        //임시코드


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
