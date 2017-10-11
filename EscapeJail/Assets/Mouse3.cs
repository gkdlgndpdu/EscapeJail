using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//박치기형
public class Mouse3 : MonsterBase
{

    private float RushPower = 5f;
    private float RushAfterDelay = 1f;
    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Mouse2;
        SetHp(10);
        nearestAcessDistance = 3f;

    } 

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetUpMonsterAttribute();
    }
    

    protected new void OnEnable()
    {
        base.OnEnable();

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


    protected override IEnumerator AttackRoutine()
    {
        nowAttack = true;

        //선딜
        yield return new WaitForSeconds(0.5f);
        AttackOn();

        Vector3 RushDir = GamePlayerManager.Instance.player.transform.position - this.transform.position;
        for (int i = 0; i < 30; i++)
        {
            RushDir.Normalize();

            if (rb != null)
                rb.velocity = RushDir * RushPower;

            yield return null;
        }



        //후딜
        yield return new WaitForSeconds(RushAfterDelay);
        AttackOff();
        nowAttack = false;
        if (rb != null)
            rb.velocity = Vector3.zero;

    
    }



}
