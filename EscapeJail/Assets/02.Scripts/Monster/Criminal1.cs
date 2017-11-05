using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Criminal1 : MonsterBase
{
 
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Criminal1;
        
        nearestAcessDistance = 5f;
        SetWeapon();
    }
    public override void ResetMonster()
    {
        base.ResetMonster();
        StartCoroutine(RandomMovePattern());
        StartCoroutine(FireRoutine());
        AttackOn();
    }

    private void SetWeapon()
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
        yield return new WaitForSeconds(Random.Range(2f, 2.5f));

        while (true)
        {        
            for (int i = 0; i < 3; i++)
            {
                 FireWeapon();
                yield return new WaitForSeconds(0.3f);
            }
       

            yield return new WaitForSeconds(Random.Range(2.5f,3f));
        }
    }

    

}
