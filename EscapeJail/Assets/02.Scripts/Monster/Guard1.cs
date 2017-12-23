using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Guard1 : MonsterBase
{

    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Guard1;
        hasBullet = true;
        nearestAcessDistance = 5f;
        moveSpeed = 1.5f;
       
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
        yield return new WaitForSeconds(Random.Range(0.7f, 1.5f));
        while (true)
        {           
            FireWeapon();
            yield return new WaitForSeconds(Random.Range(0.7f,1.5f));
        }
    }

  

 

}
