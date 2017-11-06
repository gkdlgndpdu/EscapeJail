using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Criminal5 : MonsterBase
{

    public override void ResetMonster()
    {
        base.ResetMonster();
        StartCoroutine(RandomMovePattern());
        StartCoroutine(FireRoutine());
        AttackOn();
    }
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Criminal5;
      
        nearestAcessDistance = 5f;
      
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
                yield return new WaitForSeconds(0.2f);
            }


            yield return new WaitForSeconds(Random.Range(1f,3f));
        }
    }

    
}
