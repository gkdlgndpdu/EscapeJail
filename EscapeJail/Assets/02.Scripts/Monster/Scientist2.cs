using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Scientist2 : MonsterBase
{

    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Scientist2;
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
        nowWeapon.ChangeWeapon(new Scientist_GasGun());

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
           //
            yield return new WaitForSeconds(3f);
        }
    }
 

    public void FireGasGun()
    {
        FireWeapon();
        Debug.Log("Bang");

    }




}
