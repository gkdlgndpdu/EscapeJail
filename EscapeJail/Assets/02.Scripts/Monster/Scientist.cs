using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Scientist : CharacterBase
{
    private bool isSkillOn = false; 
    private float slowTimeRatio = 0.4f;

    private float maxSaveTime =5f;
    private float nowRemainTime = 0f;

    protected override void ResetAbility()
    {
        nowRemainTime = maxSaveTime;

    }
    protected override void DieAction()
    {
        base.DieAction();
        if (isSkillOn == true)
            SkillOnOff();
    }

    private new void Awake()
    {
        base.Awake();

        SetHp(10);

        SetSkillTime(5f);

    }

    private void SetSkillTime(float t)
    {
        nowRemainTime = t;
        maxSaveTime = t;
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
        if (isSkillOn == true)
        {
            nowRemainTime -= Time.unscaledDeltaTime ;
        }
        else if(isSkillOn == false)
        {
            nowRemainTime += Time.deltaTime;         
        }

        nowRemainTime = Mathf.Clamp(nowRemainTime, 0f, maxSaveTime);

        if (nowRemainTime <= 0)
        {
            SkillOnOff();
        }

        if (playerUi != null)
            playerUi.SetSkillButtonProgress(nowRemainTime, maxSaveTime);
    }

    public override void UseCharacterSkill()
    {
        SkillOnOff();
        Debug.Log("과학자 스킬");
    }

    public override void SetBurstSpeed(bool OnOff)
    {
        isBurstMoveOn = OnOff;

        if (isSkillOn == false)
        {
            if (OnOff == true)            
                moveSpeed = burstSpeed;
            
            else            
                moveSpeed = originSpeed;
            
        }
        else if (isSkillOn == true)
        {
            if (OnOff == true)    
                moveSpeed = burstSpeed / slowTimeRatio;                   
            else
                moveSpeed = originSpeed / slowTimeRatio;
        }

        
    }

    private void SkillOnOff()
    {

        if (isBurstMoveOn == false)
        {
            //켜기
            if (isSkillOn == false)
            {
                TimeManager.Instance.BulletTimeOn(slowTimeRatio);

                isSkillOn = true;
                moveSpeed = originSpeed / slowTimeRatio;

            }
            //끄기
            else if (isSkillOn == true)
            {
                TimeManager.Instance.BulletTimeOff();


                isSkillOn = false;
                moveSpeed = originSpeed;
            }
        }
        else if (isBurstMoveOn == true)
        {
            //켜기
            if (isSkillOn == false)
            {
                TimeManager.Instance.BulletTimeOn(slowTimeRatio);
                isSkillOn = true;
                moveSpeed = burstSpeed / slowTimeRatio;

            }
            //끄기
            else if (isSkillOn == true)
            {
                TimeManager.Instance.BulletTimeOff();
                isSkillOn = false;
                moveSpeed = burstSpeed;
            }
        }
       

    }

}
