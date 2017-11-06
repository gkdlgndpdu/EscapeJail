using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Mouse2 : MonsterBase
{
    float shotDelay = 2.5f;
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Mouse2;
      
        nearestAcessDistance = 5f;
    
        moveSpeed = 1f;
    }

    public override void ResetMonster()
    {
        base.ResetMonster();
        StartCoroutine(FireRoutine());
        AttackOn();
    }

    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new MouseGun());
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
            if (isDead == true) yield break;
            FireWeapon();
            yield return new WaitForSeconds(shotDelay);
        }
    }


}
