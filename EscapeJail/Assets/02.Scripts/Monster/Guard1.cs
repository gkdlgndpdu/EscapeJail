using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Guard1 : MonsterBase
{

    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Guard1;
      
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
        nowWeapon.ChangeWeapon(new GuardPistol());

    }

  
    protected new void OnEnable()
    {
        base.OnEnable();
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(true);
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
            FireWeapon();
            yield return new WaitForSeconds(1f);
        }
    }

  

 

}
