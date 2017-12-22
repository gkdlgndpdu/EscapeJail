using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Criminal5 : MonsterBase
{
    
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

    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Criminal5;
        hasBullet = true;
        nearestAcessDistance = 5f;
        moveSpeed = 2f;
      
    }
    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new CriminalUzi());

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
            for (int i = 0; i < 5; i++)
            {
                FireWeapon();
                yield return new WaitForSeconds(0.1f);
            }


            yield return new WaitForSeconds(Random.Range(1f,2f));
        }
    }

    
}
