using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Last1 : MonsterBase
{
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Last1;
   
        nearestAcessDistance = 5f;
     
    } 
    public override void ResetMonster()
    {
        base.ResetMonster();
        StartCoroutine(RandomMovePattern());
        StartCoroutine(FireRoutine());
        AttackOff();
    }

    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new Last1Gun());

    }



    // Update is called once per frame
    private void Update()
    {
        RotateWeapon();
        if (canMove() == false) return;
        MoveToTarget();

    }

    protected override IEnumerator FireRoutine()
    {
        while (true)
        {
            //
            //발사
            SetAnimation(MonsterState.Attack);
            if (rb != null)
                rb.velocity = Vector3.zero;
            //
            yield return new WaitForSeconds(Random.Range(1f,5f));
        }
    }


    public void FireGun()
    {
        FireWeapon();
        Debug.Log("Bang");

    }


}
