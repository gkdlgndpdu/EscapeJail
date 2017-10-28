using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : BossBase
{

    //인스펙터에서 할당
    public List<Transform> moveList;

    private new void Awake()
    {
        base.Awake();
        SetHp(30);
         RegistPatternToQueue();
    }


    private enum Actions
    {
        FireStartTrigger,
        DeadTrigger,
        FireEndTrigger,
        BombAttackStart,
        Walk,
        Idle
    }

    public override void StartBossPattern()
    {
        base.StartBossPattern();
        Debug.Log("CriminalPattern Start");

        if (bossEventQueue != null)
            bossEventQueue.StartEventQueue();
    }



    private void Action(Actions action)
    {
        switch (action)
        {
            case Actions.FireStartTrigger:
                {
                    if (animator != null)
                        animator.SetTrigger("FireStartTrigger");
                }
                break;
            case Actions.DeadTrigger:
                {
                    if (animator != null)
                        animator.SetTrigger("DeadTrigger");
                }
                break;
            case Actions.FireEndTrigger:
                {
                    if (animator != null)
                        animator.SetTrigger("FireEndTrigger");
                }
                break;
            case Actions.BombAttackStart:
                {
                    if (animator != null)
                        animator.SetTrigger("BombAttackStart");
                }
                break;        
            case Actions.Walk:
                {
                    if (animator != null)
                        animator.SetFloat("Speed",1f);
                }
                break;
            case Actions.Idle:
                {
                    if (animator != null)
                        animator.SetFloat("Speed", 0f);
                }
                break;
        }
    }


    private void RegistPatternToQueue()
    {

        bossEventQueue.Initialize(this, EventOrder.InOrder);

        //  bossEventQueue.AddEvent("FirePattern1");
     
    }

}