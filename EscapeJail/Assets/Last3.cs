using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//처음에 시작하면 은신 -> 한번 공격 후에 재은신 반복


public class Last3 : MonsterBase
{
    private bool isHide = false;
    private float hideTime = 3f;
    private bool canAttack = false;

    public override void ResetMonster()
    {
        base.ResetMonster();
        StartCoroutine(hideRoutine());
        isHide = false;
        canAttack = false;
    }


    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Last3;
      
        nearestAcessDistance = 1f;
        weaponPosit.gameObject.SetActive(false);
        attackDelay = 1f;
        moveSpeed = 2f;
    }

    



 

    // Update is called once per frame
    private void Update()
    {

        if (canAttack == false) return;

        NearAttackRotate();
        if (canMove() == false) return;
        MoveToTarget();
        NearAttackLogic();
    }

    private IEnumerator hideRoutine()
    {
        HideOn();

        while (true)
        {
            //잠복중이다
            if (isHide == true)
            {
                yield return new WaitForSeconds(hideTime);
                HideOff();
            }      

            yield return null;
        }
    }


    protected override IEnumerator AttackRoutine()
    {
        nowAttack = true;
        SetAnimation(MonsterState.Attack);
        yield return new WaitForSeconds(attackDelay);
        nowAttack = false;

        HideOn();
    }




    private void MoveToNearPosit()
    {
        this.transform.position = target.transform.position;
    }

    public void HideOn()
    {
        isHide = true;

        HudOnOff(false);

        canAttack = false;

        ColliderOnOff(false);

        if (animator != null)
            animator.SetTrigger("HideOn");

    }

    public void HideOnComplete()
    {
        RemoveInList();
        MoveToSpace();

    }

    //잠시 우주에 갔다와
    private void MoveToSpace()
    {
        this.transform.position = new Vector3(2000f, 2000f, 0f);
    }

    public void HideOff()
    {
        isHide = false;

        MoveToNearPosit();

        HudOnOff(true);

        if (animator != null)
            animator.SetTrigger("HideOff");
    }

    public void HideOffComplete()
    {

        ColliderOnOff(true);
        AddToList();
        canAttack = true;
    }


}
