using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : CharacterInfo
{
    protected bool isPatternStart = false;
    [SerializeField]
    protected BossModule bossModule;
    [SerializeField]
    protected BossHpBar bosshpBar;

    //나머지는 자식에서 구현
    public virtual void StartBossPattern()
    {
        SetUiOnOff(true);
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
        AddToList();
        SetUiOnOff(false);


    }

    protected void SetUiOnOff(bool OnOff)
    {
        if (bosshpBar != null)
            bosshpBar.gameObject.SetActive(OnOff);
    }

    protected void Awake()
    {

    }


}
