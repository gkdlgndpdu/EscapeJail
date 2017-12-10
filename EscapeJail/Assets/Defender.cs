using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : CharacterBase
{
    private bool isShieldOn = false;

    private float maxSaveTime = 5f;
    private float nowRemainTime = 0f;

    protected override void ResetAbility()
    {
        ShieldOff();
         moveSpeed = originSpeed;
        nowRemainTime = maxSaveTime;
    }

    private void SetSkillTime(float t)
    {
        nowRemainTime = t;
        maxSaveTime = t;
    }

    private new void Awake()
    {
        base.Awake();
        SetHp(10);
        SetSkillTime(5f);
    }
    private new void Start()
    {
        base.Start();
        SetWeapon();
        originSpeed = moveSpeed;
    }
    private new void Update()
    {
        base.Update();
        SkillRoutine();
    }
    private void SkillRoutine()
    {
        if (isShieldOn == true)
        {
            nowRemainTime -= Time.unscaledDeltaTime;
        }
        else if (isShieldOn == false)
        {
            nowRemainTime += Time.deltaTime;
        }

        nowRemainTime = Mathf.Clamp(nowRemainTime, 0f, maxSaveTime);

        if (nowRemainTime <= 0)
        {
            ShieldOff();
        }

        if (playerUi != null)
            playerUi.SetSkillButtonProgress(nowRemainTime, maxSaveTime);
    }
    public override void UseCharacterSkill()
    {
        if (isShieldOn == true)
        {
            ShieldOff();
        }
        else if (isShieldOn == false)
        {
            ShieldOn();
        }
    }

    private void ShieldOn()
    {
        if (animator != null)
            animator.SetTrigger("ShieldOn");

        isShieldOn = true;

        moveSpeed = originSpeed - 2f;
    }
    private void ShieldOff()
    {
        if (animator != null)
            animator.SetTrigger("ShieldOff");

        isShieldOn = false;

        moveSpeed = originSpeed;
    }

    public override void GetDamage(int damage)
    {
        if (isShieldOn == true) return;
        base.GetDamage(damage);
    }


}