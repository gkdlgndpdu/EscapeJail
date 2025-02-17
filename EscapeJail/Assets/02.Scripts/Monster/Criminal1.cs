﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Criminal1 : MonsterBase
{
 
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Criminal1;
        hasBullet = true;
        nearestAcessDistance = 5f;
        SetWeapon();
        moveSpeed = 2;
    }
    public override void ResetMonster()
    {
        base.ResetMonster();
   
        AttackOn();
    }

    protected override void StartMyCoroutine()
    {
        StartCoroutine(RandomMovePattern());
        StartCoroutine(FireRoutine());
    }

    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new CriminalPistol());

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
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        WaitForSeconds ws = new WaitForSeconds(0.3f);
        
        while (true)
        {        
            for (int i = 0; i < 3; i++)
            {
                 FireWeapon();
                yield return ws;
            }
       

            yield return new WaitForSeconds(Random.Range(1.5f,2.5f));
        }
    }

    

}
