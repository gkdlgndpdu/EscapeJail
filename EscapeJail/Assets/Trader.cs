using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : CharacterBase
{
    private float skillCoolTimeMax = 10f;
    private float skillCount = 0f;
    private bool isSkillOn = true;

    protected override void ResetAbility()
    {
        skillCount = skillCoolTimeMax;
    }

    private IEnumerator skillCoolTimeRoutine()
    {
        skillCount = 0f;
        while (true)
        {
            skillCount += Time.deltaTime;

            if (playerUi != null)
                playerUi.SetSkillButtonProgress(skillCount, skillCoolTimeMax);

            if (skillCount >= skillCoolTimeMax)
            {
                isSkillOn = true;     

                yield break;
            }
            yield return null;
        }
    }

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
    }


    public override void UseCharacterSkill()
    {
        if (isSkillOn == true)
        {
            MakeItem();
            SoundManager.Instance.PlaySoundEffect("cape");
        }
    }


    private void MakeItem()
    {
        isSkillOn = false;
        StartCoroutine(skillCoolTimeRoutine());

        if (MyUtils.GetPercentResult(20) == true)
        {
            ItemSpawner.Instance.SpawnRandomItem(this.transform.position);
        }
        else
        {
            //무기 생성
            ItemSpawner.Instance.SpawnWeapon(this.transform.position);
        }


    }

}
