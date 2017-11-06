using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Guard4 : MonsterBase
{

    private bool isSheildOn = false;



    //해당 거리보다 멀리 있으면 실드킴 무조건
    private float shieldOffDistance = 3f;


    public override void ResetMonster()
    {
        base.ResetMonster();
        ShieldEffectOff();
        AttackOn();
        StartCoroutine("FireRoutine");

    }

    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Guard4;
      
        nearestAcessDistance = 3f;

        attackDelay = 1f;
        moveSpeed = 1f;
      

    }

    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new GuardPistol());

    }




    private void ShieldOnOff(bool OnOff)
    {
        if (animator == null) return;

        isSheildOn = OnOff;

        if (OnOff == true)
        {
            animator.SetTrigger("ShieldOn");
            StopCoroutine("FireRoutine");
            if (weaponPosit != null)
                weaponPosit.gameObject.SetActive(false);

        }
        else if (OnOff == false)
        {
            animator.SetTrigger("ShieldOff");
            StartCoroutine("FireRoutine");
            if (weaponPosit != null)
                weaponPosit.gameObject.SetActive(true);

        }
    }




    public override void GetDamage(int damage)
    {
        if (isSheildOn == true)
        {
            ShieldEffectOn();
            return;
            //파랑 빤작이
        }

        this.hp -= damage;
        UpdateHud();
        if (hp <= 0)
        {
            SetDie();
        }
    }
    

    // Update is called once per frame
    private void Update()
    {
        RotateWeapon();
        if (canMove() == false) return;
        ShieldRoutine();

        MoveAgainstTarget();
        //MoveToTarget();

   

    }

    //거리에 따라서 실드 껏다 켰다
    private void ShieldRoutine()
    {
        if (CanShieldOff() == true && isSheildOn == false)
        {
            ShieldOnOff(true);
        }
        else if (CanShieldOff() == false && isSheildOn == true)
        {
            ShieldOnOff(false);
        }

    }

    protected bool CanShieldOff()
    {
        return GetDistanceToPlayer() <= shieldOffDistance;
    }

    protected override IEnumerator FireRoutine()
    {
        while (true)
        {
            if(isSheildOn==false)
            FireWeapon();

            yield return new WaitForSeconds(1f);
        }
    }
        
    private void ShieldEffectOn()
    {
        GameObject target = spriteRenderer.gameObject;
        iTween.ColorTo(target, iTween.Hash("loopType", "pingPong", "Time", 0.05f, "Color", Color.blue));

        Invoke("ShieldEffectOff", 1f);

    }

    private void ShieldEffectOff()
    {
        GameObject target = spriteRenderer.gameObject;
        iTween.ColorTo(target, Color.white, 0.1f);
    }

}
