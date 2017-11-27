using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : CharacterBase
{
    [SerializeField]
    private Engineer_Drone drone;

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
            UseSkill();
        }
    }

    private void UseSkill()
    {
        isSkillOn = false;
        StartCoroutine(skillCoolTimeRoutine());

        //효과
        //총알삭제
        ObjectManager.Instance.AllEnemyBulletDestroy();
    }
}
