﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Guard3 : MonsterBase
{

    private bool isSheildOn = false;

    private float originMoveSpeed = 2.5f;
    private float dashMoveSpeed = 3.5f;

    //해당 거리보다 멀리 있으면 실드킴 무조건
    private float shieldOffDistance = 3f;


    public override void ResetMonster()
    {
        base.ResetMonster();
        ShieldEffectOff();

    }

    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Guard3;
     
        nearestAcessDistance = 1f;
        weaponPosit.gameObject.SetActive(false);
        attackDelay = 1f;
        moveSpeed = originMoveSpeed;

    }


    private void ShieldOnOff(bool OnOff)
    {
        if (animator == null) return;

        isSheildOn = OnOff;

        if (OnOff == true)
        {
            animator.SetTrigger("ShieldOn");
            moveSpeed = originMoveSpeed;

        }
        else if (OnOff == false)
        {
            animator.SetTrigger("ShieldOff");
            moveSpeed = dashMoveSpeed;
        }
    }




    public override void GetDamage(int damage)
    {
        if (isSheildOn == true)
        {
            GoogleService.Instance.SetCharacterAchivement(CharacterType.Defender);
            ShieldEffectOn();
            return;
            //파랑 빤작이
        }
        VampiricGunEffect();
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
        NearAttackRotate();
        if (canMove() == false) return;
        ShieldRoutine();
        MoveToTarget();

        if(isSheildOn==false)
        NearAttackLogic();

    }

    //거리에 따라서 실드 껏다 켰다
    private void ShieldRoutine()
    {
        if (CanShieldOff() == true && isSheildOn == false)
        {
            ShieldOnOff(true);        
        }
        else if(CanShieldOff()==false&&isSheildOn==true)
        {
            ShieldOnOff(false);
        }
            
    }

    protected bool CanShieldOff()
    {
        return GetDistanceToPlayer()>=shieldOffDistance ;
    }

    protected override IEnumerator AttackRoutine()
    {
        nowAttack = true;
        SetAnimation(MonsterState.Attack);
        SoundManager.Instance.PlaySoundEffect("swing");
        yield return new WaitForSeconds(attackDelay);
        nowAttack = false;
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
