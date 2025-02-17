﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class ExplosionEffect : MonoBehaviour
{

    private Animator animator;
    private bool isEffectOff = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// effectName인자에 null을 넣으면 기본이펙트가 나옴
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="lifeTime"></param>
    public void Initilaize(Vector3 startPos, string effectName, float lifeTime = 1,float size =1)
    {
        isEffectOff = false;

        this.gameObject.transform.position = startPos;

        this.transform.localScale = Vector3.one * size;

        if (effectName != null && animator != null)
        {
            RuntimeAnimatorController obj = Resources.Load<RuntimeAnimatorController>(string.Format("Animators/Effect/{0}", effectName));
              
            if (obj != null)
            {
             
                    animator.runtimeAnimatorController = obj;                
            }
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);            
        }     
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">0~255</param>
    public void SetAlpha(float value)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(1f, 1f, 1f, value / 255f);
    }
    

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }


    private void LateUpdate()
    {
        if (animator == null) return;

        if (AnimatorIsPlaying() == false&& isEffectOff==false)
        {
            //EffectOff();
            Invoke("EffectOff", 0.5f);
            isEffectOff = true;

        }

    }

    void EffectOff()
    {
        this.gameObject.SetActive(false);
    }


}
