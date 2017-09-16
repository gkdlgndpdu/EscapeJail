using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : CharacterInfo
{
    protected bool isPatternStart = false;
    [SerializeField]
    protected BossModule bossModule;
    public virtual  void StartBossPattern()
    {
      
   
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
    }

    protected void Awake()
    {

    }


}
