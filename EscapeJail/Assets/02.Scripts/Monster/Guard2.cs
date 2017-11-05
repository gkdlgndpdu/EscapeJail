using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Guard2 : MonsterBase
{
    private float fireDelay = 1f;
    private float rageFireDelay = 0.3f;
    private float originFireDelay = 0f;
    private float originSpeed = 0f;
    private float rageSpeed = 3f;
    private bool isRageOn = false;
    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Guard2;
      
        nearestAcessDistance = 5f;
        SetWeapon();

    }
    public override void ResetMonster()
    {
        base.ResetMonster();
        StartCoroutine(RandomMovePattern());
        StartCoroutine(FireRoutine());
        RageOnOff(false);
        AttackOn();

    }

  

    private void RageOnOff(bool OnOff)
    {
        isRageOn = OnOff;

        if (OnOff == true)
        {
            GameObject target = spriteRenderer.gameObject;
            iTween.ColorTo(target, iTween.Hash("loopType", "pingPong", "Time", 0.1f, "Color", Color.red));

            this.moveSpeed = rageSpeed;
            this.fireDelay = rageFireDelay;       

        }
        else if (OnOff == false)
        {
            GameObject target = spriteRenderer.gameObject;
            iTween.ColorTo(target, Color.white, 0.1f);

            this.moveSpeed = originSpeed;
            this.fireDelay = originFireDelay;

        }


    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);

        if (hp <= hpMax / 2 && isRageOn == false)
            RageOnOff(true);

    }

    private void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new GuardRifle());

    }

  


    private new void Awake()
    {
        base.Awake();
        SetOriginData();
    }

    private void SetOriginData()
    {
        originSpeed = moveSpeed;
        originFireDelay = fireDelay;

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
        yield return new WaitForSeconds(1.0f);

        while (true)
        {     

            for (int i = 0; i < 5; i++)
            {
                FireWeapon();
                 yield return new WaitForSeconds(0.08f);
            }


            yield return new WaitForSeconds(fireDelay);
        }
    }
    

  
}
