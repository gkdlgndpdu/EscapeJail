﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Last1 : MonsterBase
{
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Last1;
   
        nearestAcessDistance = 8f;
        moveSpeed = 2f;
     
    } 
    public override void ResetMonster()
    {
        base.ResetMonster();
        
        AttackOff();
    }

    protected override void StartMyCoroutine()
    {
        StartCoroutine(RandomMovePattern(1f,1f));
        StartCoroutine(FireRoutine());
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
        yield return new WaitForSeconds(Random.Range(1f, 3f));
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
