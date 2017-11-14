using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Last6 : MonsterBase
{
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Last6;
  
        nearestAcessDistance = 5f;
    
        moveSpeed = 2f;
    }
    public override void ResetMonster()
    {
        base.ResetMonster();
        //  StartCoroutine(RandomMovePattern());
        nearestAcessDistance = Random.Range(3f,6f);
     
        AttackOn();
    }

    protected override void StartMyCoroutine()
    {
        StartCoroutine(FireRoutine());
        StartCoroutine(RandomMovePattern(0.5f, 1f));
    }

    protected override void SetWeapon()
    {        
        nowWeapon.ChangeWeapon(new Last6Rifle());
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
            for(int i = 0; i < 2; i++)
            {
                FireGun();
                yield return new WaitForSeconds(0.5f);
            }
     
            if (rb != null)
                rb.velocity = Vector3.zero;
            //
            yield return new WaitForSeconds(1f);
        }
    }


    public void FireGun()
    {
        FireWeapon();

    }


}