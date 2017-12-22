using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Mouse4 : MonsterBase
{

    float fireDelay = 3f;
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Mouse4;
        hasBullet = true;
        nearestAcessDistance = 1f;
    
    }

    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new MouseRifle());

    }


    public override void ResetMonster()
    {
        base.ResetMonster();
      
        AttackOn();

    }

    protected override void StartMyCoroutine()
    {
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
        while (true)
        {
            for (int i = 0; i < 7; i++)
            {
                FireWeapon();
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(fireDelay);
            fireDelay = Random.Range(2f, 3.5f);
        }
    }


    protected override void FireWeapon()
    {
        if (nowWeapon != null)
            nowWeapon.FireBullet(this.transform.position, Vector3.zero);
    }

}
