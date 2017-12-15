using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(Animator))]
public class MonsterSpawnEffect : MonoBehaviour
{
    //애니메이션이 종료되면 실행될 함수
    Action<Vector3> animationEndFunc;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Initialize(Vector3 spawnPosit,Action<Vector3> animationEndFunc)
    {
        this.transform.position = spawnPosit;
       this.animationEndFunc = animationEndFunc;
        SoundManager.Instance.PlaySoundEffect("monstershow");

        if (animator != null)
        animator.SetTrigger(StagerController.Instance.NowStageLevel.ToString());
            
        


    }

    private void Update()
    {
        if (AnimatorIsPlaying() == false)
        {
            if (animationEndFunc != null)
                animationEndFunc.Invoke(this.transform.position);

            EffectOff();



        }
    }

    private void EffectOff()
    {
        this.gameObject.SetActive(false);
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

}
