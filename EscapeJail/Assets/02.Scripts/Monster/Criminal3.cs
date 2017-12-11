using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal3 : MonsterBase
{
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Criminal3;
     
        nearestAcessDistance = 1f;
        weaponPosit.gameObject.SetActive(false);
        attackDelay = 1f;
        moveSpeed = 1.5f;

    }

    protected override void StartMyCoroutine()
    {
        StartCoroutine(PathFindRoutine());
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
        SetAnimation(MonsterState.Attack);  
        yield return new WaitForSeconds(attackDelay);
        nowAttack = false;
    }


    public void OnDisable()
    {     
        AttackOff();
    }



}
