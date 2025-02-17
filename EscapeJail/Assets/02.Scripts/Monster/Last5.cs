﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Last5 : MonsterBase
{


    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Last5;
        hasBullet = true;
        nearestAcessDistance = 5f;
       
    }
    public override void ResetMonster()
    {
        base.ResetMonster();
      
    }

    protected override void StartMyCoroutine()
    {
        StartCoroutine(RandomMovePattern());
        StartCoroutine(FireRoutine());
    }

    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new Last5Bazooka());

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
            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
    }


    public void FireGun()
    {
        FireWeapon();
        Debug.Log("Bang");

    }

}
