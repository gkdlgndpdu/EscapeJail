using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : CharacterInfo
{
    protected bool isPatternStart = false;
 
    protected BossModule bossModule;

    protected BossEventQueue bossEventQueue;

    [SerializeField]
    protected BossHpBar bosshpBar;

    //나머지는 자식에서 구현
    public virtual void StartBossPattern()
    {
        SetUiOnOff(true);
        AddToList();
    }

    protected void AddToList()
    {
        MonsterManager.Instance.AddToList(this.gameObject);
    }

    protected void DeleteInList()
    {
        MonsterManager.Instance.DeleteInList(this.gameObject);
    }

    protected void OnDisable()
    {
        DeleteInList();
    }

    protected void Start()
    {
        SetUiOnOff(false);
    }

    protected void SetUiOnOff(bool OnOff)
    {
        if (bosshpBar != null)
            bosshpBar.gameObject.SetActive(OnOff);
    }

    protected void Awake()
    {
        bossModule = GetComponentInParent<BossModule>();
        bossEventQueue = GetComponent<BossEventQueue>();

    }

    protected void Initiaize()
    {

    }

    protected void BossDie()
    {
        StagerController.Instance.DestroyThisStage();
        StagerController.Instance.CreateNextStage();
        GamePlayerManager.Instance.ResetPlayerPosit();
    }


}
