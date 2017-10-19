using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Scientist : CharacterBase
{
    private bool isSkillOn = false;

    private new void Awake()
    {
        base.Awake();

        SetHp(10);
    }

    private new void Start()
    {

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

    private void SkillOnOff()
    {
        //켜기
        if (isSkillOn == false)
        {
            Time.timeScale = 0.5f;
            isSkillOn = true;

        }
        //끄기
        else if (isSkillOn == true)
        {
            Time.timeScale = 1f;
            isSkillOn = false;
        }

    }

}
