﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class CharacterStateEffect : MonoBehaviour
{

    private float lifeTime = 0f;
    private float count = 0f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform target;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    public void Initialize(float lifeTime, float size, Transform target, CharacterCondition characterCondition,float alpha = 136f)
    {
        this.target = target;
        this.lifeTime = lifeTime;
        this.transform.localScale = Vector3.one * size;
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha/255f);
        SetEffect(characterCondition);
    }
  
    private void SetEffect(CharacterCondition characterCondition)
    {
        string path = string.Format("Animators/Effect/{0}", characterCondition.ToString());
        RuntimeAnimatorController animController= Resources.Load<RuntimeAnimatorController>(path);
        if (animator != null&& animController!=null)
        {
            animator.runtimeAnimatorController = animController;
        }


    }

 
    /// <summary>
    /// 이펙트가 사라지는 시간을 줄여줌
    /// </summary>
    public void CountReset()
    {
        count = 0f;
    }

    private void Update()
    {
        if (target != null)
            this.transform.position = target.transform.position;
        else if (target == null)
            EffectOff();

            count += Time.deltaTime;
        if (count >= lifeTime)
        {
            EffectOff();
        }
    }

    public void EffectOff()
    {
        CountReset();  
        gameObject.SetActive(false);     
    }   


}
