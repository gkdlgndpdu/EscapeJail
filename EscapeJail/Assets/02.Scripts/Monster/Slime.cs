using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonsterBase
{
    //유정란
    public bool CanDivide = true;
    private int babyNum = 2;

    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Slime;
      
        nearestAcessDistance = 1f;

    }

    public override void ResetMonster()
    {
        base.ResetMonster();
        AttackOn();
        CanDivide = true;
        
    }

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetUpMonsterAttribute();
    }

    protected override void SetDie()
    {
        base.SetDie();
        if (parentModule != null && CanDivide == true)
        {
            if (animator != null)                
                animator.SetTrigger("DivideTrigger");
            
        }
        else
        {
            if (animator != null)
                animator.SetTrigger("SlimeDeadTrigger");
        }


    }

    public void Divide()
    {
        for (int i = 0; i < babyNum; i++)
        {
            MonsterBase slime = parentModule.SpawnSpecificMonsterInModule(monsterName, this.transform.position);
            Slime babySlime = slime as Slime;
            if (babySlime != null)
            {
                babySlime.CanDivide = false;
                babySlime.transform.localScale = babySlime.transform.localScale * 0.5f;
            }
        }
    }


    protected new void OnEnable()
    {
        base.OnEnable();

    }

    protected  void OnDisable()
    {
        if (CanDivide == false)
        {
            this.transform.localScale = Vector3.one;
        }
    }

    private new void Awake()
    {
        base.Awake();

    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove() == false) return;

        MoveToTarget();
        NearAttackLogic();


    }


}
