using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;


public class Criminal2 : MonsterBase
{


    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Criminal2;
   
        nearestAcessDistance = 5f;
        moveSpeed = 1f;
  
    }
    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new CriminalShotGun());
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


    // Update is called once per frame
    private void Update()
    {
        RotateWeapon();  
        if (canMove() == false) return;
        MoveToTarget();

    }

    protected override IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(Random.Range(2f, 2.5f));
        while (true)
        {    
            FireWeapon();
            yield return new WaitForSeconds(Random.Range(2.5f, 3f));
        }
 
    }

    
}
