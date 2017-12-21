using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//박치기형
public class Mouse3 : MonsterBase
{

    private float RushPower = 5f;
    private float RushAfterDelay = 1f;
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Mouse2;

        nearestAcessDistance = 3f;
        moveSpeed = 3f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (nowAttack == false) return;
        if (collision.gameObject.CompareTag("Player") == true)
        {
            if (rb != null)
                rb.velocity = Vector3.zero;

            if (animator != null)
                animator.SetTrigger("AttackEndTrigger");

            AttackOff();
        }
    }



}
