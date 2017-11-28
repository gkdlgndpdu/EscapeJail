using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : CharacterBase
{
    private float skillCoolTimeMax = 10f;
    private bool isSkillOn = true;

    private IEnumerator skillCoolTimeRoutine()
    {
        float count = 0f;
        while (true)
        {
            count += Time.deltaTime;

            if (playerUi != null)
                playerUi.SetSkillButtonProgress(count, skillCoolTimeMax);

            if (count >= skillCoolTimeMax)
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
        }
    }


    private void MakeItem()
    {
        isSkillOn = false;
        StartCoroutine(skillCoolTimeRoutine());

        if (MyUtils.GetPercentResult(20) == true)
        {
            ItemSpawner.Instance.SpawnRandomItem(this.transform.position, MapManager.Instance.transform);
        }
        else
        {
            //무기 생성
            ItemSpawner.Instance.SpawnWeapon(this.transform.position, MapManager.Instance.transform);
        }


    }

}
