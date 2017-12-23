using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : CharacterBase
{
    [SerializeField]
    private GameObject DronePrefab;

    private Engineer_Drone drone;

    [SerializeField]
    private Transform dronePos;

    private float skillCount = 0f;

    private float skillCoolTimeMax = 10f;
    private bool isSkillOn = true;

    protected override void ResetAbility()
    {       
        skillCount = skillCoolTimeMax;
    }

    private IEnumerator skillCoolTimeRoutine()
    {
        skillCount = 0;
        while (true)
        {
            skillCount += Time.deltaTime;

            if (playerUi != null)
                playerUi.SetSkillButtonProgress(skillCount, skillCoolTimeMax);

            if (skillCount >= skillCoolTimeMax)
            {
                isSkillOn = true;

                if (drone != null)
                    drone.ShowDrone();

                
                yield break;
            }
            yield return null;
        }
    }

    private new void Awake()
    {
        base.Awake();

        SetDrone();
    }

    private void SetDrone()
    {
        if (DronePrefab == null) return;

        GameObject makeObj = Instantiate(DronePrefab);
        if (makeObj != null)
        {
            Engineer_Drone droneobj = makeObj.GetComponent<Engineer_Drone>();
            if (droneobj != null)
            {
                droneobj.SetTarget(dronePos);
                this.drone = droneobj;
            }

        }

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
        SoundManager.Instance.PlaySoundEffect("engineerfireworks");
        if (drone != null)
            drone.HideDrone();
    }
}
