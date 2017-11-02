using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Scientist : CharacterBase
{
    private bool isSkillOn = false; 
    private float slowTimeRatio = 0.4f;

    private new void Awake()
    {
        base.Awake();

        SetHp(10);
 
    }

    private new void Start()
    {
        base.Start();
        SetWeapon();
    }

    private new void Update()
    {
        base.Update();
#if UNITY_EDITOR
        //임시코드
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UseCharacterSkill();
        }
        //임시코드
#endif
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
