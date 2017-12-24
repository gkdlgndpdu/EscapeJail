using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : CharacterBase
{
    private bool isShieldOn = false;

    private float maxSaveTime = 5f;
    private float nowRemainTime = 0f;
    private float decreaseValue = 1f;

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

    public override void SetFire()
    {
        if(isShieldOn==false)
        base.SetFire();
    }
    public override void SetPoison()
    {
        if (isShieldOn == false)
            base.SetPoison();
    }

    private new void Awake()
    {
        base.Awake();
      
        SetSkillTime(5f);
    }
    private new void Start()
    {
        base.Start();
        SetWeapon();

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

    public override void SetBurstSpeed(bool OnOff)
    {
        isBurstMoveOn = OnOff;

        if (isShieldOn == true)
            ShieldOff();


        if (OnOff == true)
        {
            moveSpeed = burstSpeed;
        }
        else
        {
            moveSpeed = originSpeed;
        }


    }

    private void ShieldOn()
    {
        if (animator != null)
            animator.SetTrigger("ShieldOn");

        isShieldOn = true;

        if (isBurstMoveOn == false)
            moveSpeed = originSpeed - decreaseValue;
        else if (isBurstMoveOn == true)
            moveSpeed = burstSpeed - decreaseValue;

        SoundManager.Instance.PlaySoundEffect("shieldset");

    }
    private void ShieldOff()
    {
        if (animator != null)
            animator.SetTrigger("ShieldOff");

        isShieldOn = false;

        if (isBurstMoveOn == false)
            moveSpeed = originSpeed;
        else if (isBurstMoveOn == true)
            moveSpeed = burstSpeed;
 
        SoundManager.Instance.PlaySoundEffect("shieldset");
    }

    public override void GetDamage(int damage)
    {
        if (isShieldOn == true) return;
        
        base.GetDamage(damage);
        
    }

    protected override void SkillOff()
    {
        if(isShieldOn==true)
        ShieldOff();
    }


}